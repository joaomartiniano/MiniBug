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
            this.Text = "MiniBug Issue Tracker";
            
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            Program.SoftwareProject = new MiniBug.Project("FlexBox Simulator");
            Program.SoftwareProject.Filename = "MINIBUG-FlexBoxSimulator.json";
            
            // *** temporário
            Program.SoftwareProject.Issues.Add(1, new Issue(1, IssueStatus.Verified, IssuePriority.Normal, "Código CSS gerado: inicialização não está a obter valor do color picker do background do container", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.Issues.Add(2, new Issue(2, IssueStatus.Verified, IssuePriority.Low, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));

            InitializeTabControl();
            InitializeGridIssues();
            PopulateGridIssues();

            // Resume the layout logic
            this.ResumeLayout();

            // experiência***
            //Program.SoftwareProject.Save();
        }

        private void InitializeTabControl()
        {
            TabControl.Left = this.ClientRectangle.Left;
            TabControl.Width = this.ClientRectangle.Width;
        }

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
            GridIssues.ReadOnly = true;
            GridIssues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridIssues.MultiSelect = true;
            GridIssues.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            //GridIssues.Font = new Font(cboFonts.SelectedItem.ToString(), UnicodeViewer.Settings.GRID_DEFAULT_FONT_SIZE);

            GridIssues.ColumnCount = 5;
            GridIssues.Columns[0].HeaderText = "ID";
            GridIssues.Columns[0].Width = -1;
            GridIssues.Columns[1].HeaderText = "Status";
            GridIssues.Columns[2].HeaderText = "Version";
            GridIssues.Columns[3].HeaderText = "Summary";
            GridIssues.Columns[4].HeaderText = "Created";
            GridIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void PopulateGridIssues()
        {
            foreach (KeyValuePair<int, Issue> item in Program.SoftwareProject.Issues)
            {
                GridIssues.Rows.Add(new string[] { item.Key.ToString(), item.Value.Status.ToString(), item.Value.Version, item.Value.Summary, item.Value.DateCreated.ToString() });
            }
        }

        private void RefreshItemGridIssues(int index, int key)
        {
            GridIssues.Rows[index].Cells[1].Value = Program.SoftwareProject.Issues[key].Status.ToString();
            GridIssues.Rows[index].Cells[2].Value = Program.SoftwareProject.Issues[key].Version;
            GridIssues.Rows[index].Cells[3].Value = Program.SoftwareProject.Issues[key].Summary;
            GridIssues.Rows[index].Cells[4].Value = Program.SoftwareProject.Issues[key].DateCreated.ToString();
        }

        /// <summary>
        /// Create a new issue.
        /// </summary>
        private void NewIssue_Click(object sender, EventArgs e)
        {
            IssueForm frmIssue = new IssueForm(OperationType.New);

            if (frmIssue.ShowDialog() == DialogResult.OK)
            {
                Program.SoftwareProject.Issues.Add(frmIssue.CurrentIssue.ID, frmIssue.CurrentIssue);
                GridIssues.Rows.Add(new string[] { frmIssue.CurrentIssue.ID.ToString(), frmIssue.CurrentIssue.Status.ToString(), frmIssue.CurrentIssue.Version, frmIssue.CurrentIssue.Summary, frmIssue.CurrentIssue.DateCreated.ToString() });
            }

            frmIssue.Dispose();
        }

        /// <summary>
        /// Edit the selected issue.
        /// </summary>
        private void EditIssue_Click(object sender, EventArgs e)
        {
            if (GridIssues.SelectedRows.Count == 1)
            {
                // Get the key of the issue in the selected row 
                int key = Int32.Parse(GridIssues.SelectedRows[0].Cells[0].Value.ToString());

                IssueForm frmIssue = new IssueForm(OperationType.Edit, Program.SoftwareProject.Issues[key]);

                if (frmIssue.ShowDialog() == DialogResult.OK)
                {
                    Program.SoftwareProject.Issues[key] = frmIssue.CurrentIssue;
                    RefreshItemGridIssues(GridIssues.SelectedRows[0].Index, key);
                }

                frmIssue.Dispose();
            }
        }

        /// <summary>
        /// Delete the selected issue.
        /// </summary>
        private void DeleteIssue_Click(object sender, EventArgs e)
        {
            if (GridIssues.SelectedRows.Count == 1)
            {
                // Confirm this operation
                if (MessageBox.Show("Are you sure you want to delete this issue?", "Delete Issue", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    // Get the key of the issue in the selected row 
                    int key = Int32.Parse(GridIssues.SelectedRows[0].Cells[0].Value.ToString());
                    // Get the index of the selected row
                    int i = GridIssues.SelectedRows[0].Index;

                    // Remove the issue from the collection
                    Program.SoftwareProject.Issues.Remove(key);
                    
                    // Remove the row from the grid
                    GridIssues.Rows.RemoveAt(i);
                }
            }
        }
    }
}
