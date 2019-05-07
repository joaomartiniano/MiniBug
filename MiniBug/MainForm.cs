// Copyright(c) João Martiniano. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MiniBug
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// If true, signals that the issues grid is initializing.
        /// </summary>
        private bool initializingGridIssues = false;

        /// <summary>
        /// If true, signals that the tasks grid is initializing.
        /// </summary>
        private bool initializingGridTasks = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Start by retrieving the application settings
            ApplicationSettings.Load();

            // *** debug
            HelperClass.DebugDisplayIndex("3: MainForm.cs: ApplicationSettings.GridIssuesColumns");

            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.Icon = MiniBug.Properties.Resources.Minibug;
            this.Text = "MiniBug Issue Tracker";
            this.MinimumSize = new Size(478, 303);

            InitializeTabControl();

            // *** debug
            HelperClass.DebugDisplayIndex("4: MainForm.cs: antes de InitializeGridIssues(): ApplicationSettings.GridIssuesColumns");

            // Initialization of the Issues and Tasks grids
            InitializeGridIssues();
            InitializeGridTasks();

            // *** debug
            HelperClass.DebugDisplayIndex("5: MainForm.cs: após InitializeGridIssues(): ApplicationSettings.GridIssuesColumns");

            // Apply the settings to the Issues and Tasks grids
            ApplySettingsToGrids();

            // Populate the Issues and Tasks grids
            PopulateGridIssues();
            PopulateGridTasks();            

            SetControlsState();

            // Initialize the recent projects submenu
            InitializeRecentProjects();

            // Resume the layout logic
            this.ResumeLayout();

            // Set the sort glyph for the issues and tasks DataGridViews
            SetGridSortGlyph(GridType.All);


            // *** display index

            // *** debug
            /*Console.WriteLine("3: MainForm.cs: What's stored in ApplicationSettings...");
            foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                Console.WriteLine("Item: {0} - Display index: {1}", item.Value.Name, item.Value.DisplayIndex);
            }*/
            HelperClass.DebugDisplayIndex("FINAL: MainForm.cs: ApplicationSettings.GridIssuesColumns");


            /*GridIssues.Refresh();
            foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                GridIssues.Columns[item.Value.Name].DisplayIndex = item.Value.DisplayIndex;
            }
            GridIssues.Refresh();
            GridIssues.Columns["priority"].DisplayIndex = 7;*/
        }

        /// <summary>
        /// Initialize the recent projects submenu.
        /// </summary>
        private void InitializeRecentProjects()
        {
            bool flag = false;

            // Initialize the settings for recent projects
            if ((Properties.Settings.Default.RecentProjectsNames == null) || (Properties.Settings.Default.RecentProjectsPaths == null))
            {
                Properties.Settings.Default.RecentProjectsNames = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.RecentProjectsPaths = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();

                flag = true;
            }
            else if ((Properties.Settings.Default.RecentProjectsNames.Count > 0) && (Properties.Settings.Default.RecentProjectsPaths.Count > 0))
            {
                // Load the recent projects from the application settings and insert them in the recent projects submenu
                for (int i = 0; i <= Properties.Settings.Default.RecentProjectsNames.Count - 1; ++i)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(Properties.Settings.Default.RecentProjectsNames[i])
                    {
                        Tag = Properties.Settings.Default.RecentProjectsPaths[i]
                    };
                    recentProjectsToolStripMenuItem.DropDownItems.Add(item);

                    // Add an event handler for the new menu item
                    item.Click += new System.EventHandler(this.FileMenuRecentProjectItem_Click);
                }
            }
            else
            {
                flag = true;
            }

            // If there are no recent projects, disable menu items
            if (flag)
            {
                // Disable the recent projects menu item
                recentProjectsToolStripMenuItem.Enabled = false;

                // Disable the clear recent projects list menu item
                clearRecentProjectsListToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Initialize the tab control.
        /// </summary>
        private void InitializeTabControl()
        {
            TabControl.Left = this.ClientRectangle.Left;
            TabControl.Width = this.ClientRectangle.Width + 3;
            TabControl.Height = this.ClientRectangle.Height - toolStrip1.Height;
            // *** TabControl.Dock = DockStyle.
        }

        /// <summary>
        /// Enable/disable controls when the user changes tabs.
        /// </summary>
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsState();
        }

        /// <summary>
        /// Sets the state (enabled/disabled) of some controls (menu items, toolbar icons, etc.), based on current conditions.
        /// </summary>
        private void SetControlsState()
        {
            // The user can edit the project settings only if the current project != null
            editProjectToolStripMenuItem.Enabled = (Program.SoftwareProject == null) ? false : true;

            // The user can export the project only if the current project != null AND there are issues OR tasks
            exportToolStripMenuItem.Enabled = false;
            if (Program.SoftwareProject != null)
            {
                if (((Program.SoftwareProject.Issues != null) && (Program.SoftwareProject.Issues.Count > 0)) ||
                    ((Program.SoftwareProject.Tasks != null) && (Program.SoftwareProject.Tasks.Count > 0)))
                {
                    exportToolStripMenuItem.Enabled = true;
                }
            }
            
            if (TabControl.SelectedIndex == 0) // The tab 'Issues' is selected
            {
                // The user can create a new issue only if the current project != null
                newIssueToolStripMenuItem.Enabled = IconNewIssue.Enabled = (Program.SoftwareProject == null) ? false : true;

                if ((Program.SoftwareProject != null) && (Program.SoftwareProject.Issues != null) && (GridIssues.Rows.Count > 0))
                {
                    // ENABLE these controls if there are issues
                    editIssueToolStripMenuItem.Enabled = true;
                    deleteIssueToolStripMenuItem.Enabled = true;
                    cloneIssueToolStripMenuItem.Enabled = true;
                    IconEditIssue.Enabled = true;
                    IconDeleteIssue.Enabled = true;
                    IconCloneIssue.Enabled = true;
                }
                else
                {
                    // DISABLE these controls if there are no issues
                    editIssueToolStripMenuItem.Enabled = false;
                    deleteIssueToolStripMenuItem.Enabled = false;
                    cloneIssueToolStripMenuItem.Enabled = false;
                    IconEditIssue.Enabled = false;
                    IconDeleteIssue.Enabled = false;
                    IconCloneIssue.Enabled = false;
                }

                // Disable tasks menu items
                newTaskToolStripMenuItem.Enabled = false;
                editTaskToolStripMenuItem.Enabled = false;
                deleteTaskToolStripMenuItem.Enabled = false;
                cloneTaskToolStripMenuItem.Enabled = false;

                // Disable tasks toolbar icons
                IconNewTask.Enabled = false;
                IconEditTask.Enabled = false;
                IconDeleteTask.Enabled = false;
                IconCloneTask.Enabled = false;
            }
            else if (TabControl.SelectedIndex == 1) // The tab 'Tasks' is selected
            {
                // The user can create a new task only if the current project != null
                newTaskToolStripMenuItem.Enabled = IconNewTask.Enabled = (Program.SoftwareProject == null) ? false : true;

                if ((Program.SoftwareProject != null) && (Program.SoftwareProject.Tasks != null) && (GridTasks.Rows.Count > 0))
                {
                    // ENABLE these controls if there are tasks
                    editTaskToolStripMenuItem.Enabled = true;
                    deleteTaskToolStripMenuItem.Enabled = true;
                    cloneTaskToolStripMenuItem.Enabled = true;
                    IconEditTask.Enabled = true;
                    IconDeleteTask.Enabled = true;
                    IconCloneTask.Enabled = true;
                }
                else
                {
                    // DISABLE these controls if there are no issues
                    editTaskToolStripMenuItem.Enabled = false;
                    deleteTaskToolStripMenuItem.Enabled = false;
                    cloneTaskToolStripMenuItem.Enabled = false;
                    IconEditTask.Enabled = false;
                    IconDeleteTask.Enabled = false;
                    IconCloneTask.Enabled = false;
                }

                // Disable issues menu items
                newIssueToolStripMenuItem.Enabled = false;
                editIssueToolStripMenuItem.Enabled = false;
                deleteIssueToolStripMenuItem.Enabled = false;
                cloneIssueToolStripMenuItem.Enabled = false;

                // Disable issues toolbar icons
                IconNewIssue.Enabled = false;
                IconEditIssue.Enabled = false;
                IconDeleteIssue.Enabled = false;
                IconCloneIssue.Enabled = false;
            }
        }

        /// <summary>
        /// Handle closing of the main form.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseApplication();
            e.Cancel = false;
        }

        /// <summary>
        /// Perform cleanup or final operations when the application is about to close.
        /// </summary>
        private void CloseApplication()
        {
            GridTasksGetDisplayIndexForAll();

            // Save the order of the columns in the issues and tasks DataGridViews
            ApplicationSettings.Save(ApplicationSettings.SaveSettings.ColumnOrderSort);
        }

        #region RecentProject
        /// <summary>
        /// Add a project to the recent projects submenu and to the application settings
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="filename">The path and name of the file.</param>
        private void AddRecentProject(string projectName, string fileName)
        {
            recentProjectsToolStripMenuItem.Enabled = true;
            clearRecentProjectsListToolStripMenuItem.Enabled = true;

            // Determine if the project already exists in the Recent Projects submenu
            for (int i = 0, c = recentProjectsToolStripMenuItem.DropDownItems.Count; i < c; ++i)
            {
                if (recentProjectsToolStripMenuItem.DropDownItems[i].Text == projectName)
                {
                    // If the project is already at the top of the list, do nothing
                    if (i == 0)
                    {
                        return;
                    }

                    // Remove the item from its current position
                    recentProjectsToolStripMenuItem.DropDownItems.RemoveAt(i);
                    Properties.Settings.Default.RecentProjectsNames.RemoveAt(i);
                    Properties.Settings.Default.RecentProjectsPaths.RemoveAt(i);

                    // Insert at the top of the list
                    AddRecentProjectItem(projectName, fileName);

                    return;
                }
            }
            
            // Remove the last item in the menu when the maximum number of items is reached
            if (recentProjectsToolStripMenuItem.DropDownItems.Count == ApplicationSettings.MaxRecentProjects)
            {
                recentProjectsToolStripMenuItem.DropDownItems.RemoveAt(recentProjectsToolStripMenuItem.DropDownItems.Count - 1);

                Properties.Settings.Default.RecentProjectsNames.RemoveAt(Properties.Settings.Default.RecentProjectsNames.Count - 1);
                Properties.Settings.Default.RecentProjectsPaths.RemoveAt(Properties.Settings.Default.RecentProjectsPaths.Count - 1);
            }

            // Add the new menu item to the top of the submenu
            AddRecentProjectItem(projectName, fileName);
        }

        /// <summary>
        /// Add the recent project item details to the top of the submenu and to the application settings.
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="fileName"></param>
        private void AddRecentProjectItem(string projectName, string fileName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(projectName)
            {
                Tag = fileName
            };

            // Add the new menu item to the top of the submenu
            recentProjectsToolStripMenuItem.DropDownItems.Insert(0, item);

            // Save the project name and path and filename in the application settings
            Properties.Settings.Default.RecentProjectsNames.Insert(0, projectName);
            Properties.Settings.Default.RecentProjectsPaths.Insert(0, fileName);
            Properties.Settings.Default.Save();

            // Add an event handler for the new menu item
            item.Click += new System.EventHandler(this.FileMenuRecentProjectItem_Click);
        }

        /// <summary>
        /// Clear the recent projects list.
        /// </summary>
        private void ClearRecentProjects()
        {
            // Clear the recent projects submenu
            recentProjectsToolStripMenuItem.DropDownItems.Clear();

            // Clear the recent projects (file names and paths) in the application settings
            Properties.Settings.Default.RecentProjectsNames = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.RecentProjectsPaths = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.Save();

            // Disable the recent projects menu item
            recentProjectsToolStripMenuItem.Enabled = false;

            // Disable the clear recent projects list menu item
            clearRecentProjectsListToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Occurs when one of the recent projects menu item is clicked.
        /// </summary>
        private void FileMenuRecentProjectItem_Click(object sender, EventArgs e)
        {
            OpenProject(((ToolStripMenuItem)sender).Tag.ToString());
        }
        #endregion

        #region Project
        /// <summary>
        /// Create a new project.
        /// </summary>
        private void NewProject()
        {
            FileSystemOperationStatus status = FileSystemOperationStatus.None;
            ProjectForm frmProject = new ProjectForm(OperationType.New);

            DialogResult result = frmProject.ShowDialog();

            if (result == DialogResult.OK)
            {
                Program.SoftwareProject = null;

                // Clear the issues and tasks grids
                GridIssues.Rows.Clear();
                GridIssues.Refresh();
                GridTasks.Rows.Clear();
                GridTasks.Refresh();

                Program.SoftwareProject = new Project(frmProject.ProjectName)
                {
                    Filename = frmProject.ProjectFilename,
                    Location = frmProject.ProjectLocation
                };

                status = ApplicationData.SaveProject(Program.SoftwareProject);
            }
            
            frmProject.Dispose();

            if (result == DialogResult.Cancel) return;

            // If there was an error creating the new project file, show feedback
            if (status != FileSystemOperationStatus.OK)
            {
                ShowProjectErrorFeedback(status);

                // Abort the new project
                Program.SoftwareProject = null;
            }
            else
            {
                // Set the main form title bar text
                this.Text = $"{frmProject.ProjectName} - MiniBug Issue Tracker";

                // Add this project to the recent projects submenu and application settings
                AddRecentProject(Program.SoftwareProject.Name, System.IO.Path.Combine(Program.SoftwareProject.Location, Program.SoftwareProject.Filename));
            }

            SetControlsState();
        }

        /// <summary>
        /// Open an existing project.
        /// </summary>
        /// <param name="filename">(optional) The file to open. If present, the project file is opened directly.</param>
        private void OpenProject(string filename = "")
        {
            bool flag = (filename != string.Empty) ? true : false;

            if (filename == string.Empty)
            {
                openFileDialog1.Title = "Open Project";
                openFileDialog1.Multiselect = false;
                openFileDialog1.Filter = "JSON files (*.json)|*.json";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.FileName = string.Empty;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    flag = true;
                }
            }

            if (flag)
            {
                FileSystemOperationStatus status = FileSystemOperationStatus.None;
                Project newProject = new Project();

                // *** ??? o que é a linha seguinte, comentada? remover ??
                //status = ApplicationData.LoadProject(filename, out Program.SoftwareProject);
                status = ApplicationData.LoadProject(filename, out newProject);

                // If there was an error loading the new project file, show feedback
                if (status != FileSystemOperationStatus.OK)
                {
                    ShowProjectErrorFeedback(status);

                    // Abort the new project
                    //Program.SoftwareProject = null;
                    newProject = null;
                }
                else
                {
                    Program.SoftwareProject = null;
                    Program.SoftwareProject = newProject;

                    // Set the main form title bar text
                    this.Text = $"{Program.SoftwareProject.Name} - MiniBug Issue Tracker";

                    // Clear the issues and tasks grids
                    GridIssues.Rows.Clear();
                    GridIssues.Refresh();
                    GridTasks.Rows.Clear();
                    GridTasks.Refresh();

                    // Suspend the layout logic for the form, while the application is initializing
                    this.SuspendLayout();

                    PopulateGridIssues();
                    PopulateGridTasks();

                    // Add this project to the recent projects submenu and application settings
                    AddRecentProject(Program.SoftwareProject.Name, System.IO.Path.Combine(Program.SoftwareProject.Location, Program.SoftwareProject.Filename));

                    // Resume the layout logic
                    this.ResumeLayout();
                }
            }

            SetControlsState();
        }

        /// <summary>
        /// Edit the current project settings.
        /// </summary>
        private void EditProject()
        {
            FileSystemOperationStatus status = FileSystemOperationStatus.None;
            ProjectForm frmProject = new ProjectForm(OperationType.Edit, Program.SoftwareProject.Name, Program.SoftwareProject.Filename, Program.SoftwareProject.Location);

            if (frmProject.ShowDialog() == DialogResult.OK)
            {
                // Set the main form title bar text
                this.Text = $"{frmProject.ProjectName} - MiniBug Issue Tracker";

                Program.SoftwareProject.Name = frmProject.ProjectName;
                Program.SoftwareProject.Filename = frmProject.ProjectFilename;
                Program.SoftwareProject.Location = frmProject.ProjectLocation;

                status = ApplicationData.SaveProject(Program.SoftwareProject);
            }

            frmProject.Dispose();

            // If there was an error creating the new project file, show feedback
            if (status != FileSystemOperationStatus.OK)
            {
                ShowProjectErrorFeedback(status);

                // **** TODO: reverter alterações ****
                // Abort the new project
                //Program.SoftwareProject = null;
            }
        }

        /// <summary>
        /// Export the current project.
        /// </summary>
        private void ExportProject()
        {
            DialogResult FrmExportResult = DialogResult.None;
            ExportForm frmExport = new ExportForm();

            // Show the export to CSV form
            FrmExportResult = frmExport.ShowDialog();
            frmExport.Dispose();

            if (FrmExportResult == DialogResult.Cancel)
            {
                return;
            }

            // Export the project
            Program.SoftwareProject.Export(frmExport.IssuesFilename, frmExport.TasksFilename);

            // Show feedback about the project export operation
            ImportExportFeedbackForm frmFeedback = new ImportExportFeedbackForm(ImportExportOperation.Export);
            frmFeedback.ShowDialog();
            frmFeedback.Dispose();

            // Clear the export result information
            Program.SoftwareProject.ExportResult = null;
        }

        /// <summary>
        /// Saves the current project. Every operation that modifies the current project must call this method after modifying the data.
        /// </summary>
        /// <param name="context"></param>
        //private void SaveProject(OperationContext context)
        private void SaveProject()
        {
           FileSystemOperationStatus status = FileSystemOperationStatus.None;
            status = ApplicationData.SaveProject(Program.SoftwareProject);

            // If there was an error saving the project file, show feedback
            if (status != FileSystemOperationStatus.OK)
            {
                ShowProjectErrorFeedback(status);
            }
        }

        /// <summary>
        /// Show feedback when an error occurs, when saving the project file.
        /// </summary>
        /// <param name="status"></param>
        private void ShowProjectErrorFeedback(FileSystemOperationStatus status)
        {
            if (status != FileSystemOperationStatus.None)
            {
                FeedbackForm frmFeedback = new FeedbackForm();
                frmFeedback.ComposeMessage(status);
                frmFeedback.ShowDialog();
                frmFeedback.Dispose();
            }
        }
        #endregion

        #region Menu
        /// <summary>
        /// Create a new project.
        /// </summary>
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        /// <summary>
        /// Open an existing project.
        /// </summary>
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        /// <summary>
        /// Edit the current project settings.
        /// </summary>
        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditProject();
        }

        /// <summary>
        /// Export the current project issues and tasks to a file.
        /// </summary>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportProject();
        }

        /// <summary>
        /// Change the application settings.
        /// </summary>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm frmSettings = new SettingsForm();

            if (frmSettings.ShowDialog() == DialogResult.OK)
            {
                ApplySettingsToGrids();
            }

            frmSettings.Dispose();
        }

        /// <summary>
        /// Clear the recent projects list.
        /// </summary>
        private void clearRecentProjectsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearRecentProjects();
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Create a new issue.
        /// </summary>
        private void newIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewIssue();
        }

        /// <summary>
        /// Edit the selected issue.
        /// </summary>
        private void editIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditIssue();
        }

        /// <summary>
        /// Delete the selected issues.
        /// </summary>
        private void deleteIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteIssue();
        }

        /// <summary>
        /// Clone the selected issue.
        /// </summary>
        private void cloneIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloneIssue();
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTask();
        }

        /// <summary>
        /// Edit the selected task.
        /// </summary>
        private void editTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        /// <summary>
        /// Delete the selected tasks.
        /// </summary>
        private void deleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        /// <summary>
        /// Clone the selected task.
        /// </summary>
        private void cloneTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloneTask();
        }

        /// <summary>
        /// Show information about this application.
        /// </summary>
        private void aboutMiniBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();

            frmAbout.ShowDialog();

            frmAbout.Dispose();
        }
        #endregion

        #region Toolbar

        /// <summary>
        /// Create a new issue.
        /// </summary>
        private void IconNewIssue_Click(object sender, EventArgs e)
        {
            NewIssue();
        }

        /// <summary>
        /// Edit the selected issue.
        /// </summary>
        private void IconEditIssue_Click(object sender, EventArgs e)
        {
            EditIssue();
        }

        /// <summary>
        /// Delete the selected issues.
        /// </summary>
        private void IconDeleteIssue_Click(object sender, EventArgs e)
        {
            DeleteIssue();
        }

        /// <summary>
        /// Clone the selected issue.
        /// </summary>
        private void IconCloneIssue_Click(object sender, EventArgs e)
        {
            CloneIssue();
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        private void IconNewTask_Click(object sender, EventArgs e)
        {
            NewTask();
        }

        /// <summary>
        /// Edit the selected task.
        /// </summary>
        private void IconEditTask_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        /// <summary>
        /// Delete the selected tasks.
        /// </summary>
        private void IconDeleteTask_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        /// <summary>
        /// Clone the selected task.
        /// </summary>
        private void IconCloneTask_Click(object sender, EventArgs e)
        {
            CloneTask();
        }

        #endregion

        /// <summary>
        ///  Apply the settings to the Issues and Tasks grids.
        /// </summary>
        private void ApplySettingsToGrids()
        {
            // Apply settings to the Issues grid

            // Grid Font
            GridIssues.Font = ApplicationSettings.GridFont;
            GridIssues.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            // Grid borders
            if (ApplicationSettings.GridShowBorders)
            {
                GridIssues.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                GridIssues.GridColor = ApplicationSettings.GridBorderColor;
            }
            else
            {
                GridIssues.CellBorderStyle = DataGridViewCellBorderStyle.None;
            }

            // Selection color
            GridIssues.DefaultCellStyle.SelectionBackColor = ApplicationSettings.GridSelectionBackColor;
            GridIssues.DefaultCellStyle.SelectionForeColor = ApplicationSettings.GridSelectionForeColor;

            // Alternating row colors
            GridIssues.RowsDefaultCellStyle.BackColor = ApplicationSettings.GridRowBackColor;
            if (ApplicationSettings.GridAlternatingRowColor)
            {
                GridIssues.AlternatingRowsDefaultCellStyle.BackColor = ApplicationSettings.GridAlternateRowBackColor;
            }
            else
            {
                GridIssues.AlternatingRowsDefaultCellStyle.BackColor = ApplicationSettings.GridRowBackColor;
            }

            // (end Issues grid)

            // Apply settings to the Tasks grid

            // Grid Font
            GridTasks.Font = ApplicationSettings.GridFont;
            GridTasks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            // Grid borders
            if (ApplicationSettings.GridShowBorders)
            {
                GridTasks.GridColor = ApplicationSettings.GridBorderColor;
            }
            else
            {
                GridTasks.CellBorderStyle = DataGridViewCellBorderStyle.None;
            }

            // Selection color
            GridTasks.DefaultCellStyle.SelectionBackColor = ApplicationSettings.GridSelectionBackColor;
            GridTasks.DefaultCellStyle.SelectionForeColor = ApplicationSettings.GridSelectionForeColor;

            // Alternating row colors
            GridTasks.RowsDefaultCellStyle.BackColor = ApplicationSettings.GridRowBackColor;
            if (ApplicationSettings.GridAlternatingRowColor)
            {
                GridTasks.AlternatingRowsDefaultCellStyle.BackColor = ApplicationSettings.GridAlternateRowBackColor;
            }

            // (end Tasks grid)
        }

        /// <summary>
        /// Display the sort glyph, in the sort columns of the issues or tasks DataGridViews.
        /// </summary>
        /// <param name="grid">The DataGridView to apply the sort glyph.</param>
        private void SetGridSortGlyph(GridType grid)
        {
            if ((grid == GridType.All) || (grid == GridType.Issues))
            {
                GridIssues.Columns[ApplicationSettings.GridIssuesColumns[ApplicationSettings.GridIssuesSort.FirstColumn].Name].HeaderCell.SortGlyphDirection = ApplicationSettings.GridIssuesSort.FirstColumnSortOrder;

                if (ApplicationSettings.GridIssuesSort.SecondColumn != null)
                {
                    GridIssues.Columns[ApplicationSettings.GridIssuesColumns[ApplicationSettings.GridIssuesSort.SecondColumn.Value].Name].HeaderCell.SortGlyphDirection = ApplicationSettings.GridIssuesSort.SecondColumnSortOrder.Value;
                }
            }

            if ((grid == GridType.All) || (grid == GridType.Tasks))
            {
                GridTasks.Columns[ApplicationSettings.GridTasksColumns[ApplicationSettings.GridTasksSort.FirstColumn].Name].HeaderCell.SortGlyphDirection = ApplicationSettings.GridTasksSort.FirstColumnSortOrder;

                if (ApplicationSettings.GridTasksSort.SecondColumn != null)
                {
                    GridTasks.Columns[ApplicationSettings.GridTasksColumns[ApplicationSettings.GridTasksSort.SecondColumn.Value].Name].HeaderCell.SortGlyphDirection = ApplicationSettings.GridTasksSort.SecondColumnSortOrder.Value;
                }
            }
        }

        /// <summary>
        /// Remove the sort glyph, in the specificied columns, of the issues and/or tasks DataGridView.
        /// </summary>
        /// <param name="grid">The DataGridView(s) to remove the sort glyph.</param>
        /// <param name="issuesSortSettings">Contains the columns to remove the glyph in the issues DataGridView.</param>
        /// <param name="tasksSortSettings">Contains the columns to remove the glyph in the tasks DataGridView.</param>
        private void RemoveGridSortGlyph(GridType grid, GridIssuesSortSettings issuesSortSettings, GridTasksSortSettings tasksSortSettings)
        {
            if ((issuesSortSettings != null) && ((grid == GridType.All) || (grid == GridType.Issues)))
            {
                GridIssues.Columns[ApplicationSettings.GridIssuesColumns[issuesSortSettings.FirstColumn].Name].HeaderCell.SortGlyphDirection = SortOrder.None;

                if (issuesSortSettings.SecondColumn != null)
                {
                    GridIssues.Columns[ApplicationSettings.GridIssuesColumns[issuesSortSettings.SecondColumn.Value].Name].HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }

            if ((tasksSortSettings != null) && ((grid == GridType.All) || (grid == GridType.Tasks)))
            {
                GridTasks.Columns[ApplicationSettings.GridTasksColumns[tasksSortSettings.FirstColumn].Name].HeaderCell.SortGlyphDirection = SortOrder.None;

                if (tasksSortSettings.SecondColumn != null)
                {
                    GridTasks.Columns[ApplicationSettings.GridTasksColumns[tasksSortSettings.SecondColumn.Value].Name].HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
        }

        #region Tasks
        /// <summary>
        /// Initialize the tasks DataGridView.
        /// </summary>
        private void InitializeGridTasks()
        {
            initializingGridTasks = true;

            GridTasks.BackgroundColor = TabControl.DefaultBackColor;
            GridTasks.BorderStyle = BorderStyle.None;
            GridTasks.Dock = DockStyle.Fill;

            GridTasks.AllowUserToAddRows = false;
            GridTasks.AllowUserToDeleteRows = false;
            GridTasks.AllowUserToOrderColumns = true;
            GridTasks.AllowUserToResizeColumns = true;
            GridTasks.AllowUserToResizeRows = false;
            GridTasks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            GridTasks.ColumnHeadersVisible = true;
            GridTasks.RowHeadersVisible = false;
            GridTasks.ReadOnly = true;
            GridTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridTasks.MultiSelect = true;
            GridTasks.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            GridTasks.ShowCellToolTips = true;

            // Add columns to the tasks grid
            DataGridViewTextBoxColumn column = null;
            GridColumn Col;

            // ID
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.ID];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridTasks.Columns.Add(column);

            // Priority
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.Priority];
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 32,
                DisplayIndex = Col.DisplayIndex
            };
            GridTasks.Columns.Add(imageColumn);

            // Status
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.Status];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.DefaultCellStyle.Padding = new Padding(15, 0, 6, 0);
            GridTasks.Columns.Add(column);

            // Target version
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.TargetVersion];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridTasks.Columns.Add(column);

            // Summary
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.Summary];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                DisplayIndex = Col.DisplayIndex
            };
            GridTasks.Columns.Add(column);

            // Date created
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.DateCreated];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            GridTasks.Columns.Add(column);

            // Date modified
            Col = ApplicationSettings.GridTasksColumns[TaskFieldsUI.DateModified];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            GridTasks.Columns.Add(column);

            GridTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set each of the columns in programmatic sort mode.
            foreach (DataGridViewColumn c in GridTasks.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            initializingGridTasks = false;
        }

        /// <summary>
        /// Populate the tasks grid.
        /// </summary>
        private void PopulateGridTasks()
        {
            if ((Program.SoftwareProject != null) && (Program.SoftwareProject.Tasks != null))
            { 
                foreach (KeyValuePair<int, Task> item in Program.SoftwareProject.Tasks)
                {
                    AddTaskToGrid(item.Value);
                }
            }

            // Sort the contents according to the sort criteria
            GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));
        }

        /// <summary>
        /// Updates the visibility of the tasks grid columns, according to the current settings.
        /// </summary>
        private void UpdateColumnsVisibilityGridTasks()
        {
            GridTasks.SuspendLayout();

            foreach (KeyValuePair<TaskFieldsUI, GridColumn> item in ApplicationSettings.GridTasksColumns)
            {
                GridTasks.Columns[item.Value.Name].Visible = item.Value.Visible;
            }

            GridTasks.ResumeLayout();
        }

        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // ***** adaptar este método **

            if ((Program.SoftwareProject == null) || (Program.SoftwareProject.Tasks.Count == 0))
            {
                return;
            }

            int key = Convert.ToInt32(GridTasks["id", e.RowIndex].Value.ToString());

            // Text color of finished tasks
            if (Program.SoftwareProject.Tasks[key].Status == TaskStatus.Finished)
            {
                e.CellStyle.ForeColor = ApplicationSettings.GridClosedItem;
                e.CellStyle.SelectionForeColor = ApplicationSettings.GridClosedItem;
            }

            // Configure/format the "priority" column's cells
            if (GridTasks.Columns[e.ColumnIndex].Name == "priority")
            {
                // Test if the cell has no image assigned
                if (e.Value == null)
                {
                    // Assign a 1x1 bitmap so that a "missing image" icon is not displayed
                    e.Value = new Bitmap(1, 1);
                }
                else
                {
                    // Set the tooltip for this cell, but only if the project has tasks
                    if ((Program.SoftwareProject.Tasks == null) || (Program.SoftwareProject.Tasks.Count == 0))
                    {
                        return;
                    }

                    //int key = Convert.ToInt32(GridTasks["id", e.RowIndex].Value.ToString());
                    string s = string.Empty;

                    switch (Program.SoftwareProject.Tasks[key].Priority)
                    {
                        case TaskPriority.Low:
                            s = "Low priority";
                            break;

                        case TaskPriority.Normal:
                            s = "Normal priority";
                            break;

                        case TaskPriority.High:
                            s = "High priority";
                            break;

                        case TaskPriority.Urgent:
                            s = "Urgent priority";
                            break;

                        case TaskPriority.Immediate:
                            s = "Immediate priority";
                            break;
                    }

                    GridTasks[e.ColumnIndex, e.RowIndex].ToolTipText = s;
                }
            }
        }

        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridTasks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            e.PaintBackground(e.ClipBounds, true);
            e.PaintContent(e.ClipBounds);

            // Draw a filled rectangle in the "Status" column
            if (GridTasks.Columns[e.ColumnIndex].Name == "status")
            {
                int key = Convert.ToInt32(GridTasks["id", e.RowIndex].Value.ToString());
                Brush fillColor;

                // Set the color for the rectangle, according to the Task status
                fillColor = ApplicationSettings.TaskStatusColors[Program.SoftwareProject.Tasks[key].Status];

                if (fillColor != Brushes.Transparent)
                {
                    int size = ApplicationSettings.GridStatusRectangleSize;
                    Rectangle rect = new Rectangle(e.CellBounds.Location.X + 4, e.CellBounds.Location.Y + (e.CellBounds.Height / 2) - (size / 2), size, size);
                    e.Graphics.FillRectangle(fillColor, rect);
                    e.PaintContent(rect);
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// Returns a bitmap image corresponding to a given task priority.
        /// </summary>
        /// <param name="priority">A task priority</param>
        /// <returns>A bitmap image.</returns>
        private object GetTaskPriorityImage(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.High:
                    return MiniBug.Properties.Resources.Priority_High;

                case TaskPriority.Urgent:
                    return MiniBug.Properties.Resources.Priority_Urgent;

                case TaskPriority.Immediate:
                    return MiniBug.Properties.Resources.Priority_Immediate;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Add a new task to the grid.
        /// </summary>
        /// <param name="newTask">The task to add to the grid.</param>
        private void AddTaskToGrid(Task newTask)
        {
            GridTasks.Rows.Add(new object[] {
                newTask.ID.ToString(),
                GetTaskPriorityImage(newTask.Priority),
                newTask.Status.ToDescription(),
                newTask.TargetVersion,
                newTask.Summary,
                newTask.DateCreated.ToString("g"),
                newTask.DateModified.ToString("g")
            });
        }

        // ***** testar
        /// <summary>
        /// Refresh task's data in the tasks grid.
        /// </summary>
        /// <param name="rowIndex">The row index in the task grid, that holds the data for the task.</param>
        /// <param name="taskID">The id of the task (the task key in the collection of tasks).</param>
        private void RefreshTaskInGrid(int rowIndex, int taskID)
        {
            string key = string.Empty;

            key = ApplicationSettings.GridTasksColumns[TaskFieldsUI.Priority].Name;
            GridTasks.Rows[rowIndex].Cells[key].Value = GetTaskPriorityImage(Program.SoftwareProject.Tasks[taskID].Priority);

            key = ApplicationSettings.GridTasksColumns[TaskFieldsUI.Status].Name;
            GridTasks.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Tasks[taskID].Status.ToDescription();

            key = ApplicationSettings.GridTasksColumns[TaskFieldsUI.TargetVersion].Name;
            GridTasks.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Tasks[taskID].TargetVersion;

            key = ApplicationSettings.GridTasksColumns[TaskFieldsUI.Summary].Name;
            GridTasks.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Tasks[taskID].Summary;

            key = ApplicationSettings.GridTasksColumns[TaskFieldsUI.DateCreated].Name;
            GridTasks.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Tasks[taskID].DateCreated.ToString("g");

            key = ApplicationSettings.GridTasksColumns[TaskFieldsUI.DateModified].Name;
            GridTasks.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Tasks[taskID].DateModified.ToString("g");
        }

        /// <summary>
        /// Create a new task.
        /// </summary>
        private void NewTask()
        {
            TaskForm frmTask = new TaskForm(OperationType.New);

            if (frmTask.ShowDialog() == DialogResult.OK)
            {
                Program.SoftwareProject.AddTask(frmTask.CurrentTask);

                // Add the new task to the grid
                AddTaskToGrid(frmTask.CurrentTask);

                // Unselect all the previously selected rows
                foreach (DataGridViewRow row in GridTasks.SelectedRows)
                {
                    row.Selected = false;
                }

                // Select the last row (the one which was added)
                GridTasks.Rows[GridTasks.Rows.Count - 1].Selected = true;

                // Save the project file
                SaveProject();

                // Refresh the UI controls
                SetControlsState();

                // Sort the contents according to the sort criteria
                GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));
            }

            frmTask.Dispose();
        }

        /// <summary>
        /// Edit the selected task.
        /// </summary>
        private void EditTask()
        {
            if (GridTasks.SelectedRows.Count == 1)
            {
                // Get the key of the task in the selected row 
                int id = Int32.Parse(GridTasks.SelectedRows[0].Cells["id"].Value.ToString());

                TaskForm frmTask = new TaskForm(OperationType.Edit, Program.SoftwareProject.Tasks[id]);

                if (frmTask.ShowDialog() == DialogResult.OK)
                {
                    Program.SoftwareProject.Tasks[id] = frmTask.CurrentTask;

                    // Refresh the task information in the grid
                    RefreshTaskInGrid(GridTasks.SelectedRows[0].Index, id);

                    // Save the project file
                    SaveProject();

                    // Refresh the UI controls
                    SetControlsState();

                    // Sort the contents according to the sort criteria
                    GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));
                }

                frmTask.Dispose();
            }
        }

        /// <summary>
        /// Delete the selected tasks.
        /// </summary>
        private void DeleteTask()
        {
            if (GridTasks.SelectedRows.Count > 0)
            {
                string msg = (GridTasks.SelectedRows.Count == 1) ? string.Empty : "s";

                // Confirm this operation
                if (MessageBox.Show("Are you sure you want to delete the selected task" + msg + "?", "Delete Task" + msg, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    // Iterate all the selected rows in the grid
                    foreach (DataGridViewRow row in GridTasks.SelectedRows)
                    {
                        // Get the key of the task in the selected row 
                        int key = Int32.Parse(row.Cells["id"].Value.ToString());

                        // Get the index of the selected row
                        int i = row.Index;

                        // Remove the task from the collection
                        Program.SoftwareProject.Tasks.Remove(key);

                        // Remove the row from the grid
                        GridTasks.Rows.RemoveAt(i);
                    }

                    // Save the project file
                    SaveProject();

                    // Refresh the UI controls
                    SetControlsState();

                    // Sort the contents according to the sort criteria
                    GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));
                }
            }
        }

        /// <summary>
        /// Clone the selected task.
        /// </summary>
        private void CloneTask()
        {
            if (GridTasks.SelectedRows.Count == 1)
            {
                // Get the key of the task in the selected row 
                int id = Int32.Parse(GridTasks.SelectedRows[0].Cells["id"].Value.ToString());

                Task newTask = new Task();
                Program.SoftwareProject.Tasks[id].Clone(ref newTask);
                Program.SoftwareProject.AddTask(newTask);

                // Add the new task to the grid
                AddTaskToGrid(newTask);

                // Save the project file
                SaveProject();

                // Refresh the UI controls
                SetControlsState();

                // Sort the contents according to the sort criteria
                GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));
            }
        }

        /// <summary>
        /// Handle a double-click on the tasks grid: edit a task.
        /// </summary>
        private void GridTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditTask();
        }

        /// <summary>
        /// When the selection changes in the tasks grid, enable/disable certain icons.
        /// </summary>
        private void GridTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (GridTasks.SelectedRows.Count == 1)
            {
                IconEditTask.Enabled = true;
                IconCloneTask.Enabled = true;
            }
            else
            {
                IconEditTask.Enabled = false;
                IconCloneTask.Enabled = false;
            }
        }

        /// <summary>
        /// Delete the selected tasks when the user clicks the Delete key.
        /// </summary>
        private void GridTasks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteTask();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditTask();
            }
        }

        /// <summary>
        /// Handle changing the column order in the tasks grid.
        /// </summary>
        private void GridTasks_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (!this.initializingGridTasks)
            {
                // Get the Task instance with the specified column name
                GridColumn Col = ApplicationSettings.GridTasksColumns.Where(z => z.Value.Name == e.Column.Name).FirstOrDefault().Value;

                // Update the column order
                if (Col != null)
                {
                    Col.DisplayIndex = e.Column.DisplayIndex;
                }
            }
        }

        // *** em implementação ***
        private void GridTasksGetDisplayIndexForAll()
        {
            foreach (KeyValuePair<TaskFieldsUI, GridColumn> item in ApplicationSettings.GridTasksColumns)
            {
                item.Value.DisplayIndex = GridTasks.Columns[item.Value.Name].DisplayIndex;
            }
        }
        #endregion

        #region Issues
        /// <summary>
        /// Initialize the issues DataGridView.
        /// </summary>
        private void InitializeGridIssues()
        {
            initializingGridIssues = true;

            GridIssues.BackgroundColor = TabControl.DefaultBackColor;
            GridIssues.BorderStyle = BorderStyle.None;
            GridIssues.Dock = DockStyle.Fill;

            GridIssues.AllowUserToAddRows = false;
            GridIssues.AllowUserToDeleteRows = false;
            GridIssues.AllowUserToOrderColumns = true;
            GridIssues.AllowUserToResizeColumns = true;
            GridIssues.AllowUserToResizeRows = false;
            GridIssues.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            GridIssues.ColumnHeadersVisible = true;
            GridIssues.RowHeadersVisible = false;
            GridIssues.ReadOnly = true;
            GridIssues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridIssues.MultiSelect = true;
            GridIssues.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            GridIssues.ShowCellToolTips = true;

            GridIssues.AutoGenerateColumns = false;


            // Add columns to the issues grid
            DataGridViewTextBoxColumn column = null;
            GridColumn Col;

            // *** debug
            HelperClass.DebugDisplayIndex("4.1: MainForm.cs: dentro de InitializeGridIssues(): ApplicationSettings.GridIssuesColumns");

            // ID
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.ID];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//,
                //DisplayIndex = Col.DisplayIndex
            };
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns.Add(column);

            // Priority
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Priority];
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 32//,
                //DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(imageColumn);

            // Status
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Status];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//,
                //DisplayIndex = Col.DisplayIndex
            };
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.DefaultCellStyle.Padding = new Padding(15, 0, 6, 0);
            GridIssues.Columns.Add(column);

            // Version
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Version];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//,
                //DisplayIndex = Col.DisplayIndex
            };
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns.Add(column);

            // Target version
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.TargetVersion];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//,
                //DisplayIndex = Col.DisplayIndex
            };
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns.Add(column);

            // Summary
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Summary];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible//,
                //DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // Date created
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateCreated];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//,
                //DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // Date modified
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateModified];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells//,
                //DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // *** debug
            HelperClass.DebugDisplayIndex("4.2: MainForm.cs: dentro de InitializeGridIssues(): ApplicationSettings.GridIssuesColumns");

            GridIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // *** Set the display order of the columns
            /*foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                GridIssues.Columns[item.Value.Name].DisplayIndex = item.Value.DisplayIndex;
            }*/
            foreach (DataGridViewColumn dgvwColumn in GridIssues.Columns)
            {
                GridColumn issueCol = ApplicationSettings.GridIssuesColumns.Where(z => z.Value.Name == dgvwColumn.Name).FirstOrDefault().Value;
                dgvwColumn.DisplayIndex = issueCol.DisplayIndex;
            }
            // ***

            // *** debug
            HelperClass.DebugDisplayIndex("4.3: MainForm.cs: dentro de InitializeGridIssues(): ApplicationSettings.GridIssuesColumns");


            // Set each of the columns in programmatic sort mode.
            foreach (DataGridViewColumn c in GridIssues.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            // *** debug
            HelperClass.DebugDisplayIndex("4.4: MainForm.cs: dentro de InitializeGridIssues(): ApplicationSettings.GridIssuesColumns");

            initializingGridIssues = false;
        }

        /// <summary>
        /// Populate the issues grid.
        /// </summary>
        private void PopulateGridIssues()
        {
            if ((Program.SoftwareProject != null) && (Program.SoftwareProject.Issues != null))
            {
                foreach (KeyValuePair<int, Issue> item in Program.SoftwareProject.Issues)
                {
                    AddIssueToGrid(item.Value);
                }
            }

            // Sort the contents according to the sort criteria
            GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));
        }

        /// <summary>
        /// Updates the visibility of the issues grid columns, according to the current settings.
        /// </summary>
        private void UpdateColumnsVisibilityGridIssues()
        {
            GridIssues.SuspendLayout();

            foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                GridIssues.Columns[item.Value.Name].Visible = item.Value.Visible;
            }

            GridIssues.ResumeLayout();
        }

        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridIssues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // ***** adaptar este método **

            if ((Program.SoftwareProject == null) || (Program.SoftwareProject.Issues.Count == 0))
            {
                return;
            }

            int key = Convert.ToInt32(GridIssues["id", e.RowIndex].Value.ToString());

            // Text color of closed issues
            if (Program.SoftwareProject.Issues[key].Status == IssueStatus.Closed)
            {
                e.CellStyle.ForeColor = ApplicationSettings.GridClosedItem;
                e.CellStyle.SelectionForeColor = ApplicationSettings.GridClosedItem;
            }

            // Configure/format the "priority" column's cells
            if (GridIssues.Columns[e.ColumnIndex].Name == "priority")
            {
                // Test if the cell has no image assigned
                if (e.Value == null)
                {
                    // Assign a 1x1 bitmap so that a "missing image" icon is not displayed
                    e.Value = new Bitmap(1, 1);

                    // Assign an empty tooltip
                    GridIssues[e.ColumnIndex, e.RowIndex].ToolTipText = string.Empty;
                }
                else
                {
                    // Set the tooltip for this cell, but only if the project has issues
                    if ((Program.SoftwareProject.Issues == null) || (Program.SoftwareProject.Issues.Count == 0))
                    {
                        return;
                    }

                    string s = string.Empty;

                    switch (Program.SoftwareProject.Issues[key].Priority)
                    {
                        case IssuePriority.Low:
                            s = "Low priority";
                            break;

                        case IssuePriority.Normal:
                            s = "Normal priority";
                            break;

                        case IssuePriority.High:
                            s = "High priority";
                            break;

                        case IssuePriority.Urgent:
                            s = "Urgent priority";
                            break;

                        case IssuePriority.Immediate:
                            s = "Immediate priority";
                            break;
                    }

                    GridIssues[e.ColumnIndex, e.RowIndex].ToolTipText = s;
                }
            }
        }

        // ***** atualizar este método
        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridIssues_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            e.PaintBackground(e.ClipBounds, true);
            e.PaintContent(e.ClipBounds);

            // Draw a filled rectangle in the "Status" column
            if (GridIssues.Columns[e.ColumnIndex].Name == "status")
            {
                int key = Convert.ToInt32(GridIssues["id", e.RowIndex].Value.ToString());
                Brush fillColor;

                // Set the color for the rectangle, according to the Issue status
                fillColor = ApplicationSettings.IssueStatusColors[Program.SoftwareProject.Issues[key].Status];

                if (fillColor != Brushes.Transparent)
                {
                    int size = ApplicationSettings.GridStatusRectangleSize;
                    Rectangle rect = new Rectangle(e.CellBounds.Location.X + 4, e.CellBounds.Location.Y + (e.CellBounds.Height / 2) - (size / 2), size, size);
                    e.Graphics.FillRectangle(fillColor, rect);
                    e.PaintContent(rect);
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// Returns a bitmap image corresponding to a given issue priority.
        /// </summary>
        /// <param name="priority">An issue priority</param>
        /// <returns>A bitmap image.</returns>
        private object GetIssuePriorityImage(IssuePriority priority)
        {
            switch (priority)
            {
                case IssuePriority.High:
                    return MiniBug.Properties.Resources.Priority_High;

                case IssuePriority.Urgent:
                    return MiniBug.Properties.Resources.Priority_Urgent;

                case IssuePriority.Immediate:
                    return MiniBug.Properties.Resources.Priority_Immediate;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Add a new issue to the grid.
        /// </summary>
        /// <param name="newIssue">The issue to add to the grid.</param>
        private void AddIssueToGrid(Issue newIssue)
        {
            GridIssues.Rows.Add(new object[] {
                newIssue.ID.ToString(),
                GetIssuePriorityImage(newIssue.Priority),
                newIssue.Status.ToDescription(),
                newIssue.Version,
                newIssue.TargetVersion,
                newIssue.Summary,
                newIssue.DateCreated.ToString("g"),
                newIssue.DateModified.ToString("g")
            });
        }

        /// <summary>
        /// Refresh an issue's data in the issues grid.
        /// </summary>
        /// <param name="rowIndex">The row index in the issue grid, that holds the data for the issue.</param>
        /// <param name="issueID">The id of the issue (the issue key in the collection of issues).</param>
        private void RefreshIssueInGrid(int rowIndex, int issueID)
        {
            string key = string.Empty;

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Priority].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = GetIssuePriorityImage(Program.SoftwareProject.Issues[issueID].Priority);

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Status].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].Status.ToDescription();

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Version].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].Version;

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.TargetVersion].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].TargetVersion;

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Summary].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].Summary;

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateCreated].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].DateCreated.ToString("g");

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateModified].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].DateModified.ToString("g");
        }

        /// <summary>
        /// Create a new issue.
        /// </summary>
        private void NewIssue()
        {
            IssueForm frmIssue = new IssueForm(OperationType.New);

            if (frmIssue.ShowDialog() == DialogResult.OK)
            {
                Program.SoftwareProject.AddIssue(frmIssue.CurrentIssue);

                // Add the new issue to the grid
                AddIssueToGrid(frmIssue.CurrentIssue);

                // Unselect all the previously selected rows
                foreach (DataGridViewRow row in GridIssues.SelectedRows)
                {
                    row.Selected = false;
                }

                // Select the last row (the one which was added)
                GridIssues.Rows[GridIssues.Rows.Count - 1].Selected = true;

                // Save the project file
                SaveProject();

                // Refresh the UI controls
                SetControlsState();

                // Sort the contents according to the sort criteria
                GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));
            }

            frmIssue.Dispose();
        }

        /// <summary>
        /// Edit the selected issue.
        /// </summary>
        private void EditIssue()
        {
            if (GridIssues.SelectedRows.Count == 1)
            {
                // Get the key of the issue in the selected row 
                int id = Int32.Parse(GridIssues.SelectedRows[0].Cells["id"].Value.ToString());

                IssueForm frmIssue = new IssueForm(OperationType.Edit, Program.SoftwareProject.Issues[id]);

                if (frmIssue.ShowDialog() == DialogResult.OK)
                {
                    Program.SoftwareProject.Issues[id] = frmIssue.CurrentIssue;

                    // Refresh the issue information in the grid
                    RefreshIssueInGrid(GridIssues.SelectedRows[0].Index, id);

                    // Save the project file
                    SaveProject();

                    // Refresh the UI controls
                    SetControlsState();

                    // Sort the contents according to the sort criteria
                    GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));
                }

                frmIssue.Dispose();
            }
        }

        /// <summary>
        /// Delete the selected issues.
        /// </summary>
        private void DeleteIssue()
        {
            if (GridIssues.SelectedRows.Count > 0)
            {
                string msg = (GridIssues.SelectedRows.Count == 1) ? string.Empty : "s";

                // Confirm this operation
                if (MessageBox.Show("Are you sure you want to delete the selected issue" + msg + "?", "Delete Issue" + msg, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    // Iterate all the selected rows in the grid
                    foreach (DataGridViewRow row in GridIssues.SelectedRows)
                    {
                        // Get the key of the issue in the selected row 
                        int key = Int32.Parse(row.Cells["id"].Value.ToString());

                        // Get the index of the selected row
                        int i = row.Index;

                        // Remove the issue from the collection
                        Program.SoftwareProject.Issues.Remove(key);

                        // Remove the row from the grid
                        GridIssues.Rows.RemoveAt(i);
                    }

                    // Save the project file
                    SaveProject();

                    // Refresh the UI controls
                    SetControlsState();

                    // Sort the contents according to the sort criteria
                    GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));
                }
            }
        }

        /// <summary>
        /// Clone the selected issue.
        /// </summary>
        private void CloneIssue()
        {
            if (GridIssues.SelectedRows.Count == 1)
            {
                // Get the key of the issue in the selected row 
                int id = Int32.Parse(GridIssues.SelectedRows[0].Cells["id"].Value.ToString());

                Issue newIssue = new Issue();
                Program.SoftwareProject.Issues[id].Clone(ref newIssue);
                Program.SoftwareProject.AddIssue(newIssue);

                // Add the new issue to the grid
                AddIssueToGrid(newIssue);

                // Save the project file
                SaveProject();

                // Refresh the UI controls
                SetControlsState();

                // Sort the contents according to the sort criteria
                GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));
            }
        }

        /// <summary>
        /// Handle a double-click on the issues grid: edit an issue.
        /// </summary>
        private void GridIssues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditIssue();
        }

        /// <summary>
        /// When the selection changes in the issues grid, enable/disable certain icons.
        /// </summary>
        private void GridIssues_SelectionChanged(object sender, EventArgs e)
        {
            if (GridIssues.SelectedRows.Count == 1)
            {
                IconEditIssue.Enabled = true;
                IconCloneIssue.Enabled = true;
            }
            else
            {
                IconEditIssue.Enabled = false;
                IconCloneIssue.Enabled = false;
            }
        }

        /// <summary>
        /// Delete the selected issues when the user clicks the Delete key.
        /// </summary>
        private void GridIssues_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteIssue();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditIssue();
            }
        }

        /// <summary>
        /// Handle changing the column order in the issues grid.
        /// </summary>
        private void GridIssues_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (!this.initializingGridIssues)
            {
                // Get the Issue instance with the specified column name
                GridColumn Col = ApplicationSettings.GridIssuesColumns.Where(z => z.Value.Name == e.Column.Name).FirstOrDefault().Value;

                // Update the column order
                if (Col != null)
                {
                    Col.DisplayIndex = e.Column.DisplayIndex;
                }
            }
        }
        #endregion

        /// <summary>
        /// **** em desenvolvimento ****
        /// </summary>
        private void IconConfigureView_Click(object sender, EventArgs e)
        {
            // Store the current sort settings
            GridIssuesSortSettings GridIssuesSortOld = new GridIssuesSortSettings(ApplicationSettings.GridIssuesSort.FirstColumn, ApplicationSettings.GridIssuesSort.FirstColumnSortOrder, ApplicationSettings.GridIssuesSort.SecondColumn, ApplicationSettings.GridIssuesSort.SecondColumnSortOrder);
            GridTasksSortSettings GridTasksSortOld = new GridTasksSortSettings(ApplicationSettings.GridTasksSort.FirstColumn, ApplicationSettings.GridTasksSort.FirstColumnSortOrder, ApplicationSettings.GridTasksSort.SecondColumn, ApplicationSettings.GridTasksSort.SecondColumnSortOrder);

            ConfigureViewForm frmConfigureView = new ConfigureViewForm();

            if (frmConfigureView.ShowDialog() == DialogResult.OK)
            {
                // Apply the new visibility settings
                UpdateColumnsVisibilityGridIssues();
                UpdateColumnsVisibilityGridTasks();

                ApplicationSettings.Save(ApplicationSettings.SaveSettings.ColumnOrderSort);

                // Apply the new sort settings, if there were changes
                if (!GridIssuesSortOld.Equals(ApplicationSettings.GridIssuesSort))
                {
                    // Remove the sort glyph from the old sort columns
                    RemoveGridSortGlyph(GridType.Issues, GridIssuesSortOld, null);

                    GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));

                    // Set the sort glyph
                    SetGridSortGlyph(GridType.Issues);
                }

                // Apply the new sort settings, if there were changes
                if (!GridTasksSortOld.Equals(ApplicationSettings.GridTasksSort))
                {
                    // Remove the sort glyph from the old sort columns
                    RemoveGridSortGlyph(GridType.Tasks, null, GridTasksSortOld);

                    GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));

                    // Set the sort glyph
                    SetGridSortGlyph(GridType.Tasks);
                }
            }

            frmConfigureView.Dispose();            
        }

        /// <summary>
        /// Handle clicks on the header cells in the issues DataGridView: sort by the clicked column.
        /// </summary>
        private void GridIssues_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Is the clicked column header the first sort column?
            if (GridIssues.Columns[e.ColumnIndex].Name == ApplicationSettings.GridIssuesColumns[ApplicationSettings.GridIssuesSort.FirstColumn].Name)
            {
                // Reverse the sort order
                if (ApplicationSettings.GridIssuesSort.FirstColumnSortOrder == SortOrder.Ascending)
                {
                    ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = SortOrder.Descending;
                }
                else
                {
                    ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = SortOrder.Ascending;
                }

                // Remove the sort glyph from the second sort column (if it is not null)
                if (ApplicationSettings.GridIssuesSort.SecondColumn != null)
                {
                    GridIssues.Columns[ApplicationSettings.GridIssuesColumns[ApplicationSettings.GridIssuesSort.SecondColumn.Value].Name].HeaderCell.SortGlyphDirection = SortOrder.None;
                }
                // Set the second sort column to null
                ApplicationSettings.GridIssuesSort.SecondColumn = null;
                ApplicationSettings.GridIssuesSort.SecondColumnSortOrder = null;
            }
            else if ((ApplicationSettings.GridIssuesSort.SecondColumn != null) && (GridIssues.Columns[e.ColumnIndex].Name == ApplicationSettings.GridIssuesColumns[ApplicationSettings.GridIssuesSort.SecondColumn.Value].Name))
            {
                // Is the clicked column header the second sort column?

                // Remove the sort glyph from the first sort column
                GridIssues.Columns[ApplicationSettings.GridIssuesColumns[ApplicationSettings.GridIssuesSort.FirstColumn].Name].HeaderCell.SortGlyphDirection = SortOrder.None;

                // This is now the first sort column
                ApplicationSettings.GridIssuesSort.FirstColumn = ApplicationSettings.GridIssuesSort.SecondColumn.Value;
                // Reverse the sort order
                ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = (ApplicationSettings.GridIssuesSort.SecondColumnSortOrder.Value == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;

                // Set the second sort column to null
                ApplicationSettings.GridIssuesSort.SecondColumn = null;
                ApplicationSettings.GridIssuesSort.SecondColumnSortOrder = null;
            }
            else
            {
                // Remove the sort glyph from the old sort columns
                RemoveGridSortGlyph(GridType.Issues, ApplicationSettings.GridIssuesSort, null);

                // Get the issue field with the specified column name
                IssueFieldsUI column = ApplicationSettings.GridIssuesColumns.Where(z => z.Value.Name == GridIssues.Columns[e.ColumnIndex].Name).FirstOrDefault().Key;

                // Set the new sort settings
                ApplicationSettings.GridIssuesSort.FirstColumn = column;
                ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = SortOrder.Ascending;
                ApplicationSettings.GridIssuesSort.SecondColumn = null;
                ApplicationSettings.GridIssuesSort.SecondColumnSortOrder = null;
            }

            GridIssues.Sort(new IssuesDataGridViewRowComparer(SortOrder.Ascending));

            // Set the sort glyph
            SetGridSortGlyph(GridType.Issues);
        }

        /// <summary>
        /// Handle clicks on the header cells in the tasks DataGridView: sort by the clicked column.
        /// </summary>
        private void GridTasks_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Is the clicked column header the first sort column?
            if (GridTasks.Columns[e.ColumnIndex].Name == ApplicationSettings.GridTasksColumns[ApplicationSettings.GridTasksSort.FirstColumn].Name)
            {
                // Reverse the sort order
                if (ApplicationSettings.GridTasksSort.FirstColumnSortOrder == SortOrder.Ascending)
                {
                    ApplicationSettings.GridTasksSort.FirstColumnSortOrder = SortOrder.Descending;
                }
                else
                {
                    ApplicationSettings.GridTasksSort.FirstColumnSortOrder = SortOrder.Ascending;
                }

                // Remove the sort glyph from the second sort column (if it is not null)
                if (ApplicationSettings.GridTasksSort.SecondColumn != null)
                {
                    GridTasks.Columns[ApplicationSettings.GridTasksColumns[ApplicationSettings.GridTasksSort.SecondColumn.Value].Name].HeaderCell.SortGlyphDirection = SortOrder.None;
                }
                // Set the second sort column to null
                ApplicationSettings.GridTasksSort.SecondColumn = null;
                ApplicationSettings.GridTasksSort.SecondColumnSortOrder = null;
            }
            else if ((ApplicationSettings.GridTasksSort.SecondColumn != null) && (GridTasks.Columns[e.ColumnIndex].Name == ApplicationSettings.GridTasksColumns[ApplicationSettings.GridTasksSort.SecondColumn.Value].Name))
            {
                // Is the clicked column header the second sort column?

                // Remove the sort glyph from the first sort column
                GridTasks.Columns[ApplicationSettings.GridTasksColumns[ApplicationSettings.GridTasksSort.FirstColumn].Name].HeaderCell.SortGlyphDirection = SortOrder.None;

                // This is now the first sort column
                ApplicationSettings.GridTasksSort.FirstColumn = ApplicationSettings.GridTasksSort.SecondColumn.Value;
                // Reverse the sort order
                ApplicationSettings.GridTasksSort.FirstColumnSortOrder = (ApplicationSettings.GridTasksSort.SecondColumnSortOrder.Value == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;

                // Set the second sort column to null
                ApplicationSettings.GridTasksSort.SecondColumn = null;
                ApplicationSettings.GridTasksSort.SecondColumnSortOrder = null;
            }
            else
            {
                // Remove the sort glyph from the old sort columns
                RemoveGridSortGlyph(GridType.Tasks, null, ApplicationSettings.GridTasksSort);

                // Get the task field with the specified column name
                TaskFieldsUI column = ApplicationSettings.GridTasksColumns.Where(z => z.Value.Name == GridTasks.Columns[e.ColumnIndex].Name).FirstOrDefault().Key;

                // Set the new sort settings
                ApplicationSettings.GridTasksSort.FirstColumn = column;
                ApplicationSettings.GridTasksSort.FirstColumnSortOrder = SortOrder.Ascending;
                ApplicationSettings.GridTasksSort.SecondColumn = null;
                ApplicationSettings.GridTasksSort.SecondColumnSortOrder = null;
            }

            GridTasks.Sort(new TasksDataGridViewRowComparer(SortOrder.Ascending));

            // Set the sort glyph
            SetGridSortGlyph(GridType.Tasks);
        }
    }
}
