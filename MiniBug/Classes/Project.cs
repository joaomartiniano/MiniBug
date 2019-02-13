using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBug
{
    /// <summary>
    /// Stores the issues and tasks of a software project.
    /// </summary>
    public class Project
    {
        public int IssueIdCounter { get; private set; } = 0;

        public int TaskIdCounter { get; private set; } = 0;

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the project file.
        /// </summary>
        public string Filename { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location of the project file.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the project issues.
        /// </summary>
        public Dictionary<int, Issue> Issues { get; set; } = new Dictionary<int, Issue>();

        /// <summary>
        /// Gets or sets the project tasks.
        /// </summary>
        public Dictionary<int, Task> Tasks { get; set; } = new Dictionary<int, Task>();

        /// <summary>
        /// Creates a new project.
        /// </summary>
        public Project()
        {
            IssueIdCounter = 1;
            TaskIdCounter = 1;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="name">The project name.</param>
        public Project(string name)
        {
            Name = name;
            IssueIdCounter = 1;
            TaskIdCounter = 1;
        }

        /// <summary>
        /// Add a new issue.
        /// </summary>
        /// <param name="newIssue">An instance of the Issue class to add.</param>
        /// <returns>The id of the added issue.</returns>
        public int AddIssue(Issue newIssue)
        {
            newIssue.ID = IssueIdCounter;
            Issues.Add(IssueIdCounter, newIssue);
            IssueIdCounter++;

            return newIssue.ID;
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="newTask">An instance of the Task class to add.</param>
        /// <returns>The id of the added task.</returns>
        public int AddTask(Task newTask)
        {
            newTask.ID = TaskIdCounter;
            Tasks.Add(TaskIdCounter, newTask);
            TaskIdCounter++;

            return newTask.ID;
        }
    }
}
