using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;

namespace MiniBug
{
    /// <summary>
    /// Stores the issues and tasks of a software project.
    /// </summary>
    [Serializable]
    public class Project
    {
        /// <summary>
        /// Gets the file format version of the project file.
        /// </summary>
        [JsonProperty]
        public string Version { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the current value of issue ID counter: the next issue created will have this value. This property is incremented automatically.
        /// </summary>
        [JsonProperty]
        public int IssueIdCounter { get; private set; } = 0;

        /// <summary>
        /// Gets the current value of task ID counter: the next task created will have this value. This property is incremented automatically.
        /// </summary>
        [JsonProperty]
        public int TaskIdCounter { get; private set; } = 0;

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the project file.
        /// </summary>
        [JsonIgnore]
        public string Filename { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location of the project file.
        /// </summary>
        [JsonIgnore]
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
            Version = ApplicationSettings.ProjectFileFormatVersion;
            IssueIdCounter = 1;
            TaskIdCounter = 1;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="name">The project name.</param>
        public Project(string name)
        {
            Version = ApplicationSettings.ProjectFileFormatVersion;
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

        /// <summary>
        /// Export the issues and tasks of a project to a file.
        /// </summary>
        public ExportProjectResult Export(string fileNameIssues, string fileNameTasks)
        {
            ExportProjectResult Result = new ExportProjectResult(true, fileNameIssues, true, fileNameTasks, FileSystemOperationStatus.None, FileSystemOperationStatus.None);

            // Export the issues
            if (Issues != null)
            {
                if ((Result.IssuesError = ExportIssues(fileNameIssues)) != FileSystemOperationStatus.ExportOK)
                {
                    Result.IssuesOK = false;
                }
            }

            // Export the tasks
            if (Tasks != null)
            {
                if ((Result.TasksError = ExportTasks(fileNameTasks)) != FileSystemOperationStatus.ExportOK)
                {
                    Result.TasksOK = false;
                }
            }

            return Result;
        }

        /// <summary>
        /// Export the project issues.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private FileSystemOperationStatus ExportIssues(string fileName)
        {
            try
            {
                using (var writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(Issues.Values);
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                return FileSystemOperationStatus.ExportToCsvErrorDirectoryNotFound;
            }
            catch (System.IO.PathTooLongException) // The path is too long
            {
                return FileSystemOperationStatus.ExportToCsvErrorPathTooLong;
            }
            catch (CsvHelperException) // Error reported by CsvHelper
            {
                return FileSystemOperationStatus.ExportToCsvErrorExporterComponent;
            }
            catch // General input/output error
            {
                return FileSystemOperationStatus.ExportToCsvIOError;
            }

            return FileSystemOperationStatus.ExportOK;
        }

        /// <summary>
        /// Export the project tasks.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="softwareProject"></param>
        /// <returns></returns>
        private FileSystemOperationStatus ExportTasks(string fileName)
        {
            try
            {
                using (var writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(Tasks.Values);
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                return FileSystemOperationStatus.ExportToCsvErrorDirectoryNotFound;
            }
            catch (System.IO.PathTooLongException) // The path is too long
            {
                return FileSystemOperationStatus.ExportToCsvErrorPathTooLong;
            }
            catch (CsvHelperException) // Error reported by CsvHelper
            {
                return FileSystemOperationStatus.ExportToCsvErrorExporterComponent;
            }
            catch // General input/output error
            {
                return FileSystemOperationStatus.ExportToCsvIOError;
            }

            return FileSystemOperationStatus.ExportOK;
        }
    }
}
