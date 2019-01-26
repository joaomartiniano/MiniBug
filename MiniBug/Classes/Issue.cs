using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBug
{
    public enum IssueStatus { None = 0, Unconfirmed, Confirmed, InProgress, Resolved, Verified };

    public enum IssuePriority { None = 0, Low, Normal, High, Urgent, Immediate };

    public class Issue
    {
        /// <summary>
        /// Gets the ID of this issue.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets or sets the status of this issue.
        /// </summary>
        public IssueStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the priority of this issue.
        /// </summary>
        public IssuePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the summary of this issue.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the description of this issue.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the version of the software on which this issue occurs.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the future version on which this issue will be resolved.
        /// </summary>
        public string TargetVersion { get; set; }

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
        /// <param name="id">The ID of this issue.</param>
        public Issue(int id, IssueStatus status, IssuePriority priority, string summary, string description, string version, string targetVersion)
        {
            ID = id;
            Status = status;
            Priority = priority;
            Summary = summary;
            Description = description;
            Version = version;
            TargetVersion = targetVersion;

            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
