using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBug
{
    public enum TaskStatus { None = 0 /* ....add more */ };

    public enum TaskPriority { None = 0, Low, Normal, High, Urgent, Immediate };

    public class Task
    {
        /// <summary>
        /// Gets the ID of this task.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets or sets the status of this task.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the priority of this task.
        /// </summary>
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the summary of this task.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the description of this task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the future version on which this task will be applied/finished.
        /// </summary>
        public string TargetVersion { get; set; }

        /// <summary>
        /// Gets the date/time this task was created.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets or sets the date/time this task was modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="id">The ID of this task.</param>
        public Task(int id)
        {
            ID = id;
        }
    }
}
