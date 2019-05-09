namespace MiniBug
{
    partial class SettingsForm
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
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lblGridlineColor = new System.Windows.Forms.Label();
            this.chkShowGridlines = new System.Windows.Forms.CheckBox();
            this.chkAlternateRowColors = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.GridlineColor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFont = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFontSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SelectionBackgroundColor = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SelectionTextColor = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AlternateRowColor = new System.Windows.Forms.Label();
            this.lblAlternateRowColor = new System.Windows.Forms.Label();
            this.RowColor = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btLoadDefaults = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(308, 365);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 8;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(389, 364);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 24);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lblGridlineColor
            // 
            this.lblGridlineColor.AutoSize = true;
            this.lblGridlineColor.Location = new System.Drawing.Point(42, 58);
            this.lblGridlineColor.Name = "lblGridlineColor";
            this.lblGridlineColor.Size = new System.Drawing.Size(82, 13);
            this.lblGridlineColor.TabIndex = 1;
            this.lblGridlineColor.Text = "&Gridline Color:";
            // 
            // chkShowGridlines
            // 
            this.chkShowGridlines.AutoSize = true;
            this.chkShowGridlines.Location = new System.Drawing.Point(23, 30);
            this.chkShowGridlines.Name = "chkShowGridlines";
            this.chkShowGridlines.Size = new System.Drawing.Size(104, 17);
            this.chkShowGridlines.TabIndex = 0;
            this.chkShowGridlines.Text = "S&how Gridlines";
            this.chkShowGridlines.UseVisualStyleBackColor = true;
            this.chkShowGridlines.CheckedChanged += new System.EventHandler(this.chkShowGridlines_CheckedChanged);
            // 
            // chkAlternateRowColors
            // 
            this.chkAlternateRowColors.AutoSize = true;
            this.chkAlternateRowColors.Location = new System.Drawing.Point(23, 64);
            this.chkAlternateRowColors.Name = "chkAlternateRowColors";
            this.chkAlternateRowColors.Size = new System.Drawing.Size(146, 17);
            this.chkAlternateRowColors.TabIndex = 2;
            this.chkAlternateRowColors.Text = "&Alternating Row Colors";
            this.chkAlternateRowColors.UseVisualStyleBackColor = true;
            this.chkAlternateRowColors.CheckedChanged += new System.EventHandler(this.chkAlternateRowColors_CheckedChanged);
            // 
            // GridlineColor
            // 
            this.GridlineColor.BackColor = System.Drawing.Color.White;
            this.GridlineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridlineColor.Location = new System.Drawing.Point(130, 53);
            this.GridlineColor.Name = "GridlineColor";
            this.GridlineColor.Size = new System.Drawing.Size(35, 22);
            this.GridlineColor.TabIndex = 2;
            this.GridlineColor.Click += new System.EventHandler(this.GridlineColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "&Font:";
            // 
            // cboFont
            // 
            this.cboFont.FormattingEnabled = true;
            this.cboFont.Location = new System.Drawing.Point(58, 17);
            this.cboFont.Name = "cboFont";
            this.cboFont.Size = new System.Drawing.Size(308, 21);
            this.cboFont.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "&Size:";
            // 
            // txtFontSize
            // 
            this.txtFontSize.Location = new System.Drawing.Point(416, 16);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.Size = new System.Drawing.Size(48, 22);
            this.txtFontSize.TabIndex = 3;
            this.txtFontSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFontSize_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "&Background Color:";
            // 
            // SelectionBackgroundColor
            // 
            this.SelectionBackgroundColor.BackColor = System.Drawing.Color.White;
            this.SelectionBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectionBackgroundColor.Location = new System.Drawing.Point(134, 23);
            this.SelectionBackgroundColor.Name = "SelectionBackgroundColor";
            this.SelectionBackgroundColor.Size = new System.Drawing.Size(35, 22);
            this.SelectionBackgroundColor.TabIndex = 1;
            this.SelectionBackgroundColor.Click += new System.EventHandler(this.SelectionBackgroundColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(196, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "&Text Color:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SelectionTextColor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.SelectionBackgroundColor);
            this.groupBox1.Location = new System.Drawing.Point(21, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 66);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection";
            // 
            // SelectionTextColor
            // 
            this.SelectionTextColor.BackColor = System.Drawing.Color.White;
            this.SelectionTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectionTextColor.Location = new System.Drawing.Point(263, 23);
            this.SelectionTextColor.Name = "SelectionTextColor";
            this.SelectionTextColor.Size = new System.Drawing.Size(35, 22);
            this.SelectionTextColor.TabIndex = 3;
            this.SelectionTextColor.Click += new System.EventHandler(this.SelectionTextColor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AlternateRowColor);
            this.groupBox2.Controls.Add(this.lblAlternateRowColor);
            this.groupBox2.Controls.Add(this.RowColor);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkAlternateRowColors);
            this.groupBox2.Location = new System.Drawing.Point(21, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 128);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Row Colors";
            // 
            // AlternateRowColor
            // 
            this.AlternateRowColor.BackColor = System.Drawing.Color.White;
            this.AlternateRowColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AlternateRowColor.Location = new System.Drawing.Point(162, 90);
            this.AlternateRowColor.Name = "AlternateRowColor";
            this.AlternateRowColor.Size = new System.Drawing.Size(35, 22);
            this.AlternateRowColor.TabIndex = 4;
            this.AlternateRowColor.Click += new System.EventHandler(this.AlternateRowColor_Click);
            // 
            // lblAlternateRowColor
            // 
            this.lblAlternateRowColor.AutoSize = true;
            this.lblAlternateRowColor.Location = new System.Drawing.Point(42, 95);
            this.lblAlternateRowColor.Name = "lblAlternateRowColor";
            this.lblAlternateRowColor.Size = new System.Drawing.Size(114, 13);
            this.lblAlternateRowColor.TabIndex = 3;
            this.lblAlternateRowColor.Text = "A&lternate Row Color:";
            // 
            // RowColor
            // 
            this.RowColor.BackColor = System.Drawing.Color.White;
            this.RowColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RowColor.Location = new System.Drawing.Point(92, 28);
            this.RowColor.Name = "RowColor";
            this.RowColor.Size = new System.Drawing.Size(35, 22);
            this.RowColor.TabIndex = 1;
            this.RowColor.Click += new System.EventHandler(this.RowColor_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "&Row Color:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblGridlineColor);
            this.groupBox3.Controls.Add(this.GridlineColor);
            this.groupBox3.Controls.Add(this.chkShowGridlines);
            this.groupBox3.Location = new System.Drawing.Point(21, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(443, 90);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gridlines";
            // 
            // btLoadDefaults
            // 
            this.btLoadDefaults.Location = new System.Drawing.Point(21, 365);
            this.btLoadDefaults.Name = "btLoadDefaults";
            this.btLoadDefaults.Size = new System.Drawing.Size(96, 23);
            this.btLoadDefaults.TabIndex = 7;
            this.btLoadDefaults.Text = "&Load defaults";
            this.btLoadDefaults.UseVisualStyleBackColor = true;
            this.btLoadDefaults.Click += new System.EventHandler(this.btLoadDefaults_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 400);
            this.Controls.Add(this.btLoadDefaults);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtFontSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboFont);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lblGridlineColor;
        private System.Windows.Forms.CheckBox chkShowGridlines;
        private System.Windows.Forms.CheckBox chkAlternateRowColors;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label GridlineColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFontSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label SelectionBackgroundColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label SelectionTextColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label RowColor;
        private System.Windows.Forms.Label AlternateRowColor;
        private System.Windows.Forms.Label lblAlternateRowColor;
        private System.Windows.Forms.Button btLoadDefaults;
    }
}