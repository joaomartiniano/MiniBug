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
    public partial class TaskForm : Form
    {
        /// <summary>
        /// The current operation.
        /// </summary>
        public MiniBug.OperationType Operation { get; private set; } = OperationType.None;

        /// <summary>
        /// The current task (being created or edited).
        /// </summary>
        public MiniBug.Task CurrentTask { get; private set; } = null;

        /// <summary>
        /// List of status options.
        /// </summary>
        private List<ComboBoxItem> StatusOptionsList = new List<ComboBoxItem>();

        /// <summary>
        /// List of priority options.
        /// </summary>
        private List<ComboBoxItem> PriorityList = new List<ComboBoxItem>();

        public TaskForm(OperationType operation, MiniBug.Task task = null)
        {
            InitializeComponent();

            Operation = operation;

            if (Operation == OperationType.New)
            {
                // Create a new instance of the Task class
                CurrentTask = new Task();
            }
            else if ((Operation == OperationType.Edit) && (task != null))
            {
                // Edit an existing task
                CurrentTask = task;
            }

            // Populate the status list
            foreach (TaskStatus stat in Enum.GetValues(typeof(TaskStatus)))
            {
                if (stat != TaskStatus.None)
                {
                    StatusOptionsList.Add(new ComboBoxItem(Convert.ToInt32(stat), stat.ToDescription()));
                }
            }

            // Populate the priority list
            foreach (TaskPriority p in Enum.GetValues(typeof(TaskPriority)))
            {
                if (p != TaskPriority.None)
                {
                    PriorityList.Add(new ComboBoxItem(Convert.ToInt32(p), p.ToString()));
                }
            }
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the form, while the application is initializing
            this.SuspendLayout();

            this.Icon = MiniBug.Properties.Resources.Minibug;
            this.AcceptButton = btOk;
            this.CancelButton = btCancel;
            this.MinimumSize = new Size(690, 351);

            txtDescription.AcceptsReturn = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;

            // Initialize and populate the Status combobox
            cboStatus.AutoCompleteMode = AutoCompleteMode.None;
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.DataSource = StatusOptionsList;
            cboStatus.ValueMember = "Value";
            cboStatus.DisplayMember = "Text";

            // Initialize and populate the Priority combobox
            cboPriority.AutoCompleteMode = AutoCompleteMode.None;
            cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPriority.DataSource = PriorityList;
            cboPriority.ValueMember = "Value";
            cboPriority.DisplayMember = "Text";

            // Make initializations based on the type of operation
            if (Operation == OperationType.New)
            {
                this.Text = "Add New Task";
                lblDateCreatedTitle.Visible = false;
                lblDateCreated.Visible = false;
                lblDateModifiedTitle.Visible = false;
                lblDateModified.Visible = false;

                cboStatus.SelectedIndex = 0;
                cboPriority.SelectedIndex = 0;

                lblID.Text = Program.SoftwareProject.TaskIdCounter.ToString();
            }
            else if (Operation == OperationType.Edit)
            {
                this.Text = "Edit Task";

                // Populate the controls
                lblDateCreated.Text = CurrentTask.DateCreated.ToString();
                lblDateModified.Text = CurrentTask.DateModified.ToString();
                txtSummary.Text = CurrentTask.Summary;
                txtTargetVersion.Text = CurrentTask.TargetVersion;
                txtDescription.Text = CurrentTask.Description;

                cboStatus.SelectedValue = Convert.ToInt32(CurrentTask.Status);
                cboPriority.SelectedValue = Convert.ToInt32(CurrentTask.Priority);

                lblID.Text = CurrentTask.ID.ToString();
            }

            txtDescription.Font = ApplicationSettings.FormDescriptionFieldFont;

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
                CurrentTask.Summary = txtSummary.Text;
                CurrentTask.Status = ((TaskStatus)((MiniBug.ComboBoxItem)cboStatus.SelectedItem).Value);
                CurrentTask.Priority = ((TaskPriority)((MiniBug.ComboBoxItem)cboPriority.SelectedItem).Value);
                CurrentTask.TargetVersion = txtTargetVersion.Text;
                CurrentTask.Description = txtDescription.Text;

                if (Operation == OperationType.New)
                {
                    CurrentTask.DateCreated = DateTime.Now;
                }

                CurrentTask.DateModified = DateTime.Now;

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
