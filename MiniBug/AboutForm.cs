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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = "About MiniBug";
            this.AcceptButton = btOK;

            pictureBox1.Left = (lblApplicationName.Left / 2) - (pictureBox1.Width / 2);

            lblVersion.Text += " " + Application.ProductVersion.ToString();
        }

        /// <summary>
        /// Open a web browser when the user clicks on the link label.
        /// </summary>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        /// <summary>
        /// Close this form.
        /// </summary>
        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
