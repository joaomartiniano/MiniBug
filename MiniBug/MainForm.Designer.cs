﻿namespace MiniBug
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
            this.GridTasks = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.IconNewIssue = new System.Windows.Forms.ToolStripButton();
            this.IconEditIssue = new System.Windows.Forms.ToolStripButton();
            this.IconDeleteIssue = new System.Windows.Forms.ToolStripButton();
            this.IconCloneIssue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.IconNewTask = new System.Windows.Forms.ToolStripButton();
            this.IconEditTask = new System.Windows.Forms.ToolStripButton();
            this.IconDeleteTask = new System.Windows.Forms.ToolStripButton();
            this.IconCloneTask = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMiniBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridIssues)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridTasks)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridIssues
            // 
            this.GridIssues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridIssues.Location = new System.Drawing.Point(6, 6);
            this.GridIssues.Name = "GridIssues";
            this.GridIssues.Size = new System.Drawing.Size(776, 327);
            this.GridIssues.TabIndex = 0;
            this.GridIssues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridIssues_CellDoubleClick);
            this.GridIssues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridIssues_CellFormatting);
            this.GridIssues.SelectionChanged += new System.EventHandler(this.GridIssues_SelectionChanged);
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(2, 76);
            this.TabControl.Margin = new System.Windows.Forms.Padding(0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(799, 365);
            this.TabControl.TabIndex = 1;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GridIssues);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 339);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Issues";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GridTasks);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 339);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tasks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GridTasks
            // 
            this.GridTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridTasks.Location = new System.Drawing.Point(6, 6);
            this.GridTasks.Name = "GridTasks";
            this.GridTasks.Size = new System.Drawing.Size(776, 343);
            this.GridTasks.TabIndex = 0;
            this.GridTasks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridTasks_CellDoubleClick);
            this.GridTasks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridTasks_CellFormatting);
            this.GridTasks.SelectionChanged += new System.EventHandler(this.GridTasks_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IconNewIssue,
            this.IconEditIssue,
            this.IconDeleteIssue,
            this.IconCloneIssue,
            this.toolStripSeparator2,
            this.IconNewTask,
            this.IconEditTask,
            this.IconDeleteTask,
            this.IconCloneTask});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(801, 52);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // IconNewIssue
            // 
            this.IconNewIssue.Image = global::MiniBug.Properties.Resources.NewBug_32x32;
            this.IconNewIssue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconNewIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconNewIssue.Name = "IconNewIssue";
            this.IconNewIssue.Size = new System.Drawing.Size(63, 49);
            this.IconNewIssue.Text = "New Issue";
            this.IconNewIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconNewIssue.ToolTipText = "New Issue";
            this.IconNewIssue.Click += new System.EventHandler(this.IconNewIssue_Click);
            // 
            // IconEditIssue
            // 
            this.IconEditIssue.Image = global::MiniBug.Properties.Resources.EditBug_32x32;
            this.IconEditIssue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconEditIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconEditIssue.Name = "IconEditIssue";
            this.IconEditIssue.Size = new System.Drawing.Size(60, 49);
            this.IconEditIssue.Text = "Edit Issue";
            this.IconEditIssue.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.IconEditIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconEditIssue.ToolTipText = "Edit Issue";
            this.IconEditIssue.Click += new System.EventHandler(this.IconEditIssue_Click);
            // 
            // IconDeleteIssue
            // 
            this.IconDeleteIssue.Image = global::MiniBug.Properties.Resources.DeleteBug_32x32;
            this.IconDeleteIssue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconDeleteIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconDeleteIssue.Name = "IconDeleteIssue";
            this.IconDeleteIssue.Size = new System.Drawing.Size(73, 49);
            this.IconDeleteIssue.Text = "Delete Issue";
            this.IconDeleteIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconDeleteIssue.Click += new System.EventHandler(this.IconDeleteIssue_Click);
            // 
            // IconCloneIssue
            // 
            this.IconCloneIssue.Image = global::MiniBug.Properties.Resources.CloneBug_32x32;
            this.IconCloneIssue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconCloneIssue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconCloneIssue.Name = "IconCloneIssue";
            this.IconCloneIssue.Size = new System.Drawing.Size(70, 49);
            this.IconCloneIssue.Text = "Clone Issue";
            this.IconCloneIssue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconCloneIssue.Click += new System.EventHandler(this.IconCloneIssue_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // IconNewTask
            // 
            this.IconNewTask.Image = global::MiniBug.Properties.Resources.NewTask_32x32;
            this.IconNewTask.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconNewTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconNewTask.Name = "IconNewTask";
            this.IconNewTask.Size = new System.Drawing.Size(59, 49);
            this.IconNewTask.Text = "New Task";
            this.IconNewTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconNewTask.Click += new System.EventHandler(this.IconNewTask_Click);
            // 
            // IconEditTask
            // 
            this.IconEditTask.Image = global::MiniBug.Properties.Resources.EditTask_32x32;
            this.IconEditTask.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconEditTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconEditTask.Name = "IconEditTask";
            this.IconEditTask.Size = new System.Drawing.Size(56, 49);
            this.IconEditTask.Text = "Edit Task";
            this.IconEditTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconEditTask.Click += new System.EventHandler(this.IconEditTask_Click);
            // 
            // IconDeleteTask
            // 
            this.IconDeleteTask.Image = global::MiniBug.Properties.Resources.DeleteTask_32x32;
            this.IconDeleteTask.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IconDeleteTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconDeleteTask.Name = "IconDeleteTask";
            this.IconDeleteTask.Size = new System.Drawing.Size(69, 49);
            this.IconDeleteTask.Text = "Delete Task";
            this.IconDeleteTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconDeleteTask.Click += new System.EventHandler(this.IconDeleteTask_Click);
            // 
            // IconCloneTask
            // 
            this.IconCloneTask.Image = ((System.Drawing.Image)(resources.GetObject("IconCloneTask.Image")));
            this.IconCloneTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IconCloneTask.Name = "IconCloneTask";
            this.IconCloneTask.Size = new System.Drawing.Size(66, 49);
            this.IconCloneTask.Text = "Clone Task";
            this.IconCloneTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.IconCloneTask.Click += new System.EventHandler(this.IconCloneTask_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.issuesToolStripMenuItem,
            this.tasksToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(801, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = global::MiniBug.Properties.Resources.NewProject_32x32;
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newProjectToolStripMenuItem.Text = "New project...";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Image = global::MiniBug.Properties.Resources.OpenProject_32x32;
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openProjectToolStripMenuItem.Text = "Open project...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // issuesToolStripMenuItem
            // 
            this.issuesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newIssueToolStripMenuItem,
            this.editIssueToolStripMenuItem,
            this.deleteIssueToolStripMenuItem,
            this.cloneIssueToolStripMenuItem});
            this.issuesToolStripMenuItem.Name = "issuesToolStripMenuItem";
            this.issuesToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.issuesToolStripMenuItem.Text = "Issues";
            // 
            // newIssueToolStripMenuItem
            // 
            this.newIssueToolStripMenuItem.Image = global::MiniBug.Properties.Resources.NewBug_32x32;
            this.newIssueToolStripMenuItem.Name = "newIssueToolStripMenuItem";
            this.newIssueToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newIssueToolStripMenuItem.Text = "New issue...";
            this.newIssueToolStripMenuItem.Click += new System.EventHandler(this.newIssueToolStripMenuItem_Click);
            // 
            // editIssueToolStripMenuItem
            // 
            this.editIssueToolStripMenuItem.Image = global::MiniBug.Properties.Resources.EditBug_32x32;
            this.editIssueToolStripMenuItem.Name = "editIssueToolStripMenuItem";
            this.editIssueToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.editIssueToolStripMenuItem.Text = "Edit issue...";
            this.editIssueToolStripMenuItem.Click += new System.EventHandler(this.editIssueToolStripMenuItem_Click);
            // 
            // deleteIssueToolStripMenuItem
            // 
            this.deleteIssueToolStripMenuItem.Image = global::MiniBug.Properties.Resources.DeleteBug_32x32;
            this.deleteIssueToolStripMenuItem.Name = "deleteIssueToolStripMenuItem";
            this.deleteIssueToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.deleteIssueToolStripMenuItem.Text = "Delete issue";
            this.deleteIssueToolStripMenuItem.Click += new System.EventHandler(this.deleteIssueToolStripMenuItem_Click);
            // 
            // cloneIssueToolStripMenuItem
            // 
            this.cloneIssueToolStripMenuItem.Image = global::MiniBug.Properties.Resources.CloneBug_32x32;
            this.cloneIssueToolStripMenuItem.Name = "cloneIssueToolStripMenuItem";
            this.cloneIssueToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.cloneIssueToolStripMenuItem.Text = "Clone issue";
            this.cloneIssueToolStripMenuItem.Click += new System.EventHandler(this.cloneIssueToolStripMenuItem_Click);
            // 
            // tasksToolStripMenuItem
            // 
            this.tasksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTaskToolStripMenuItem,
            this.editTaskToolStripMenuItem,
            this.deleteTaskToolStripMenuItem,
            this.cloneTaskToolStripMenuItem});
            this.tasksToolStripMenuItem.Name = "tasksToolStripMenuItem";
            this.tasksToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.tasksToolStripMenuItem.Text = "Tasks";
            // 
            // newTaskToolStripMenuItem
            // 
            this.newTaskToolStripMenuItem.Image = global::MiniBug.Properties.Resources.NewTask_32x32;
            this.newTaskToolStripMenuItem.Name = "newTaskToolStripMenuItem";
            this.newTaskToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newTaskToolStripMenuItem.Text = "New task...";
            this.newTaskToolStripMenuItem.Click += new System.EventHandler(this.newTaskToolStripMenuItem_Click);
            // 
            // editTaskToolStripMenuItem
            // 
            this.editTaskToolStripMenuItem.Image = global::MiniBug.Properties.Resources.EditTask_32x32;
            this.editTaskToolStripMenuItem.Name = "editTaskToolStripMenuItem";
            this.editTaskToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editTaskToolStripMenuItem.Text = "Edit task...";
            this.editTaskToolStripMenuItem.Click += new System.EventHandler(this.editTaskToolStripMenuItem_Click);
            // 
            // deleteTaskToolStripMenuItem
            // 
            this.deleteTaskToolStripMenuItem.Image = global::MiniBug.Properties.Resources.DeleteTask_32x32;
            this.deleteTaskToolStripMenuItem.Name = "deleteTaskToolStripMenuItem";
            this.deleteTaskToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteTaskToolStripMenuItem.Text = "Delete task";
            this.deleteTaskToolStripMenuItem.Click += new System.EventHandler(this.deleteTaskToolStripMenuItem_Click);
            // 
            // cloneTaskToolStripMenuItem
            // 
            this.cloneTaskToolStripMenuItem.Name = "cloneTaskToolStripMenuItem";
            this.cloneTaskToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cloneTaskToolStripMenuItem.Text = "Clone task";
            this.cloneTaskToolStripMenuItem.Click += new System.EventHandler(this.cloneTaskToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMiniBugToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMiniBugToolStripMenuItem
            // 
            this.aboutMiniBugToolStripMenuItem.Image = global::MiniBug.Properties.Resources.About_32x32;
            this.aboutMiniBugToolStripMenuItem.Name = "aboutMiniBugToolStripMenuItem";
            this.aboutMiniBugToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.aboutMiniBugToolStripMenuItem.Text = "About MiniBug";
            this.aboutMiniBugToolStripMenuItem.Click += new System.EventHandler(this.aboutMiniBugToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.TabControl);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridIssues)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridTasks)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridIssues;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton IconNewIssue;
        private System.Windows.Forms.ToolStripButton IconEditIssue;
        private System.Windows.Forms.ToolStripButton IconDeleteIssue;
        private System.Windows.Forms.ToolStripButton IconCloneIssue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton IconNewTask;
        private System.Windows.Forms.ToolStripButton IconEditTask;
        private System.Windows.Forms.ToolStripButton IconDeleteTask;
        private System.Windows.Forms.DataGridView GridTasks;
        private System.Windows.Forms.ToolStripButton IconCloneTask;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem issuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMiniBugToolStripMenuItem;
    }
}

