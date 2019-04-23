namespace MiniBug
{
    partial class ExportForm
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
            this.btExport = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblIssues = new System.Windows.Forms.Label();
            this.lblTasks = new System.Windows.Forms.Label();
            this.txtIssuesFilename = new System.Windows.Forms.TextBox();
            this.txtTasksFilename = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.lblCsvIssues = new System.Windows.Forms.Label();
            this.lblCsvTasks = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.BackColor = System.Drawing.Color.White;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(609, 63);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Export Project to CSV";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(442, 232);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(75, 23);
            this.btExport.TabIndex = 11;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(523, 232);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 12;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Location = new System.Drawing.Point(35, 87);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(478, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "This project will be exported to two files: one containing the issues and the oth" +
    "er the tasks.";
            // 
            // lblIssues
            // 
            this.lblIssues.AutoSize = true;
            this.lblIssues.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssues.Location = new System.Drawing.Point(14, 125);
            this.lblIssues.Name = "lblIssues";
            this.lblIssues.Size = new System.Drawing.Size(94, 13);
            this.lblIssues.TabIndex = 2;
            this.lblIssues.Text = "Issues File Name:";
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasks.Location = new System.Drawing.Point(14, 153);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(90, 13);
            this.lblTasks.TabIndex = 5;
            this.lblTasks.Text = "Tasks File Name:";
            // 
            // txtIssuesFilename
            // 
            this.txtIssuesFilename.Location = new System.Drawing.Point(114, 120);
            this.txtIssuesFilename.Name = "txtIssuesFilename";
            this.txtIssuesFilename.Size = new System.Drawing.Size(270, 22);
            this.txtIssuesFilename.TabIndex = 3;
            this.txtIssuesFilename.TextChanged += new System.EventHandler(this.txtIssuesFilename_TextChanged);
            // 
            // txtTasksFilename
            // 
            this.txtTasksFilename.Location = new System.Drawing.Point(114, 148);
            this.txtTasksFilename.Name = "txtTasksFilename";
            this.txtTasksFilename.Size = new System.Drawing.Size(270, 22);
            this.txtTasksFilename.TabIndex = 6;
            this.txtTasksFilename.TextChanged += new System.EventHandler(this.txtTasksFilename_TextChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(14, 181);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 13);
            this.lblLocation.TabIndex = 8;
            this.lblLocation.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(114, 176);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(403, 22);
            this.txtLocation.TabIndex = 9;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(523, 176);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 10;
            this.btBrowse.Text = "Browse...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // lblCsvIssues
            // 
            this.lblCsvIssues.AutoSize = true;
            this.lblCsvIssues.Location = new System.Drawing.Point(390, 125);
            this.lblCsvIssues.Name = "lblCsvIssues";
            this.lblCsvIssues.Size = new System.Drawing.Size(25, 13);
            this.lblCsvIssues.TabIndex = 4;
            this.lblCsvIssues.Text = ".csv";
            // 
            // lblCsvTasks
            // 
            this.lblCsvTasks.AutoSize = true;
            this.lblCsvTasks.Location = new System.Drawing.Point(390, 153);
            this.lblCsvTasks.Name = "lblCsvTasks";
            this.lblCsvTasks.Size = new System.Drawing.Size(25, 13);
            this.lblCsvTasks.TabIndex = 7;
            this.lblCsvTasks.Text = ".csv";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MiniBug.Properties.Resources.Info_16x16;
            this.pictureBox1.Location = new System.Drawing.Point(17, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 267);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCsvTasks);
            this.Controls.Add(this.lblCsvIssues);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtTasksFilename);
            this.Controls.Add(this.txtIssuesFilename);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.lblIssues);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.lblFormTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblIssues;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.TextBox txtIssuesFilename;
        private System.Windows.Forms.TextBox txtTasksFilename;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Label lblCsvIssues;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCsvTasks;
    }
}