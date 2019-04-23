namespace MiniBug
{
    partial class ExportFeedbackForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblIssuesFile = new System.Windows.Forms.Label();
            this.lblTasksFile = new System.Windows.Forms.Label();
            this.IconIssuesFile = new System.Windows.Forms.PictureBox();
            this.IconTasksFile = new System.Windows.Forms.PictureBox();
            this.lblTasksError = new System.Windows.Forms.Label();
            this.lblIssues = new System.Windows.Forms.Label();
            this.lblTasks = new System.Windows.Forms.Label();
            this.IconDescription = new System.Windows.Forms.PictureBox();
            this.lblIssuesError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.IconIssuesFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconTasksFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconDescription)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.BackColor = System.Drawing.Color.White;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.Location = new System.Drawing.Point(0, -2);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(609, 63);
            this.lblFormTitle.TabIndex = 1;
            this.lblFormTitle.Text = "Export Project to CSV";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(522, 342);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 2;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(36, 85);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(495, 29);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            // 
            // lblIssuesFile
            // 
            this.lblIssuesFile.AutoSize = true;
            this.lblIssuesFile.Location = new System.Drawing.Point(61, 140);
            this.lblIssuesFile.Name = "lblIssuesFile";
            this.lblIssuesFile.Size = new System.Drawing.Size(57, 13);
            this.lblIssuesFile.TabIndex = 4;
            this.lblIssuesFile.Text = "Issues file";
            // 
            // lblTasksFile
            // 
            this.lblTasksFile.AutoSize = true;
            this.lblTasksFile.Location = new System.Drawing.Point(61, 250);
            this.lblTasksFile.Name = "lblTasksFile";
            this.lblTasksFile.Size = new System.Drawing.Size(53, 13);
            this.lblTasksFile.TabIndex = 5;
            this.lblTasksFile.Text = "Tasks file";
            // 
            // IconIssuesFile
            // 
            this.IconIssuesFile.Location = new System.Drawing.Point(42, 139);
            this.IconIssuesFile.Name = "IconIssuesFile";
            this.IconIssuesFile.Size = new System.Drawing.Size(16, 16);
            this.IconIssuesFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconIssuesFile.TabIndex = 7;
            this.IconIssuesFile.TabStop = false;
            // 
            // IconTasksFile
            // 
            this.IconTasksFile.Location = new System.Drawing.Point(42, 249);
            this.IconTasksFile.Name = "IconTasksFile";
            this.IconTasksFile.Size = new System.Drawing.Size(16, 16);
            this.IconTasksFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconTasksFile.TabIndex = 8;
            this.IconTasksFile.TabStop = false;
            // 
            // lblTasksError
            // 
            this.lblTasksError.Location = new System.Drawing.Point(61, 270);
            this.lblTasksError.Name = "lblTasksError";
            this.lblTasksError.Padding = new System.Windows.Forms.Padding(3);
            this.lblTasksError.Size = new System.Drawing.Size(536, 52);
            this.lblTasksError.TabIndex = 9;
            this.lblTasksError.Text = "Description of tasks file error";
            this.lblTasksError.Paint += new System.Windows.Forms.PaintEventHandler(this.lblTasksError_Paint);
            // 
            // lblIssues
            // 
            this.lblIssues.AutoSize = true;
            this.lblIssues.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssues.Location = new System.Drawing.Point(39, 120);
            this.lblIssues.Name = "lblIssues";
            this.lblIssues.Size = new System.Drawing.Size(57, 13);
            this.lblIssues.TabIndex = 10;
            this.lblIssues.Text = "Issues file";
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasks.Location = new System.Drawing.Point(38, 229);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(54, 13);
            this.lblTasks.TabIndex = 11;
            this.lblTasks.Text = "Tasks file";
            // 
            // IconDescription
            // 
            this.IconDescription.Location = new System.Drawing.Point(17, 85);
            this.IconDescription.Name = "IconDescription";
            this.IconDescription.Size = new System.Drawing.Size(16, 16);
            this.IconDescription.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconDescription.TabIndex = 12;
            this.IconDescription.TabStop = false;
            // 
            // lblIssuesError
            // 
            this.lblIssuesError.Location = new System.Drawing.Point(61, 159);
            this.lblIssuesError.Name = "lblIssuesError";
            this.lblIssuesError.Padding = new System.Windows.Forms.Padding(3);
            this.lblIssuesError.Size = new System.Drawing.Size(536, 52);
            this.lblIssuesError.TabIndex = 13;
            this.lblIssuesError.Text = "Description of tasks file error";
            this.lblIssuesError.Paint += new System.Windows.Forms.PaintEventHandler(this.lblIssuesError_Paint);
            // 
            // ExportFeedbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 377);
            this.Controls.Add(this.lblIssuesError);
            this.Controls.Add(this.IconDescription);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.lblIssues);
            this.Controls.Add(this.lblTasksError);
            this.Controls.Add(this.IconTasksFile);
            this.Controls.Add(this.IconIssuesFile);
            this.Controls.Add(this.lblTasksFile);
            this.Controls.Add(this.lblIssuesFile);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.lblFormTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportFeedbackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.ExportFeedbackForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IconIssuesFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconTasksFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconDescription)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblIssuesFile;
        private System.Windows.Forms.Label lblTasksFile;
        private System.Windows.Forms.PictureBox IconIssuesFile;
        private System.Windows.Forms.PictureBox IconTasksFile;
        private System.Windows.Forms.Label lblTasksError;
        private System.Windows.Forms.Label lblIssues;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.PictureBox IconDescription;
        private System.Windows.Forms.Label lblIssuesError;
    }
}