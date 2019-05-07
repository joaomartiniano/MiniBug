// Copyright(c) João Martiniano. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBug
{
    public enum ImportExportOperation { None = 0, Import, Export }

    /// <summary>
    /// Detail information about a specific import/export operation.
    /// </summary>
    public class ImportExportResultData
    {
        public string FileName { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        private FileSystemOperationStatus _result;

        public FileSystemOperationStatus Result
        {
            get { return _result; }
            set { _result = value; ComposeMessage(); }
        }

        public string ResultMessage { get; set; } = string.Empty;

        public string ResultDescription { get; set; } = string.Empty;

        public string ResultSolution { get; set; } = string.Empty;

        private void ComposeMessage()
        {
            switch (Result)
            {
                case FileSystemOperationStatus.ExportOK:
                    ResultMessage = "Export successful";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorDirectoryNotFound:
                    ResultMessage = "Location not found";
                    ResultDescription = "The specified location does not exist.";
                    ResultSolution = "Choose a different location.";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorPathTooLong:
                    ResultMessage = "Path too long";
                    ResultDescription = "The path, file name or both are too long.";
                    ResultSolution = "Choose a different location and/or a shorter filename.";
                    break;

                case FileSystemOperationStatus.ExportToCsvErrorExporterComponent:
                    ResultMessage = "Component error";
                    ResultDescription = "The component responsible for exporting the project has failed.";
                    ResultSolution = "Please try again.";
                    break;

                case FileSystemOperationStatus.ExportToCsvIOError:
                    ResultMessage = "I/O Error";
                    ResultDescription = "There was a general input/output error while attempting to export.";
                    ResultSolution = "Choose a different drive/device to export the project.";
                    break;

                default:
                    ResultMessage = string.Empty;
                    ResultDescription = string.Empty;
                    ResultSolution = string.Empty;
                    break;
            }
        }
    }

    /// <summary>
    /// Result of an import/export operation. Contains details about the import/export operation.
    /// </summary>
    public class ImportExportResult
    {
        public ImportExportOperation Operation { get; set; } = ImportExportOperation.None;

        public ImportExportResultData Issues { get; set; } = null;

        public ImportExportResultData Tasks { get; set; } = null;

        public ImportExportResult(ImportExportOperation operation)
        {
            Operation = operation;

            Issues = new ImportExportResultData();
            Tasks = new ImportExportResultData();
        }
    }
}
