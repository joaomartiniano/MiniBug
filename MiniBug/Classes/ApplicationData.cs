using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;

namespace MiniBug
{
    public struct ExportProjectResult
    {
        /// <summary>If true, indicates that the project issues where exported successfully.</summary>
        public bool IssuesOK;

        /// <summary>Path and file name of the issues file.</summary>
        public string IssuesFile;

        /// <summary>If true, indicates that the project tasks where exported successfully.</summary>
        public bool TasksOK;

        /// <summary>Path and file name of the tasks file.</summary>
        public string TasksFile;

        /// <summary>Type of error encountered while exporting the project issues.</summary>
        public FileSystemOperationStatus IssuesError;

        /// <summary>Type of error encountered while exporting the project tasks.</summary>
        public FileSystemOperationStatus TasksError;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="issuesOK">If true, indicates that the project issues where exported successfully.</param>
        /// <param name="issuesFile">Path and file name of the issues file.</param>
        /// <param name="tasksOK">If true, indicates that the project tasks where exported successfully.</param>
        /// <param name="tasksFile">Path and file name of the tasks file.</param>
        /// <param name="issuesError">Type of error encountered while exporting the project issues.</param>
        /// <param name="tasksError">Type of error encountered while exporting the project tasks.</param>
        public ExportProjectResult(bool issuesOK, string issuesFile, bool tasksOK, string tasksFile, FileSystemOperationStatus issuesError, FileSystemOperationStatus tasksError)
        {
            IssuesOK = issuesOK;
            IssuesFile = issuesFile;
            TasksOK = tasksOK;
            TasksFile = tasksFile;
            IssuesError = issuesError;
            TasksError = tasksError;
        }
    }

    public enum FileSystemOperationStatus {
        None = 0,

        /// <summary>The operation was successfull.</summary>
        OK = 1,

        /// <summary>Trying to load a project with an unsupported file format.</summary>
        ProjectLoadErrorUnsupportedFormat,

        /// <summary>Trying to load a project file from a non-existing directory.</summary>
        ProjectLoadErrorDirectoryNotFound,

        /// <summary>File not found while trying to load a project.</summary>
        ProjectLoadErrorFileNotFound,

        /// <summary>Project file path and/or file name too long.</summary>
        ProjectLoadErrorPathTooLong,

        /// <summary>Error deserializing the project file.</summary>
        ProjectLoadErrorDeserialization,

        /// <summary>General I/O error while trying to load the project file.</summary>
        ProjectLoadErrorIO,

        /// <summary>Trying to save a project file to a non-existing directory.</summary>
        ProjectSaveErrorDirectoryNotFound,

        /// <summary>Project file's path and/or file name too long.</summary>
        ProjectSaveErrorPathTooLong,

        /// <summary>Error serializing the project file.</summary>
        ProjectSaveErrorSerialization,

        /// <summary>General I/O error while trying to save the project file.</summary>
        ProjectSaveIOError,

        /// <summary>Project successfully exported.</summary>
        ExportOK,

        /// <summary>Export projet to CSV: trying to export to a non-existing directory.</summary>
        ExportToCsvErrorDirectoryNotFound,

        /*/// <summary>Export projet to CSV: error reported by the CSV exporting module.</summary>
        ExportToCsvErrorFileNotFound,*/

        /// <summary>Export projet to CSV: file path and/or file name too long.</summary>
        ExportToCsvErrorPathTooLong,

        /// <summary>Export projet to CSV: error reported by the component responsible for performing the export.</summary>
        ExportToCsvErrorExporterComponent,

        /// <summary>Export projet to CSV: general I/O error.</summary>
        ExportToCsvIOError
    }

    public static class ApplicationData
    {
        /// <summary>
        /// Saves a project data to a file. The file is overwritten.
        /// </summary>
        /// <param name="softwareProject">An instance of the Project class.</param>
        public static FileSystemOperationStatus SaveProject(in Project softwareProject)
        {
            string output = string.Empty;
            string filename = string.Empty;

            output = JsonConvert.SerializeObject(softwareProject);
            filename = System.IO.Path.Combine(softwareProject.Location, softwareProject.Filename);

            try
            {
                System.IO.File.WriteAllText(filename, output);
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                return FileSystemOperationStatus.ProjectSaveErrorDirectoryNotFound;
            }
            catch (System.IO.PathTooLongException) // The path is too long
            {
                return FileSystemOperationStatus.ProjectSaveErrorPathTooLong;
            }
            catch (JsonException) // Error serializing the project
            {
                return FileSystemOperationStatus.ProjectSaveErrorSerialization;
            }
            catch // General input/output error
            {
                return FileSystemOperationStatus.ProjectSaveIOError;
            }

            return FileSystemOperationStatus.OK;
        }

        /// <summary>
        /// Load project data from a file.
        /// </summary>
        /// <param name="filename">Name and location of the project file.</param>
        /// <param name="softwareProject">An instance of the Project class.</param>
        public static FileSystemOperationStatus LoadProject(string filename, out Project softwareProject)
        {
            String input = string.Empty;

            try
            {
                // Open the file
                System.IO.StreamReader r = new System.IO.StreamReader(filename);

                // Read the file contents
                input = r.ReadToEnd();

                r.Close();

                softwareProject = JsonConvert.DeserializeObject<Project>(input);

                // Check the version of the project file: if not supported, abort the operation
                if (softwareProject.Version != ApplicationSettings.ProjectFileFormatVersion)
                {
                    softwareProject = null;
                    return FileSystemOperationStatus.ProjectLoadErrorUnsupportedFormat;
                }

                // Insert the path and filename into the project
                softwareProject.Location = Path.GetDirectoryName(filename);
                softwareProject.Filename = Path.GetFileName(filename);
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                softwareProject = null;
                return FileSystemOperationStatus.ProjectLoadErrorDirectoryNotFound;
            }
            catch (System.IO.FileNotFoundException) // The file does not exist
            {
                softwareProject = null;
                return FileSystemOperationStatus.ProjectLoadErrorFileNotFound;
            }
            catch (System.IO.PathTooLongException) // The path is too long
            {
                softwareProject = null;
                return FileSystemOperationStatus.ProjectLoadErrorPathTooLong;
            }
            catch (JsonException) // Error deserializing the file
            {
                softwareProject = null;
                return FileSystemOperationStatus.ProjectLoadErrorDeserialization;
            }
            catch // General input/output error
            {
                softwareProject = null;
                return FileSystemOperationStatus.ProjectLoadErrorIO;
            }

            return FileSystemOperationStatus.OK;
        }

        /* ** remover *** este código passou para Project
        /// <summary>
        /// Export the issues and tasks of a project to a file.
        /// </summary>
        /// <param name="softwareProject">An instance of the Project class.</param>
        //public static FileSystemOperationStatus ExportProject(string fileNameIssues, string fileNameTasks, in Project softwareProject)
        public static ExportProjectResult ExportProject(string fileNameIssues, string fileNameTasks, in Project softwareProject)
        {
            ExportProjectResult Result = new ExportProjectResult(true, fileNameIssues, true, fileNameTasks, FileSystemOperationStatus.None, FileSystemOperationStatus.None);

            // Export the issues
            if (softwareProject.Issues != null)
            {
                if ((Result.IssuesError = ExportIssues(fileNameIssues, softwareProject)) != FileSystemOperationStatus.ExportOK)
                {
                    Result.IssuesOK = false;
                }
            }

            // Export the tasks
            if (softwareProject.Tasks != null)
            {
                //ExportTasks(fileNameTasks, softwareProject);
                if ((Result.TasksError = ExportTasks(fileNameTasks, softwareProject)) != FileSystemOperationStatus.ExportOK)
                {
                    Result.TasksOK = false;
                }
            }

            //return FileSystemOperationStatus.ExportOK;
            return Result;
        }

        /// <summary>
        /// Export the project issues.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="softwareProject"></param>
        /// <returns></returns>
        private static FileSystemOperationStatus ExportIssues(string fileName, in Project softwareProject)
        {
            try
            {
                using (var writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(softwareProject.Issues.Values);
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
        private static FileSystemOperationStatus ExportTasks(string fileName, in Project softwareProject)
        {
            try
            {
                using (var writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(softwareProject.Tasks.Values);
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
        */
    }
}
