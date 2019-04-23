using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MiniBug
{
    public partial class ExportFeedbackForm : Form
    {
        private ExportProjectResult Result;

        public ExportFeedbackForm(ExportProjectResult result)
        {
            Result = result;

            InitializeComponent();
        }

        private void ExportFeedbackForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.AcceptButton = btClose;

            lblFormTitle.Width = this.ClientRectangle.Width;

            if (Result.IssuesOK && Result.TasksOK)
            {
                lblDescription.Text = "The project was successfully exported to the following files:";
                lblIssuesError.Visible = false;
                lblTasksError.Visible = false;
                
                // Icons
                IconDescription.Image = MiniBug.Properties.Resources.StatusOK_16x16;
                IconIssuesFile.Image = MiniBug.Properties.Resources.StatusOK_16x16;
                IconTasksFile.Image = MiniBug.Properties.Resources.StatusOK_16x16;
            }
            else
            {
                lblDescription.Text = "An error occurred while trying to export the project.";
                ComposeErrorMessages();

                lblIssuesError.Visible = !Result.IssuesOK;
                lblTasksError.Visible = !Result.TasksOK;

                // Icons
                IconDescription.Image = MiniBug.Properties.Resources.StatusWarning_16x16;
                IconIssuesFile.Image = (Result.IssuesOK) ? MiniBug.Properties.Resources.StatusOK_16x16 : MiniBug.Properties.Resources.StatusCriticalError_16x16;
                IconTasksFile.Image = (Result.TasksOK) ? MiniBug.Properties.Resources.StatusOK_16x16 : MiniBug.Properties.Resources.StatusCriticalError_16x16;
            }

            lblIssuesFile.Text = Path.GetFileName(Result.IssuesFile);
            lblTasksFile.Text = Path.GetFileName(Result.TasksFile);

            if (Result.IssuesOK)
            {
                lblTasks.Top = lblIssuesError.Top + 8;
                IconTasksFile.Top = lblTasks.Top + lblTasks.Height + 6;
                lblTasksFile.Top = IconTasksFile.Top;
                lblTasksError.Top = lblTasksFile.Top + lblTasksFile.Height + 5;
            }

            // Resume the layout logic
            this.ResumeLayout();
        }

        private void ComposeErrorMessages()
        {
            switch (Result.IssuesError)
            {
                case FileSystemOperationStatus.ExportToCsvErrorDirectoryNotFound:
                    lblIssuesError.Text = "Location Not Found: The specified location does not exist.\n\nSolution: choose a different location.";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorPathTooLong:
                    lblIssuesError.Text = "Path Too Long: The path, file name or both are too long.\n\nSolution: choose a different location and/or a shorter filename.";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorExporterComponent:
                    lblIssuesError.Text = "Component error: The component ***********";
                    break;

                case FileSystemOperationStatus.ExportToCsvIOError:
                    lblIssuesError.Text = "I/O Error: There was a general input/output error while attempting to export.\n\nSolution: choose a different drive/device to export the project.";
                    break;
            }

            switch (Result.TasksError)
            {
                case FileSystemOperationStatus.ExportToCsvErrorDirectoryNotFound:
                    lblTasksError.Text = "Location Not Found: The specified location does not exist.\n\nSolution: choose a different location.";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorPathTooLong:
                    lblTasksError.Text = "Path Too Long: The path, file name or both are too long.\n\nSolution: choose a different location and/or a shorter filename.";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorExporterComponent:
                    lblTasksError.Text = "Component error***********";
                    break;

                case FileSystemOperationStatus.ExportToCsvIOError:
                    lblTasksError.Text = "I/O Error\n\nThere was a general input/output error while attempting to export.\n\nSolution: choose a different drive/device to export the project.";
                    break;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblIssuesError_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, lblIssuesError.DisplayRectangle, Color.DarkGray, ButtonBorderStyle.Solid);
        }

        private void lblTasksError_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, lblTasksError.DisplayRectangle, Color.DarkGray, ButtonBorderStyle.Solid);
        }
    }
}
