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

namespace MiniBug
{
    public partial class ExportForm : Form
    {
        /// <summary>
        /// True if the project has issues.
        /// </summary>
        private bool HasIssues = false;

        /// <summary>
        /// True if the project has tasks.
        /// </summary>
        private bool HasTasks = false;

        /// <summary>
        /// Location where the files will be exported.
        /// </summary>
        private string FilesLocation = string.Empty;

        /// <summary>
        /// File name of the issues .CSV file.
        /// </summary>
        public string IssuesFilename { get; private set; } = string.Empty;

        /// <summary>
        /// File name of the tasks .CSV file.
        /// </summary>
        public string TasksFilename { get; private set; } = string.Empty;

        public ExportForm()
        {
            InitializeComponent();

            HasIssues = ((Program.SoftwareProject.Issues != null) && (Program.SoftwareProject.Issues.Count > 0)) ? true : false;
            HasTasks = ((Program.SoftwareProject.Tasks != null) && (Program.SoftwareProject.Tasks.Count > 0)) ? true : false;

            IssuesFilename = $"{Program.SoftwareProject.Name} - Issues";
            TasksFilename = $"{Program.SoftwareProject.Name} - Tasks";
            FilesLocation = Program.SoftwareProject.Location;
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.AcceptButton = btExport;
            this.CancelButton = btCancel;

            lblFormTitle.Width = this.ClientRectangle.Width;

            txtIssuesFilename.MaxLength = 251;
            txtTasksFilename.MaxLength = 251;
            txtLocation.Text = FilesLocation;

            if (HasIssues && HasTasks)
            {
                lblInfo.Text = "This project will be exported to two CSV files: a file containing the issues and a file containing the tasks.";
                txtIssuesFilename.Text = IssuesFilename;
                txtTasksFilename.Text = TasksFilename;
            }
            else if (HasIssues && !HasTasks)
            {
                lblInfo.Text = "This project will be exported to a .CSV file containing the issues.";

                // Hide controls
                lblTasks.Visible = false;
                txtTasksFilename.Visible = false;
                lblCsvTasks.Visible = false;

                // Push controls up
                lblLocation.Top = lblTasks.Top;
                txtLocation.Top = txtTasksFilename.Top;
                btBrowse.Top = txtLocation.Top;

                txtIssuesFilename.Text = IssuesFilename;
            }
            else if (!HasIssues && HasTasks)
            {
                lblInfo.Text = "This project will be exported to a .CSV file containing the tasks.";

                // Hide controls
                lblIssues.Visible = false;
                txtIssuesFilename.Visible = false;
                lblCsvIssues.Visible = false;

                // Push controls up
                lblLocation.Top = lblTasks.Top;
                txtLocation.Top = txtTasksFilename.Top;
                btBrowse.Top = txtLocation.Top;

                lblTasks.Top = lblIssues.Top;
                txtTasksFilename.Top = txtIssuesFilename.Top;
                lblCsvTasks.Top = lblCsvIssues.Top;

                txtTasksFilename.Text = TasksFilename;
            }

            SetControlsState();

            // Resume the layout logic
            this.ResumeLayout();

            SetAccessibilityInformation();
        }

        /// <summary>
        /// Add accessibility data to form controls.
        /// </summary>
        private void SetAccessibilityInformation()
        {
            txtIssuesFilename.AccessibleDescription = "The name of the file containing the project Issues";
            txtTasksFilename.AccessibleDescription = "The name of the file containing the project Tasks";
            txtLocation.AccessibleDescription = "Folder where the project file will be exported";
            btBrowse.AccessibleDescription = "Browse for the folder where the project will be exported";
            btExport.AccessibleDescription = "Start the export operation";
        }

        /// <summary>
        /// Set the state of controls based on certain conditions.
        /// </summary>
        private void SetControlsState()
        {
            if (HasIssues && HasTasks)
            {
                btExport.Enabled = (!string.IsNullOrWhiteSpace(txtIssuesFilename.Text) && !string.IsNullOrWhiteSpace(txtTasksFilename.Text) && !string.IsNullOrWhiteSpace(txtLocation.Text)) ? true : false;
            }
            else if (HasIssues && !HasTasks)
            {
                btExport.Enabled = (!string.IsNullOrWhiteSpace(txtIssuesFilename.Text) && !string.IsNullOrWhiteSpace(txtLocation.Text)) ? true : false;
            }
            else if (!HasIssues && HasTasks)
            {
                btExport.Enabled = (!string.IsNullOrWhiteSpace(txtTasksFilename.Text) && !string.IsNullOrWhiteSpace(txtLocation.Text)) ? true : false;
            }

        }

        /// <summary>
        /// Check if this textbox is empty.
        /// </summary>
        private void txtIssuesFilename_TextChanged(object sender, EventArgs e)
        {
            SetControlsState();
        }

        /// <summary>
        /// Check if this textbox is empty.
        /// </summary>
        private void txtTasksFilename_TextChanged(object sender, EventArgs e)
        {
            SetControlsState();
        }

        /// <summary>
        /// Browse the location where the project will be exported.
        /// </summary>
        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = folderBrowserDialog1.SelectedPath;
            }

            SetControlsState();
        }

        /// <summary>
        /// Export the project.
        /// </summary>
        private void btExport_Click(object sender, EventArgs e)
        {
            IssuesFilename = System.IO.Path.Combine(txtLocation.Text, txtIssuesFilename.Text + ".csv");
            TasksFilename = System.IO.Path.Combine(txtLocation.Text, txtTasksFilename.Text + ".csv");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Cancel this operation and close the form.
        /// </summary>
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
