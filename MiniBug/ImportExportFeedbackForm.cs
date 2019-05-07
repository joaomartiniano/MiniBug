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
    public partial class ImportExportFeedbackForm : Form
    {
        /// <summary>
        /// The operation (import or export) to show feedback about.
        /// </summary>
        public ImportExportOperation Operation = ImportExportOperation.None;

        public ImportExportFeedbackForm(ImportExportOperation operation)
        {
            InitializeComponent();

            Operation = operation;
        }

        private void ImportExportFeedbackForm_Load(object sender, EventArgs e)
        {
            Padding labelPadding = new Padding(0, 1, 0, 1);
            Padding labelPaddingTitle = new Padding(0, 8, 0, 1);
            Color colorSuccess = Color.DarkGreen;
            Color colorError = Color.DarkRed;

            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            lblFormTitle.Width = this.ClientRectangle.Width;

            this.AcceptButton = btClose;

            FlowLayoutPanel1.AutoScroll = true;
            FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            FlowLayoutPanel1.WrapContents = false;

            if (Operation == ImportExportOperation.Import)
            {
                this.Text = "Import Project";
                lblFormTitle.Text = "Import From CSV";
            }
            else if (Operation == ImportExportOperation.Export)
            {
                this.Text = "Export Project";
                lblFormTitle.Text = "Export Project To CSV";

                /* The success of the export operation. Possible values:
                 *  0 -> error
                 *  1 -> success + error
                 *  2 -> success
                 */
                int success = 0;               
	            
                // Determine the success (none, partial or full) of the export operation
                if ((Program.SoftwareProject.ExportResult.Issues.Result == FileSystemOperationStatus.None) && 
                    (Program.SoftwareProject.ExportResult.Tasks.Result == FileSystemOperationStatus.None))
                {
                    return;
                }
                else if ((Program.SoftwareProject.ExportResult.Issues.Result == FileSystemOperationStatus.None) &&
                         (Program.SoftwareProject.ExportResult.Tasks.Result == FileSystemOperationStatus.ExportOK))
                {
                    success = 2;
                }
                else if ((Program.SoftwareProject.ExportResult.Issues.Result == FileSystemOperationStatus.ExportOK) &&
                         (Program.SoftwareProject.ExportResult.Tasks.Result == FileSystemOperationStatus.None))
                {
                    success = 2;
                }
                else if ((Program.SoftwareProject.ExportResult.Issues.Result == FileSystemOperationStatus.ExportOK) &&
                         (Program.SoftwareProject.ExportResult.Tasks.Result == FileSystemOperationStatus.ExportOK))
                {
                    success = 2;
                }
                else if ((Program.SoftwareProject.ExportResult.Issues.Result != FileSystemOperationStatus.ExportOK) &&
                         (Program.SoftwareProject.ExportResult.Tasks.Result == FileSystemOperationStatus.ExportOK))
                {
                    success = 1;
                }
                else if ((Program.SoftwareProject.ExportResult.Issues.Result == FileSystemOperationStatus.ExportOK) &&
                         (Program.SoftwareProject.ExportResult.Tasks.Result != FileSystemOperationStatus.ExportOK))
                {
                    success = 1;
                }
                else
                {
                    success = 0;
                }

                if (success == 0)
                {
                    // Completely unable to export the project
                    lblDescription.Text = "One or more errors occurred while trying to export the project.";
                    IconDescription.Image = MiniBug.Properties.Resources.StatusCriticalError_16x16;
                }
                else if (success == 1)
                {
                    // Partially able to export the project
                    lblDescription.Text = "An error occurred while exporting the project.";
                    IconDescription.Image = MiniBug.Properties.Resources.StatusWarning_16x16;
                }
                else if (success == 2)
                {
                    // Export completed successfully
                    lblDescription.Text = "The project was successfully exported.";
                    IconDescription.Image = MiniBug.Properties.Resources.StatusOK_16x16;
                }

                // Issues: details
                if (Program.SoftwareProject.ExportResult.Issues.Result != FileSystemOperationStatus.None)
                {
                    FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                    {
                        Name = "lbl1",
                        Text = $"Issues: {Program.SoftwareProject.ExportResult.Issues.ResultMessage}",
                        AutoSize = true,
                        Padding = labelPadding,
                        ForeColor = (Program.SoftwareProject.ExportResult.Issues.Result == FileSystemOperationStatus.ExportOK) ? colorSuccess : colorError
                    });
                    FlowLayoutPanel1.Controls["lbl1"].Font = new Font(FlowLayoutPanel1.Controls["lbl1"].Name, 8, FontStyle.Bold);

                    FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                    {
                        Text = $"File: {Program.SoftwareProject.ExportResult.Issues.FileName}",
                        AutoSize = true,
                        Padding = labelPadding
                    });

                    FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                    {
                        Text = $"Location: {Program.SoftwareProject.ExportResult.Issues.Path}",
                        AutoSize = true,
                        Padding = labelPadding
                    });

                    // If an error occurred, show some more info
                    if (Program.SoftwareProject.ExportResult.Issues.Result != FileSystemOperationStatus.ExportOK)
                    {
                        FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                        {
                            Text = $"Description: {Program.SoftwareProject.ExportResult.Issues.ResultDescription}",
                            AutoSize = true,
                            Padding = labelPadding
                        });

                        FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                        {
                            Text = $"Solution: {Program.SoftwareProject.ExportResult.Issues.ResultSolution}",
                            AutoSize = true,
                            Padding = labelPadding
                        });
                    }
                }

                // Tasks: details
                if (Program.SoftwareProject.ExportResult.Tasks.Result != FileSystemOperationStatus.None)
                {
                    FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                    {
                        Name = "lbl2",
                        Text = $"Tasks: {Program.SoftwareProject.ExportResult.Tasks.ResultMessage}",
                        AutoSize = true,
                        Padding = labelPaddingTitle,
                        ForeColor = (Program.SoftwareProject.ExportResult.Tasks.Result == FileSystemOperationStatus.ExportOK) ? colorSuccess : colorError
                    });
                    FlowLayoutPanel1.Controls["lbl2"].Font = new Font(FlowLayoutPanel1.Controls["lbl2"].Name, 8, FontStyle.Bold);

                    FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                    {
                        Text = $"File: {Program.SoftwareProject.ExportResult.Tasks.FileName}",
                        AutoSize = true,
                        Padding = labelPadding
                    });

                    FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                    {
                        Text = $"Location: {Program.SoftwareProject.ExportResult.Tasks.Path}",
                        AutoSize = true,
                        Padding = labelPadding
                    });

                    // If an error occurred, show some more info
                    if (Program.SoftwareProject.ExportResult.Tasks.Result != FileSystemOperationStatus.ExportOK)
                    {
                        FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                        {
                            Text = $"Description: {Program.SoftwareProject.ExportResult.Tasks.ResultDescription}",
                            AutoSize = true,
                            Padding = labelPadding
                        });

                        FlowLayoutPanel1.Controls.Add(new System.Windows.Forms.Label()
                        {
                            Text = $"Solution: {Program.SoftwareProject.ExportResult.Tasks.ResultSolution}",
                            AutoSize = true,
                            Padding = labelPadding
                        });
                    }
                }
            }

            // Resume the layout logic
            this.ResumeLayout();
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
