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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Start by retrieving the application settings
            ApplicationSettings.Load();

            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.Icon = MiniBug.Properties.Resources.Minibug;
            this.Text = "MiniBug Issue Tracker";
            this.MinimumSize = new Size(478, 303);

            InitializeTabControl();

            // Initialization of the Issues and Tasks grids
            InitializeGridIssues();
            InitializeGridTasks();

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


            // *** remover *** para testar feedback de exportação ***
            /*ExportProjectResult Result = new ExportProjectResult(false, "ficheiro-issues.csv", false, "ficheiro-tasks.csv", FileSystemOperationStatus.ExportToCsvIOError, FileSystemOperationStatus.ExportToCsvErrorPathTooLong);
            ExportFeedbackForm frmFeedback = new ExportFeedbackForm(Result);
            frmFeedback.ShowDialog();
            frmFeedback.Dispose();*/
            // ****

            // **** testar sort ****
            /*ApplicationSettings.GridIssuesSort.FirstColumn = IssueFieldsUI.ID;
            ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = SortOrder.Ascending;
            ApplicationSettings.GridIssuesSort.SecondColumn= IssueFieldsUI.Status;
            ApplicationSettings.GridIssuesSort.SecondColumnSortOrder = SortOrder.Descending;*/
            // ********************
        }

        /// <summary>
        /// Initialize the recent projects submenu.
        /// </summary>
        private void InitializeRecentProjects()
        {
            // Initialize the settings for recent projects
            if ((Properties.Settings.Default.RecentProjectsNames == null) || (Properties.Settings.Default.RecentProjectsPaths == null))
            {
                Properties.Settings.Default.RecentProjectsNames = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.RecentProjectsPaths = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();

                // Disable the recent projects menu item
                recentProjectsToolStripMenuItem.Enabled = false;
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
        }

        /// <summary>
        /// Initialize the tab control.
        /// </summary>
        private void InitializeTabControl()
        {
            TabControl.Left = this.ClientRectangle.Left;
            TabControl.Width = this.ClientRectangle.Width + 3;
            TabControl.Height = this.ClientRectangle.Height - toolStrip1.Height;
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
            // Save the order of the columns in the issues and tasks DataGridViews
            ApplicationSettings.Save(ApplicationSettings.SaveSettings.ColumnOrderSort);
        }

        #region RecentProject
        /// <summary>
        /// Add a project to the recent projects submenu and to the application settings
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="filename">The path and name of the file.</param>
        private void AddRecentProject(string projectName, string filename)
        {
            recentProjectsToolStripMenuItem.Enabled = true;

            ToolStripMenuItem item = new ToolStripMenuItem(projectName)
            {
                Tag = filename
            };
            
            // Remove the last item in the menu when the maximum number of items is reached
            if (recentProjectsToolStripMenuItem.DropDownItems.Count == ApplicationSettings.MaxRecentProjects)
            {
                recentProjectsToolStripMenuItem.DropDownItems.RemoveAt(recentProjectsToolStripMenuItem.DropDownItems.Count - 1);

                Properties.Settings.Default.RecentProjectsNames.RemoveAt(Properties.Settings.Default.RecentProjectsNames.Count - 1);
                Properties.Settings.Default.RecentProjectsPaths.RemoveAt(Properties.Settings.Default.RecentProjectsPaths.Count - 1);
            }

            // Add the new menu item to the top of the submenu
            recentProjectsToolStripMenuItem.DropDownItems.Insert(0, item);

            // Save the project name and path and filename in the application settings
            Properties.Settings.Default.RecentProjectsNames.Insert(0, projectName);
            Properties.Settings.Default.RecentProjectsPaths.Insert(0, filename);
            Properties.Settings.Default.Save();

            // Add an event handler for the new menu item
            item.Click += new System.EventHandler(this.FileMenuRecentProjectItem_Click);
        }

        /// <summary>
        /// Occurs when one the recent projects menu item is clicked.
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
                GridTasks.Rows.Clear();

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

                    // Suspend the layout logic for the form, while the application is initializing
                    this.SuspendLayout();

                    // Set the main form title bar text
                    this.Text = $"{Program.SoftwareProject.Name} - MiniBug Issue Tracker";

                    // Clear the issues and tasks grids
                    GridIssues.Rows.Clear();
                    GridTasks.Rows.Clear();

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
            //ExportProjectResult Result = ApplicationData.ExportProject(frmExport.IssuesFilename, frmExport.TasksFilename, Program.SoftwareProject);
            ExportProjectResult Result = Program.SoftwareProject.Export(frmExport.IssuesFilename, frmExport.TasksFilename);

            // Show feedback about the project export operation
            ExportFeedbackForm frmFeedback = new ExportFeedbackForm(Result);
            frmFeedback.ShowDialog();
            frmFeedback.Dispose();

                /*FileSystemOperationStatus status = ApplicationData.ExportProject(frmExport.IssuesFilename, frmExport.TasksFilename, Program.SoftwareProject);

                if (status == FileSystemOperationStatus.ExportOK)
                {
                    MessageBox.Show("The export operation was successfull.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowProjectErrorFeedback(status);
                }*/
            

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


        #region Tasks
        /// <summary>
        /// Initialize the tasks DataGridView.
        /// </summary>
        private void InitializeGridTasks()
        {
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
            GridTasks.Columns.Add("id", "ID");
            GridTasks.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridTasks.Columns["id"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridTasks.Columns["id"].Resizable = DataGridViewTriState.False;
            GridTasks.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "priority",
                HeaderText = string.Empty,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 32
            };
            GridTasks.Columns.Add(imageColumn);

            GridTasks.Columns.Add("status", "Status");
            GridTasks.Columns["status"].Resizable = DataGridViewTriState.False;
            GridTasks.Columns["status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridTasks.Columns["status"].DefaultCellStyle.Padding = new Padding(15, 0, 6, 0);

            GridTasks.Columns.Add("target", "Target"); // novo
            GridTasks.Columns["target"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridTasks.Columns["target"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridTasks.Columns["target"].Resizable = DataGridViewTriState.False;
            GridTasks.Columns["target"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridTasks.Columns["target"].HeaderCell.ToolTipText = "Target Version";

            GridTasks.Columns.Add("summary", "Summary");
            GridTasks.Columns.Add("created", "Created");
            GridTasks.Columns["created"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            GridTasks.Columns["priority"].DisplayIndex = 0;
            GridTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
        }

        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
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
            GridTasks.Rows.Add(new object[] { newTask.ID.ToString(), GetTaskPriorityImage(newTask.Priority), newTask.Status.ToDescription(), newTask.TargetVersion, newTask.Summary, newTask.DateCreated.ToString() });
        }

        /// <summary>
        /// Refresh task's data in the tasks grid.
        /// </summary>
        /// <param name="rowIndex">The row index in the task grid, that holds the data for the task.</param>
        /// <param name="taskID">The id of the task (the task key in the collection of tasks).</param>
        private void RefreshTaskInGrid(int rowIndex, int taskID)
        {
            GridTasks.Rows[rowIndex].Cells["priority"].Value = GetTaskPriorityImage(Program.SoftwareProject.Tasks[taskID].Priority);
            GridTasks.Rows[rowIndex].Cells["status"].Value = Program.SoftwareProject.Tasks[taskID].Status.ToDescription();
            GridTasks.Rows[rowIndex].Cells["target"].Value = Program.SoftwareProject.Tasks[taskID].TargetVersion;
            GridTasks.Rows[rowIndex].Cells["summary"].Value = Program.SoftwareProject.Tasks[taskID].Summary;
            GridTasks.Rows[rowIndex].Cells["created"].Value = Program.SoftwareProject.Tasks[taskID].DateCreated.ToString();
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
        #endregion

        #region Issues
        /// <summary>
        /// Initialize the issues DataGridView.
        /// </summary>
        private void InitializeGridIssues()
        {
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

            // Add columns to the issues grid
            DataGridViewTextBoxColumn column = null;
            GridColumn Col;

            // ID
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.ID];
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
                MinimumWidth = 32,
                DisplayIndex = Col.DisplayIndex
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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.Padding = new Padding(15, 0, 6, 0);
            GridIssues.Columns.Add(column);

            // Version
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Version];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // Target version
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.TargetVersion];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // Summary
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Summary];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // Date created
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateCreated];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);

            // Date modified
            Col = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateModified];
            column = new DataGridViewTextBoxColumn
            {
                Name = Col.Name,
                HeaderText = Col.HeaderText,
                Visible = Col.Visible,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DisplayIndex = Col.DisplayIndex
            };
            GridIssues.Columns.Add(column);
            

            /* *** VERSÂO ANTIGA *** remover
            // Add columns to the issues grid
            GridIssues.Columns.Add("id", "ID");
            GridIssues.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns["id"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns["id"].Resizable = DataGridViewTriState.False;
            GridIssues.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn {
                Name = "priority",
                HeaderText = string.Empty,
                Resizable = DataGridViewTriState.False,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 32               
            };
            GridIssues.Columns.Add(imageColumn);

            GridIssues.Columns.Add("status", "Status");
            GridIssues.Columns["status"].Resizable = DataGridViewTriState.False;
            GridIssues.Columns["status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridIssues.Columns["status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GridIssues.Columns["status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns["status"].DefaultCellStyle.Padding = new Padding(15, 0, 6, 0);

            GridIssues.Columns.Add("version", "Version");
            GridIssues.Columns["version"].Resizable = DataGridViewTriState.False;
            GridIssues.Columns["version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            GridIssues.Columns.Add("summary", "Summary");

            GridIssues.Columns.Add("created", "Created");
            GridIssues.Columns["created"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            */

            GridIssues.Columns["priority"].DisplayIndex = 0;
            GridIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
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
                return;

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
                newIssue.DateCreated.ToString(),
                newIssue.DateModified.ToString()
            });

            // Versão antiga (que funciona) **** remover ****
            //GridIssues.Rows.Add(new object[] { newIssue.ID.ToString(), GetIssuePriorityImage(newIssue.Priority), newIssue.Status.ToDescription(), newIssue.Version, newIssue.Summary, newIssue.DateCreated.ToString() });
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
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].DateCreated.ToString();

            key = ApplicationSettings.GridIssuesColumns[IssueFieldsUI.DateModified].Name;
            GridIssues.Rows[rowIndex].Cells[key].Value = Program.SoftwareProject.Issues[issueID].DateModified;


            // *** remover
            /*GridIssues.Rows[rowIndex].Cells["priority"].Value = GetIssuePriorityImage(Program.SoftwareProject.Issues[issueID].Priority);
            GridIssues.Rows[rowIndex].Cells["status"].Value = Program.SoftwareProject.Issues[issueID].Status.ToDescription();
            GridIssues.Rows[rowIndex].Cells["version"].Value = Program.SoftwareProject.Issues[issueID].Version;
            GridIssues.Rows[rowIndex].Cells["target"].Value = Program.SoftwareProject.Issues[issueID].TargetVersion;
            GridIssues.Rows[rowIndex].Cells["summary"].Value = Program.SoftwareProject.Issues[issueID].Summary;
            GridIssues.Rows[rowIndex].Cells["created"].Value = Program.SoftwareProject.Issues[issueID].DateCreated.ToString();
            GridIssues.Rows[rowIndex].Cells["modified"].Value = Program.SoftwareProject.Issues[issueID].DateModified;
            */
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
            GridColumn Col = ApplicationSettings.GridIssuesColumns.Where(z => z.Value.Name == e.Column.Name).FirstOrDefault().Value;

            // Update the column order
            if (Col != null)
            {
                Col.DisplayIndex = e.Column.DisplayIndex;
            }
        }
        #endregion

        /// <summary>
        /// **** em desenvolvimento ****
        /// </summary>
        private void IconConfigureView_Click(object sender, EventArgs e)
        {
            // *** experiência sort
            //GridIssues.Sort(GridIssues.Columns["summary"], ListSortDirection.Descending);
            //GridTasks.Sort(GridTasks.Columns["status"], ListSortDirection.Ascending);

            ConfigureViewForm frmConfigureView = new ConfigureViewForm();

            if (frmConfigureView.ShowDialog() == DialogResult.OK)
            {
                UpdateColumnsVisibilityGridIssues();
                ApplicationSettings.Save(ApplicationSettings.SaveSettings.ColumnOrderSort);
            }

            frmConfigureView.Dispose();
        }
    }
}
