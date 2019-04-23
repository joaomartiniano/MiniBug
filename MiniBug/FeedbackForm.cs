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
    public partial class FeedbackForm : Form
    {
        /// <summary>
        /// Gets or sets the caption of the form.
        /// </summary>
        public string FormCaption { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the feedback message title.
        /// </summary>
        public string MessageTitle { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the feedback message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the image displayed in the form.
        /// </summary>
        public Image FormImage { get; set; } = null;

        // *****
        //public FileSystemOperationStatus MessageType { get; set; } = FileSystemOperationStatus.None;

        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void FeedbackForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btClose;
        }

        public void SetUserInterface()
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.Text = FormCaption;
            lblMessageTitle.Text = MessageTitle;
            lblMessageTitle.AutoEllipsis = true;
            lblMessage.Text = Message;

            pictureBox1.Image = FormImage;
            pictureBox1.Left = (lblMessageTitle.Left / 2) - (pictureBox1.Width / 2);

            // Resume the layout logic
            this.ResumeLayout();
        }

        public void ComposeMessage(FileSystemOperationStatus MessageType)
        {
           switch (MessageType)
           {
                // Errors while opening a project

                case FileSystemOperationStatus.ProjectLoadErrorUnsupportedFormat:
                    FormCaption = "Project Read Error";
                    MessageTitle = "Error Reading Project File: Unsupported Project Format";
                    Message = "This version of MiniBug does not support this project file format: it appears it was created using another version of the program.";
                    Message += "\n\nSolution: use another version of this program to open the project file.";
                    FormImage = MiniBug.Properties.Resources.FileError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectLoadErrorDirectoryNotFound:
                    FormCaption = "Project Read Error";
                    MessageTitle = "Error Reading Project File: Location Not Found";
                    Message = "The specified location does not exist.";
                    FormImage = MiniBug.Properties.Resources.FolderError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectLoadErrorFileNotFound:
                    FormCaption = "Project Read Error";
                    MessageTitle = "Error Reading Project File: File Not Found";
                    Message = "The specified file was not found.";
                    Message += "\n\nPlease choose a different project file.";
                    FormImage = MiniBug.Properties.Resources.FileError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectLoadErrorPathTooLong:
                    FormCaption = "Project Read Error";
                    MessageTitle = "Error Reading Project File: Path Too Long";
                    Message = "The project path, filename or both are too long.";
                    Message += "\n\nSolution: choose a project file with a shorter path and/or shorter filename.";
                    FormImage = MiniBug.Properties.Resources.FileError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectLoadErrorDeserialization:
                    FormCaption = "Project Read Error";
                    MessageTitle = "Error Reading Project File: Unsupported Project Format";
                    Message = "The project file appears to be damaged or in an incorrect format.";
                    FormImage = MiniBug.Properties.Resources.FileError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectLoadErrorIO:
                    FormCaption = "Project Read Error";
                    MessageTitle = "Error Reading Project File: I/O Error";
                    Message = "There was a general input/output error while reading this project.";
                    Message += "\n\nCheck the status of the drive/device where the project file is stored.";
                    FormImage = MiniBug.Properties.Resources.CriticalError_64x64;
                    break;

                // Errors while saving a project

                case FileSystemOperationStatus.ProjectSaveErrorDirectoryNotFound:
                    FormCaption = "Project Save Error";
                    MessageTitle = "Error Saving Project File: Location Not Found";
                    Message = "The specified location does not exist.";
                    Message += "\n\nSolution: choose a different location for the project file.";
                    FormImage = MiniBug.Properties.Resources.FolderError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectSaveErrorPathTooLong:
                    FormCaption = "Project Save Error";
                    MessageTitle = "Error Saving Project File: Path Too Long";
                    Message = "The project path, file name or both are too long.";
                    Message += "\n\nSolution: choose a different location and/or a shorter filename.";
                    FormImage = MiniBug.Properties.Resources.FileError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectSaveErrorSerialization:
                    FormCaption = "Project Save Error";
                    MessageTitle = "Error Saving Project File: Unable to Save";
                    Message = "The project data appears to be damaged or in an incorrect format.";
                    FormImage = MiniBug.Properties.Resources.FileError_64x64;
                    break;

                case FileSystemOperationStatus.ProjectSaveIOError:
                    FormCaption = "Project Save Error";
                    MessageTitle = "Error Saving Project File: I/O Error";
                    Message = "There was a general input/output error while saving this project.";
                    Message += "\n\nSolution: choose a different drive/device to save the project file.";
                    FormImage = MiniBug.Properties.Resources.CriticalError_64x64;
                    break;

                // Export

                case FileSystemOperationStatus.ExportOK:
                    FormCaption = "Export";
                    MessageTitle = "Export Successful";
                    FormImage = MiniBug.Properties.Resources.Info_64x64;
                    break;

            }

            // Update the user interface
            SetUserInterface();
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
