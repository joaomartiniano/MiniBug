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
            Program.SoftwareProject.Issues.Add(new Issue(1, IssueStatus.Verified, IssuePriority.Normal, "Código CSS gerado: inicialização não está a obter valor do color picker do background do container", string.Empty, "0.1", "1.0.0"));
            Program.SoftwareProject.Issues.Add(new Issue(2, IssueStatus.Verified, IssuePriority.Low, "Labels com dimensões do container não mostram as dimensões durante resizing", string.Empty, "0.1", "1.0.0"));

            InitializeTabControl();
            InitializeGridIssues();
            PopulateGridIssues();

            // Resume the layout logic
            this.ResumeLayout();
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
            GridIssues.Left = TabControl.ClientRectangle.Left;
            GridIssues.Top = TabControl.ClientRectangle.Top;
            GridIssues.Width = TabControl.ClientRectangle.Width;
            GridIssues.Height = TabControl.ClientRectangle.Height;

            // *** adapt **
            //GridIssues.StandardTab = true;
            GridIssues.AllowUserToAddRows = false;
            //remove this? *** GridIssues.AllowUserToDeleteRows = false;
            GridIssues.AllowUserToOrderColumns = true;
            GridIssues.AllowUserToResizeColumns = true;
            //remove? ** GridIssues.AllowUserToResizeRows = false;
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
        }

        private void PopulateGridIssues()
        {
            foreach (Issue I in Program.SoftwareProject.Issues)
            {
                GridIssues.Rows.Add(new string[] { I.ID.ToString(), I.Status.ToString(), I.Version, I.Summary, I.DateCreated.ToString() });
            }
        }
    }
}
