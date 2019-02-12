using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MiniBug
{
    public enum IssueStatus
    {
        None = 0,
        Unconfirmed,
        Confirmed,
        [DescriptionAttribute("In progress")]
        InProgress,
        Resolved,
        Closed
    };

    public enum IssuePriority { None = 0, Low, Normal, High, Urgent, Immediate };

    public class Issue
    {
        /// <summary>
        /// Gets the ID of this issue.
        /// </summary>
        public int ID { get; set; } = 0;

        /// <summary>
        /// Gets or sets the status of this issue.
        /// </summary>
        public IssueStatus Status { get; set; } = IssueStatus.None;

        /// <summary>
        /// Gets or sets the priority of this issue.
        /// </summary>
        public IssuePriority Priority { get; set; } = IssuePriority.None;

        /// <summary>
        /// Gets or sets the summary of this issue.
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of this issue.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version of the software on which this issue occurs.
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the future version on which this issue will be resolved.
        /// </summary>
        public string TargetVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets the date/time this issue was created.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets or sets the date/time this issue was modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Creates a new issue.
        /// </summary>
        public Issue()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        /// <summary>
        /// Creates a new issue.
        /// </summary>
        /// <param name="id">The ID of this issue.</param>
        public Issue(int id)
        {
            ID = id;

            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        /// <summary>
        /// Creates a new issue.
        /// </summary>
        public Issue(IssueStatus status, IssuePriority priority, string summary, string description, string version, string targetVersion)
        {
            Status = status;
            Priority = priority;
            Summary = summary;
            Description = description;
            Version = version;
            TargetVersion = targetVersion;

            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        /// <summary>
        /// Create a clone of an instance of the Issue class.
        /// </summary>
        /// <param name="clonedInstance">The instance of the Issue class that will get the cloned instance's data.</param>
        public void Clone(ref Issue clonedInstance)
        {
            clonedInstance.Status = this.Status;
            clonedInstance.Priority = this.Priority;
            clonedInstance.Summary = this.Summary;
            clonedInstance.Description = this.Description;
            clonedInstance.Version = this.Version;
            clonedInstance.TargetVersion = this.TargetVersion;
            clonedInstance.DateModified = DateTime.Now;
        }
    }
}
