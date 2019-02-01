namespace MiniBug
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GridIssues = new System.Windows.Forms.DataGridView();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.NewProject = new System.Windows.Forms.ToolStripButton();
            this.OpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewIssue = new System.Windows.Forms.ToolStripButton();
            this.EditIssue = new System.Windows.Forms.ToolStripButton();
            this.DeleteIssue = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridIssues)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridIssues
            // 
            this.GridIssues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridIssues.Location = new System.Drawing.Point(6, 6);
            this.GridIssues.Name = "GridIssues";
            this.GridIssues.Size = new System.Drawing.Size(775, 365);
            this.GridIssues.TabIndex = 0;
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(2, 61);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(798, 377);
            this.TabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GridIssues);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(790, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Issues";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(790, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tasks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProject,
            this.OpenProject,
            this.toolStripSeparator1,
            this.NewIssue,
            this.EditIssue,
            this.DeleteIssue});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 54);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NewProject
            // 
            this.NewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewProject.Image = ((System.Drawing.Image)(resources.GetObject("NewProject.Image")));
            this.NewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewProject.Name = "NewProject";
            this.NewProject.Size = new System.Drawing.Size(36, 51);
            this.NewProject.Text = "toolStripButton1";
            this.NewProject.ToolTipText = "New Project";
            // 
            // OpenProject
            // 
            this.OpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenProject.Image = ((System.Drawing.Image)(resources.GetObject("OpenProject.Image")));
            this.OpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenProject.Name = "OpenProject";
            this.OpenProject.Size = new System.Drawing.Size(36, 36);
            this.OpenProject.Text = "toolStripButton2";
            this.OpenProject.ToolTipText = "Open Project";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // NewIssue
            // 
            this.NewIssue.Image = global::MiniBug.Properties.Resources.NewBug_32x32;
            this.NewIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewIssue.Name = "NewIssue";
            this.NewIssue.Size = new System.Drawing.Size(64, 51);
            this.NewIssue.Text = "New Issue";
            this.NewIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.NewIssue.ToolTipText = "New Issue";
            this.NewIssue.Click += new System.EventHandler(this.NewIssue_Click);
            // 
            // EditIssue
            // 
            this.EditIssue.Image = global::MiniBug.Properties.Resources.EditBug_32x32;
            this.EditIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditIssue.Name = "EditIssue";
            this.EditIssue.Size = new System.Drawing.Size(60, 51);
            this.EditIssue.Text = "Edit Issue";
            this.EditIssue.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.EditIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.EditIssue.ToolTipText = "Edit Issue";
            this.EditIssue.Click += new System.EventHandler(this.EditIssue_Click);
            // 
            // DeleteIssue
            // 
            this.DeleteIssue.Image = global::MiniBug.Properties.Resources.DeleteBug_32x32;
            this.DeleteIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteIssue.Name = "DeleteIssue";
            this.DeleteIssue.Size = new System.Drawing.Size(73, 51);
            this.DeleteIssue.Text = "Delete Issue";
            this.DeleteIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DeleteIssue.Click += new System.EventHandler(this.DeleteIssue_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridIssues)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridIssues;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton NewProject;
        private System.Windows.Forms.ToolStripButton OpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton NewIssue;
        private System.Windows.Forms.ToolStripButton EditIssue;
        private System.Windows.Forms.ToolStripButton DeleteIssue;
    }
}

