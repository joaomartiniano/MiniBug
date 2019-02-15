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

        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.AcceptButton = btClose;

            this.Text = FormCaption;
            lblMessageTitle.Text = MessageTitle;
            lblMessage.Text = Message;

            pictureBox1.Image = FormImage;
            pictureBox1.Left = (lblMessageTitle.Left / 2) - (pictureBox1.Width / 2);

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
