using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MiniBug
{
    public enum FileOperationsStatus { None = 0, Success, DirectoryNotFound, FileNotFound, IOError, PathTooLong }

    /// <summary>
    /// Stores the application data and is responsible for loading and saving it.
    /// </summary>
    class ApplicationData
    {
        public static string Filename { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        public static List<Issue> Issues { get; set; } = new List<Issue>();

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        public static List<Task> Tasks { get; set; } = new List<Task>();

        /// <summary>
        /// Load application data.
        /// </summary>
        public static FileOperationsStatus Load()
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
        public static FileOperationsStatus Save()
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
