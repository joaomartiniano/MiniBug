using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MiniBug
{
    public enum FileOperationsStatus { None = 0, Success, DirectoryNotFound, FileNotFound, IOError, PathTooLong }

    public static class ApplicationData
    {
        /// <summary>
        /// Saves a project data to a file. The file is overwritten.
        /// </summary>
        /// <param name="softwareProject">An instance of the Project class.</param>
        public static FileOperationsStatus SaveProject(in Project softwareProject)
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

        /// <summary>
        /// Load application data.
        /// </summary>
        /// <param name="filename">Name and location of the project file.</param>
        /// <param name="softwareProject">An instance of the Project class.</param>
        public static FileOperationsStatus LoadProject(string filename, out Project softwareProject)
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
                //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);
            }
            catch (System.IO.DirectoryNotFoundException) // The directory does not exist
            {
                softwareProject = null;
                return FileOperationsStatus.DirectoryNotFound;
            }
            catch (System.IO.FileNotFoundException) // The file does not exist
            {
                softwareProject = null;
                return FileOperationsStatus.FileNotFound;
            }
            catch (System.IO.PathTooLongException) // The path is too long
            {
                softwareProject = null;
                return FileOperationsStatus.PathTooLong;
            }
            catch
            {
                softwareProject = null;
                return FileOperationsStatus.IOError;
            }

            return FileOperationsStatus.Success;
        }
    }
}
