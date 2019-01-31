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
    public partial class IssueForm : Form
    {
        public MiniBug.OperationType Operation { get; private set; } = OperationType.None;

        public MiniBug.Issue CurrentIssue { get; private set; } = null;

        public IssueForm(OperationType operation, MiniBug.Issue issue = null)
        {
            InitializeComponent();

            Operation = operation;
            
            if (Operation == OperationType.New)
            {
                // ****
                CurrentIssue = new Issue(99);
            }
            else if ((Operation == OperationType.Edit) && (issue != null))
            {
                CurrentIssue = issue;
            }
        }

        private void IssueForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.AcceptButton = btOk;
            this.CancelButton = btCancel;
            this.MinimumSize = new Size(644, 351);

            txtDescription.AcceptsReturn = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;

            // Populate the Status combobox
            foreach (IssueStatus stat in Enum.GetValues(typeof(IssueStatus)))
            {
                cboStatus.Items.Add(new ComboBoxItem(Convert.ToInt32(stat), Issue.IssueStatusToString(stat)));
            }
            cboStatus.ValueMember = "Value";
            cboStatus.DisplayMember = "Text";

            // Populate the Priority combobox
            foreach (IssuePriority p in Enum.GetValues(typeof(IssuePriority)))
            {
                cboPriority.Items.Add(new ComboBoxItem(Convert.ToInt32(p), p.ToString()));
            }
            cboStatus.ValueMember = "Value";
            cboStatus.DisplayMember = "Text";

            if (Operation == OperationType.New)
            {
                this.Text = "Add New Issue";
                lblDateCreatedTitle.Visible = false;
                lblDateCreated.Visible = false;
                lblDateModifiedTitle.Visible = false;
                lblDateModified.Visible = false;
            }
            else if (Operation == OperationType.Edit)
            {
                this.Text = "Edit Issue";

                // Populate the controls
                lblID.Text = CurrentIssue.ID.ToString();
                lblDateCreated.Text = CurrentIssue.DateCreated.ToString();
                lblDateModified.Text = CurrentIssue.DateModified.ToString();
                txtSummary.Text = CurrentIssue.Summary;
                txtVersion.Text = CurrentIssue.Version;
                txtTargetVersion.Text = CurrentIssue.TargetVersion;
                txtDescription.Text = CurrentIssue.Description;

                cboStatus.SelectedIndex = (int)CurrentIssue.Status;
                cboPriority.SelectedIndex = (int)CurrentIssue.Priority;
            }

            // Resume the layout logic
            this.ResumeLayout();
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtSummary.Text != string.Empty)
            {
                CurrentIssue.Summary = txtSummary.Text;
                //CurrentIssue.Status = cboStatus.SelectedValue;
                //CurrentIssue.Priority = 
                CurrentIssue.Version = txtVersion.Text;
                CurrentIssue.TargetVersion = txtTargetVersion.Text;
                CurrentIssue.Description = txtDescription.Text;

                if (Operation == OperationType.New)
                {
                    // ***
                    //CurrentIssue.DateCreated = DateTime.Now;
                }

                CurrentIssue.DateModified = DateTime.Now;

                //TaskStatus = (TaskStatusValues)((ComboBoxItem)cboStatus.SelectedItem).Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Cancel this operation and close the form.
        /// </summary>
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
