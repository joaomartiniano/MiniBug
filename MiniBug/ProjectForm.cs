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
    public partial class ProjectForm : Form
    {
        /// <summary>
        /// The current operation.
        /// </summary>
        public MiniBug.OperationType Operation { get; private set; } = OperationType.None;

        /// <summary>
        /// The current project (being created or edited).
        /// </summary>
        public MiniBug.Project CurrentProject { get; private set; } = null;

        public ProjectForm(OperationType operation, MiniBug.Project project = null)
        {
            InitializeComponent();

            Operation = operation;

            if (Operation == OperationType.New)
            {
                // Create a new instance of the Project class
                CurrentProject = new MiniBug.Project(string.Empty);
            }
            else if ((Operation == OperationType.Edit) && (project != null))
            {
                // Edit an existing project
                CurrentProject = project;
            }
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {

            // Make initializations based on the type of operation
            if (Operation == OperationType.New)
            {
                this.Text = "Add New Project";
            }
            else if (Operation == OperationType.Edit)
            {
                this.Text = "Edit Project";
            }
        }
    }
}
