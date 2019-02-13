using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.Icon = MiniBug.Properties.Resources.Minibug;
            this.Text = "New Project - MiniBug Issue Tracker";
            
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            Program.SoftwareProject = new MiniBug.Project("FlexBox Simulator");
            Program.SoftwareProject.Filename = "MINIBUG-FlexBoxSimulator.json";

            // *** temporário
            //Program.SoftwareProject.Issues.Add(1, new Issue(1, IssueStatus.Verified, IssuePriority.Normal, "Código CSS gerado: inicialização não está a obter valor do color picker do background do container", string.Empty, "0.1", "1.0.0"));
            //Program.SoftwareProject.Issues.Add(2, new Issue(2, IssueStatus.Verified, IssuePriority.Low, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.AddIssue(new Issue(IssueStatus.Unconfirmed, IssuePriority.Immediate, "Código CSS gerado: inicialização não está a obter valor do color picker do background do container", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.AddIssue(new Issue(IssueStatus.Confirmed, IssuePriority.Urgent, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.AddIssue(new Issue(IssueStatus.InProgress, IssuePriority.High, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.AddIssue(new Issue(IssueStatus.Resolved, IssuePriority.Normal, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.AddIssue(new Issue(IssueStatus.Closed, IssuePriority.Normal, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));

            Program.SoftwareProject.AddTask(new Task(TaskStatus.NotStarted, TaskPriority.High, "Permitir trabalhar com vários projetos em simultâneo", "...", "2.0"));
            Program.SoftwareProject.AddTask(new Task(TaskStatus.NotStarted, TaskPriority.Immediate, "Gravar settings da aplicação", "...", "1.0"));

            InitializeTabControl();
            InitializeGridIssues();
            PopulateGridIssues();
            InitializeGridTasks();
            PopulateGridTasks();
            InitializeToolbarIcons();

            // Resume the layout logic
            this.ResumeLayout();

            // experiência***
            //Program.SoftwareProject.Save();
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
        /// Initialize the toolbar icons: set the initial state.
        /// </summary>
        private void InitializeToolbarIcons()
        {
            IconNewTask.Enabled = false;
            IconEditTask.Enabled = false;
            IconDeleteTask.Enabled = false;
            IconCloneTask.Enabled = false;
        }

        /// <summary>
        /// Enable/disable icons when the user changes tabs.
        /// </summary>
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                IconNewIssue.Enabled = true;
                IconEditIssue.Enabled = true;
                IconDeleteIssue.Enabled = true;
                IconCloneIssue.Enabled = true;

                IconNewTask.Enabled = false;
                IconEditTask.Enabled = false;
                IconDeleteTask.Enabled = false;
                IconCloneTask.Enabled = false;
            }
            else if (TabControl.SelectedIndex == 1)
            {
                IconNewIssue.Enabled = false;
                IconEditIssue.Enabled = false;
                IconDeleteIssue.Enabled = false;
                IconCloneIssue.Enabled = false;

                IconNewTask.Enabled = true;
                IconEditTask.Enabled = true;
                IconDeleteTask.Enabled = true;
                IconCloneTask.Enabled = true;
            }
        }

        #region Projeto
        private void NewProject()
        {
            ProjectForm frmProject = new ProjectForm(OperationType.New);

            if (frmProject.ShowDialog() == DialogResult.OK)
            {
                // ****
            }

            frmProject.Dispose();

            //TaskForm frmTask = new TaskForm(OperationType.New);

            //if (frmTask.ShowDialog() == DialogResult.OK)
            //{
            //    Program.SoftwareProject.AddTask(frmTask.CurrentTask);

            //    Add the new task to the grid
            //    AddTaskToGrid(frmTask.CurrentTask);

            //    Unselect all the previously selected rows
            //    foreach (DataGridViewRow row in GridTasks.SelectedRows)
            //    {
            //        row.Selected = false;
            //    }

            //    Select the last row(the one which was added)
            //    GridTasks.Rows[GridTasks.Rows.Count - 1].Selected = true;
            //}

            //frmTask.Dispose();
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
        /// Exit this application.
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

        #region Tasks
        private void InitializeGridTasks()
        {
            GridTasks.BackgroundColor = TabControl.DefaultBackColor;
            GridTasks.BorderStyle = BorderStyle.None;
            GridTasks.Dock = DockStyle.Fill;

            GridTasks.AllowUserToAddRows = false;
            GridTasks.AllowUserToOrderColumns = true;
            GridTasks.AllowUserToResizeColumns = true;
            GridTasks.AllowUserToResizeRows = false;
            GridTasks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            GridTasks.ColumnHeadersVisible = true;
            GridTasks.RowHeadersVisible = false;
            GridTasks.GridColor = ColorTranslator.FromHtml("#ececec");
            GridTasks.ReadOnly = true;
            GridTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridTasks.MultiSelect = true;
            GridTasks.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Add columns to the issues grid
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
            foreach (KeyValuePair<int, Task> item in Program.SoftwareProject.Tasks)
            {
                AddTaskToGrid(item.Value);
            }
        }

        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
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
                    // Set the tooltip for this cell
                    int key = Convert.ToInt32(GridTasks["id", e.RowIndex].Value.ToString());
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
            GridTasks.Rows.Add(new object[] { newTask.ID.ToString(), GetTaskPriorityImage(newTask.Priority), newTask.Status.ToDescription(), newTask.Summary, newTask.DateCreated.ToString() });
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
                    RefreshTaskInGrid(GridTasks.SelectedRows[0].Index, id);
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
            }
        }

        /// <summary>
        /// Handle a double-click on the tasks grid: edit a task.
        /// </summary>
        private void GridTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridTasks.SelectedRows.Count == 1)
            {
                // Get the key of the task in the selected row 
                int id = Int32.Parse(GridTasks.Rows[e.RowIndex].Cells["id"].Value.ToString());

                TaskForm frmTask = new TaskForm(OperationType.Edit, Program.SoftwareProject.Tasks[id]);

                if (frmTask.ShowDialog() == DialogResult.OK)
                {
                    Program.SoftwareProject.Tasks[id] = frmTask.CurrentTask;
                    RefreshTaskInGrid(GridTasks.SelectedRows[0].Index, id);
                }

                frmTask.Dispose();
            }
        }

        /// <summary>
        /// When the selection changes in the tasks grid, enable/disable certain icons.
        /// </summary>
        private void GridTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (GridTasks.SelectedRows.Count == 1)
            {
                IconEditTask.Enabled = true;
                //DeleteTask.Enabled = true;
                IconCloneTask.Enabled = true;
            }
            else
            {
                IconEditTask.Enabled = false;
                //DeleteTask.Enabled = false;
                IconCloneTask.Enabled = false;
            }
        }
        #endregion

        #region Issues
        private void InitializeGridIssues()
        {
            GridIssues.BackgroundColor = TabControl.DefaultBackColor;
            GridIssues.BorderStyle = BorderStyle.None;
            GridIssues.Dock = DockStyle.Fill;

            // *** adapt **
            //GridIssues.StandardTab = true;
            GridIssues.AllowUserToAddRows = false;
            //remove this? *** GridIssues.AllowUserToDeleteRows = false;
            GridIssues.AllowUserToOrderColumns = true;
            GridIssues.AllowUserToResizeColumns = true;
            GridIssues.AllowUserToResizeRows = false;
            GridIssues.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            GridIssues.ColumnHeadersVisible = true;
            GridIssues.RowHeadersVisible = false;
            //GridIssues.GridColor = UnicodeViewer.Settings.GRID_BORDER_COLOR;
            GridIssues.GridColor = ColorTranslator.FromHtml("#ececec");
            GridIssues.ReadOnly = true;
            GridIssues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridIssues.MultiSelect = true;
            GridIssues.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            //GridIssues.Font = new Font(cboFonts.SelectedItem.ToString(), UnicodeViewer.Settings.GRID_DEFAULT_FONT_SIZE);
            GridIssues.ShowCellToolTips = true;

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
            GridIssues.Columns["status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GridIssues.Columns["status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GridIssues.Columns.Add("version", "Version");
            GridIssues.Columns["version"].Resizable = DataGridViewTriState.False;
            GridIssues.Columns["version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            GridIssues.Columns.Add("summary", "Summary");
            GridIssues.Columns.Add("created", "Created");
            GridIssues.Columns["created"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            GridIssues.Columns["priority"].DisplayIndex = 0;
            GridIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Populate the issues grid.
        /// </summary>
        private void PopulateGridIssues()
        {
            foreach (KeyValuePair<int, Issue> item in Program.SoftwareProject.Issues)
            {
                AddIssueToGrid(item.Value);
            }
        }

        /// <summary>
        /// Advanced formatting of cells.
        /// </summary>
        private void GridIssues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Configure/format the "priority" column's cells
            if (GridIssues.Columns[e.ColumnIndex].Name == "priority")
            {
                // Test if the cell has no image assigned
                if (e.Value == null)
                {
                    // Assign a 1x1 bitmap so that a "missing image" icon is not displayed
                    e.Value = new Bitmap(1, 1);
                }
                else
                {
                    // Set the tooltip for this cell

                    int key = Convert.ToInt32(GridIssues["id", e.RowIndex].Value.ToString());
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
            else if (GridIssues.Columns[e.ColumnIndex].Name == "status")
            {
                // Set the background color for the "status" cell

                int key = Convert.ToInt32(GridIssues["id", e.RowIndex].Value.ToString());
                Color backColor = Color.White;

                switch (Program.SoftwareProject.Issues[key].Status)
                {
                    case IssueStatus.Unconfirmed:
                        backColor = Color.WhiteSmoke;
                        break;

                    case IssueStatus.Confirmed:
                        backColor = Color.WhiteSmoke;
                        break;

                    case IssueStatus.InProgress:
                        backColor = Color.LightBlue;
                        break;

                    case IssueStatus.Resolved:
                        backColor = Color.LightGreen;
                        break;

                    case IssueStatus.Closed:
                        backColor = Color.DarkSeaGreen;
                        break;
                }

                e.CellStyle.BackColor = backColor;
            }
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
            GridIssues.Rows.Add(new object[] { newIssue.ID.ToString(), GetIssuePriorityImage(newIssue.Priority), newIssue.Status.ToDescription(), newIssue.Version, newIssue.Summary, newIssue.DateCreated.ToString() });
        }

        /// <summary>
        /// Refresh an issue's data in the issues grid.
        /// </summary>
        /// <param name="rowIndex">The row index in the issue grid, that holds the data for the issue.</param>
        /// <param name="issueID">The id of the issue (the issue key in the collection of issues).</param>
        private void RefreshIssueInGrid(int rowIndex, int issueID)
        {
            GridIssues.Rows[rowIndex].Cells["priority"].Value = GetIssuePriorityImage(Program.SoftwareProject.Issues[issueID].Priority);
            GridIssues.Rows[rowIndex].Cells["status"].Value = Program.SoftwareProject.Issues[issueID].Status.ToDescription();
            GridIssues.Rows[rowIndex].Cells["version"].Value = Program.SoftwareProject.Issues[issueID].Version;
            GridIssues.Rows[rowIndex].Cells["summary"].Value = Program.SoftwareProject.Issues[issueID].Summary;
            GridIssues.Rows[rowIndex].Cells["created"].Value = Program.SoftwareProject.Issues[issueID].DateCreated.ToString();
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
                    RefreshIssueInGrid(GridIssues.SelectedRows[0].Index, id);
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
        #endregion

    }
}
