﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MiniBug
{
    public enum FileOperationsStatus { None = 0, Success, DirectoryNotFound, FileNotFound, IOError, PathTooLong }

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
        /// Gets or sets the project filename.
        /// </summary>
        public string Filename { get; set; } = string.Empty;

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

        /// <summary>
        /// Load application data.
        /// </summary>
        public FileOperationsStatus Load()
        {
            String json = string.Empty;

            try
            {
                // Open the file
                System.IO.StreamReader r = new System.IO.StreamReader(Filename);

                // Read the file contents
                json = r.ReadToEnd();

                r.Close();
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                return FileOperationsStatus.DirectoryNotFound;
            }
            catch (System.IO.FileNotFoundException) // The file does not exist
            {
                return FileOperationsStatus.FileNotFound;
            }
            catch
            {
                return FileOperationsStatus.IOError;
            }

            //Categories = new JavaScriptSerializer().Deserialize<List<Category>>(json);

            return FileOperationsStatus.Success;
        }

        /// <summary>
        /// Saves the application data. The file is overwritten.
        /// </summary>
        public FileOperationsStatus Save()
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(Issues);

            serializedResult += serializer.Serialize(Tasks);

            try
            {
                System.IO.File.WriteAllText(Filename, serializedResult);
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                return FileOperationsStatus.DirectoryNotFound;
            }
            catch (System.IO.PathTooLongException) // The path is too long
            {
                return FileOperationsStatus.PathTooLong;
            }
            catch
            {
                return FileOperationsStatus.IOError;
            }

            return FileOperationsStatus.Success;
        }
    }
}