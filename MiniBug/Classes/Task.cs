using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MiniBug
{
    /// <summary>
    ///  Status of a task.
    /// </summary>
    public enum TaskStatus
    {
        None = 0,
        [DescriptionAttribute("Not started")]
        NotStarted,
        [DescriptionAttribute("In progress")]
        InProgress,
        Finished
    };

    /// <summary>
    /// Priority of a task.
    /// </summary>
    public enum TaskPriority { None = 0, Low, Normal, High, Urgent, Immediate };

    /// <summary>
    /// Fields used on the user interface (in a DataGridView and on a form) to represent a task.
    /// </summary>
    public enum TaskFieldsUI
    {
        ID = 0,
        Priority,
        Status,
        TargetVersion,
        [DescriptionAttribute("Date created")]
        Summary,
        Description,
        [DescriptionAttribute("Target version")]
        DateCreated,
        [DescriptionAttribute("Date modified")]
        DateModified
    };

    [Serializable]
    public class Task
    {
        /// <summary>
        /// Gets the ID of this task.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the priority of this task.
        /// </summary>
        public TaskPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the status of this task.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the future version on which this task will be applied/finished.
        /// </summary>
        public string TargetVersion { get; set; }

        /// <summary>
        /// Gets or sets the summary of this task.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the description of this task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date/time this task was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date/time this task was modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        public Task()
        {
            ;
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="id">The ID of this task.</param>
        public Task(int id)
        {
            ID = id;
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        public Task(TaskStatus status, TaskPriority priority, string summary, string description, string targetVersion)
        {
            Status = status;
            Priority = priority;
            Summary = summary;
            Description = description;
            TargetVersion = targetVersion;

            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        /// <summary>
        /// Create a clone of an instance of the Task class.
        /// </summary>
        /// <param name="clonedInstance">The instance of the Task class that will get the cloned instance's data.</param>
        public void Clone(ref Task clonedInstance)
        {
            clonedInstance.Status = this.Status;
            clonedInstance.Priority = this.Priority;
            clonedInstance.Summary = this.Summary;
            clonedInstance.Description = this.Description;
            clonedInstance.TargetVersion = this.TargetVersion;
            clonedInstance.DateCreated = clonedInstance.DateModified = DateTime.Now;
        }
    }
}
