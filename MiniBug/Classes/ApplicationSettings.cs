// Copyright(c) João Martiniano. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiniBug
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// This application uses project files of this version.
        /// </summary>
        public const string ProjectFileFormatVersion = "1.0";

        /// <summary>
        /// Maximum number of recent projects to hold in a list.
        /// </summary>
        public const int MaxRecentProjects = 8;

        /// <summary>
        /// Show/hide borders around cells in the DataGridViews.
        /// </summary>
        public static bool GridShowBorders = true;

        /// <summary>
        /// DataGridView border color.
        /// </summary>
        public static Color GridBorderColor = ColorTranslator.FromHtml("#C5C5C5");

        /// <summary>
        /// DataGridView selection background color.
        /// </summary>
        public static Color GridSelectionBackColor = ColorTranslator.FromHtml("#ACD4FD");

        /// <summary>
        /// DataGridView selection foreground color.
        /// </summary>
        public static Color GridSelectionForeColor = ColorTranslator.FromHtml("#000000");

        /// <summary>
        /// Show/hide rows of the DataGridViews in alternating colors.
        /// </summary>
        public static bool GridAlternatingRowColor = false;

        /// <summary>
        /// DataGridView normal row background color.
        /// </summary>
        public static Color GridRowBackColor = Color.White;

        /// <summary>
        /// DataGridView alternate row background color.
        /// </summary>
        public static Color GridAlternateRowBackColor = Color.White;

        /// <summary>
        /// DataGridView font name and size.
        /// </summary>
        public static Font GridFont = new Font("Segoe UI", 8);

        /// <summary>
        /// Size of the colored rectangle in the status column of the DataGridViews.
        /// </summary>
        public const int GridStatusRectangleSize = 8;

        /// <summary>
        /// DataGridView foreground color of closed issues and tasks.
        /// </summary>
        public static Color GridClosedItem = ColorTranslator.FromHtml("#636363");

        /// <summary>
        /// Colors for the status column in the Issues DataGridView.
        /// </summary>
        public static Dictionary<IssueStatus, Brush> IssueStatusColors = new Dictionary<IssueStatus, Brush>() {
            { IssueStatus.None, Brushes.Transparent },
            { IssueStatus.Unconfirmed, Brushes.Transparent },
            { IssueStatus.Confirmed, Brushes.Goldenrod },
            { IssueStatus.InProgress, Brushes.Blue },
            { IssueStatus.Resolved, Brushes.ForestGreen },
            { IssueStatus.Closed, Brushes.Gray }
        };

        /// <summary>
        /// Colors for the status column in the Tasks DataGridView.
        /// </summary>
        public static Dictionary<TaskStatus, Brush> TaskStatusColors = new Dictionary<TaskStatus, Brush>() {
            { TaskStatus.None, Brushes.Transparent },
            { TaskStatus.NotStarted, Brushes.Crimson },
            { TaskStatus.InProgress, Brushes.Blue },
            { TaskStatus.Finished, Brushes.ForestGreen }
        };

        /// <summary>
        /// The columns of the issues DataGridView.
        /// </summary>
        public static Dictionary<IssueFieldsUI, GridColumn> GridIssuesColumns = new Dictionary<IssueFieldsUI, GridColumn>()
        {
            { IssueFieldsUI.ID,
                new GridColumn { Name = "id", HeaderText = "ID", Visible = true, DisplayIndex = 1, Description = "Unique numerical code of the issue" } },

            { IssueFieldsUI.Priority,
                new GridColumn { Name = "priority", HeaderText = "", Visible = true, DisplayIndex = 0, Description = "Priority of an issue" } },

            { IssueFieldsUI.Status,
                new GridColumn { Name = "status", HeaderText = "Status", Visible = true, DisplayIndex = 2, Description = "Current status of the issue" } },

            { IssueFieldsUI.Version,
                new GridColumn { Name = "version", HeaderText = "Version", Visible = true, DisplayIndex = 3, Description = "Version where the issue was detected" } },

            { IssueFieldsUI.TargetVersion,
                new GridColumn { Name = "target", HeaderText = "Target", Visible = false, DisplayIndex = 4, Description = "Version where the issue must be resolved" } },

            { IssueFieldsUI.Summary,
                new GridColumn { Name = "summary", HeaderText = "Summary", Visible = true, DisplayIndex = 5, Description = "Brief summary of the issue" } },

            { IssueFieldsUI.DateCreated,
                new GridColumn { Name = "created", HeaderText = "Created", Visible = true, DisplayIndex = 6, Description = "Date/time when the issue was created" } },

            { IssueFieldsUI.DateModified,
                new GridColumn { Name = "modified", HeaderText = "Modified", Visible = false, DisplayIndex = 7, Description = "Date/time when the issue was last modified" } }
        };

        /// <summary>
        /// The columns of the tasks DataGridView.
        /// </summary>
        public static Dictionary<TaskFieldsUI, GridColumn> GridTasksColumns = new Dictionary<TaskFieldsUI, GridColumn>()
        {
            { TaskFieldsUI.ID,
                new GridColumn { Name = "id", HeaderText = "ID", Visible = true, DisplayIndex = 1, Description = "Unique numerical code of the task" } },

            { TaskFieldsUI.Priority,
                new GridColumn { Name = "priority", HeaderText = "", Visible = true, DisplayIndex = 0, Description = "Priority of a task" } },

            { TaskFieldsUI.Status,
                new GridColumn { Name = "status", HeaderText = "Status", Visible = true, DisplayIndex = 2, Description = "Current status of the task" } },

            { TaskFieldsUI.TargetVersion,
                new GridColumn { Name = "target", HeaderText = "Target", Visible = true, DisplayIndex = 3, Description = "Version where the task must be resolved" } },

            { TaskFieldsUI.Summary,
                new GridColumn { Name = "summary", HeaderText = "Summary", Visible = true, DisplayIndex = 4, Description = "Brief summary of the task" } },

            { TaskFieldsUI.DateCreated,
                new GridColumn { Name = "created", HeaderText = "Created", Visible = true, DisplayIndex = 5, Description = "Date/time when the task was created" } },

            { TaskFieldsUI.DateModified,
                new GridColumn { Name = "modified", HeaderText = "Modified", Visible = false, DisplayIndex = 6, Description = "Date/time when the task was last modified" } }
        };

        /// <summary>
        /// Sort settings for the issues DataGridView.
        /// </summary>
        public static GridIssuesSortSettings GridIssuesSort = new GridIssuesSortSettings(IssueFieldsUI.ID, System.Windows.Forms.SortOrder.Ascending, null, null);

        /// <summary>
        /// Sort settings for the tasks DataGridView.
        /// </summary>
        public static GridTasksSortSettings GridTasksSort = new GridTasksSortSettings(TaskFieldsUI.ID, System.Windows.Forms.SortOrder.Ascending, null, null);

        /// <summary>
        /// Determines which settings to save.
        /// </summary>
        public enum SaveSettings
        {
            /// <summary>Save all settings.</summary>
            All = 0,
            /// <summary>Save only the settings which are modified via the Settings form.</summary>
            UserModifiable,
            /// <summary>Save only the column order and sort settings of the issues and tasks DataGridViews.</summary>
            ColumnOrderSort
        }

        /// <summary>
        /// Retrieve the application settings.
        /// </summary>
        public static void Load()
        {
            GridShowBorders = Properties.Settings.Default.GridShowBorders;
            GridBorderColor = Properties.Settings.Default.GridBorderColor;
            GridSelectionBackColor = Properties.Settings.Default.GridSelectionBackColor;
            GridSelectionForeColor = Properties.Settings.Default.GridSelectionForeColor;
            GridAlternatingRowColor = Properties.Settings.Default.GridAlternatingRowColor;
            GridRowBackColor = Properties.Settings.Default.GridRowBackColor;
            GridAlternateRowBackColor = Properties.Settings.Default.GridAlternateRowBackColor;
            GridFont = Properties.Settings.Default.GridFont;

            // Load the settings for the issues DataGridView columns
            if (Properties.Settings.Default.GridIssuesColumnsSettings != null)
            {
                IssueFieldsUI Col;

                // *** debug
                Console.WriteLine("1: ApplicationSettings.cs: Read from Windows settings...");

                foreach (string item in Properties.Settings.Default.GridIssuesColumnsSettings)
                {
                    string[] s = item.Split(',');

                    Col = (IssueFieldsUI)Convert.ToInt32(s[0]);
                    ApplicationSettings.GridIssuesColumns[Col].Visible = Convert.ToBoolean(s[1]);
                    ApplicationSettings.GridIssuesColumns[Col].DisplayIndex = Convert.ToInt32(s[2]);

                    // *** debug
                    Console.WriteLine("Item: {0} - Display index: {1}", Col.ToString(), s[2]);
                }
            }

            // *** debug
            /*Console.WriteLine("2: ApplicationSettings.cs: What's stored in ApplicationSettings...");
            foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                Console.WriteLine("Item: {0} - Display index: {1}", item.Value.Name, item.Value.DisplayIndex);
            }*/
            HelperClass.DebugDisplayIndex("2: ApplicationSettings.cs: ApplicationSettings.GridIssuesColumns");


            // Load the settings for the tasks DataGridView columns
            if (Properties.Settings.Default.GridTasksColumnsSettings != null)
            {
                TaskFieldsUI Col;

                foreach (string item in Properties.Settings.Default.GridTasksColumnsSettings)
                {
                    string[] s = item.Split(',');

                    Col = (TaskFieldsUI)Convert.ToInt32(s[0]);
                    ApplicationSettings.GridTasksColumns[Col].Visible = Convert.ToBoolean(s[1]);
                    ApplicationSettings.GridTasksColumns[Col].DisplayIndex = Convert.ToInt32(s[2]);
                }
            }

            // Load the issues DataGridView sort settings
            if (Properties.Settings.Default.GridIssuesSort != null)
            {
                GridIssuesSort.FirstColumn = (IssueFieldsUI)Properties.Settings.Default.GridIssuesSort[0];
                GridIssuesSort.FirstColumnSortOrder = (System.Windows.Forms.SortOrder)Properties.Settings.Default.GridIssuesSort[1];

                // If the setting has the value -1, it is null
                if (Properties.Settings.Default.GridIssuesSort[2] != -1)
                {
                    GridIssuesSort.SecondColumn = (IssueFieldsUI)Properties.Settings.Default.GridIssuesSort[2];
                }
                else
                {
                    GridIssuesSort.SecondColumn = null;
                }

                // If the setting has the value -1, it is null
                if (Properties.Settings.Default.GridIssuesSort[3] != -1)
                {
                    GridIssuesSort.SecondColumnSortOrder = (System.Windows.Forms.SortOrder)Properties.Settings.Default.GridIssuesSort[3];
                }
                else
                {
                    GridIssuesSort.SecondColumnSortOrder = null;
                }
            }

            // Load the tasks DataGridView sort settings
            if (Properties.Settings.Default.GridTasksSort != null)
            {
                GridTasksSort.FirstColumn = (TaskFieldsUI)Properties.Settings.Default.GridTasksSort[0];
                GridTasksSort.FirstColumnSortOrder = (System.Windows.Forms.SortOrder)Properties.Settings.Default.GridTasksSort[1];

                // If the setting has the value -1, it is null
                if (Properties.Settings.Default.GridTasksSort[2] != -1)
                {
                    GridTasksSort.SecondColumn = (TaskFieldsUI)Properties.Settings.Default.GridTasksSort[2];
                }
                else
                {
                    GridTasksSort.SecondColumn = null;
                }

                // If the setting has the value -1, it is null
                if (Properties.Settings.Default.GridTasksSort[3] != -1)
                {
                    GridTasksSort.SecondColumnSortOrder = (System.Windows.Forms.SortOrder)Properties.Settings.Default.GridTasksSort[3];
                }
                else
                {
                    GridTasksSort.SecondColumnSortOrder = null;
                }
            }
        }

        /// <summary>
        /// Save the application settings.
        /// </summary>
        public static void Save(SaveSettings settingsSelection = SaveSettings.All)
        {
            // User modifiable settings
            if ((settingsSelection == SaveSettings.All) || (settingsSelection == SaveSettings.UserModifiable))
            {
                Properties.Settings.Default.GridShowBorders = GridShowBorders;
                Properties.Settings.Default.GridBorderColor = GridBorderColor;
                Properties.Settings.Default.GridSelectionBackColor = GridSelectionBackColor;
                Properties.Settings.Default.GridSelectionForeColor = GridSelectionForeColor;
                Properties.Settings.Default.GridAlternatingRowColor = GridAlternatingRowColor;
                Properties.Settings.Default.GridRowBackColor = GridRowBackColor;
                Properties.Settings.Default.GridAlternateRowBackColor = GridAlternateRowBackColor;
                Properties.Settings.Default.GridFont = GridFont;
            }

            // Save some settings for the issues and tasks DataGridView columns
            if ((settingsSelection == SaveSettings.All) || (settingsSelection == SaveSettings.ColumnOrderSort))
            {
                string s  = string.Empty;

                // The settings are saved as a string collection, in the following format:
                //    "column ID,visibility,display index"
                Properties.Settings.Default.GridIssuesColumnsSettings = new System.Collections.Specialized.StringCollection();

                foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
                {
                    s = $"{Convert.ToInt32(item.Key).ToString()},{item.Value.Visible.ToString()},{item.Value.DisplayIndex.ToString()}";
                    Properties.Settings.Default.GridIssuesColumnsSettings.Add(s);

                    // *** debug
                    Console.WriteLine("Item: {0} - Display index: {1}", item.Key.ToString(), item.Value.DisplayIndex);
                }

                s = string.Empty;

                // The settings are saved as a string collection, in the following format:
                //    "column ID,visibility,display index"
                Properties.Settings.Default.GridTasksColumnsSettings = new System.Collections.Specialized.StringCollection();

                foreach (KeyValuePair<TaskFieldsUI, GridColumn> item in ApplicationSettings.GridTasksColumns)
                {
                    s = $"{Convert.ToInt32(item.Key).ToString()},{item.Value.Visible.ToString()},{item.Value.DisplayIndex.ToString()}";
                    Properties.Settings.Default.GridTasksColumnsSettings.Add(s);
                }
            }

            // Save the issues and tasks DataGridView sort settings
            if ((settingsSelection == SaveSettings.All) || (settingsSelection == SaveSettings.ColumnOrderSort))
            {
                // Issues
                Properties.Settings.Default.GridIssuesSort = new int[4];
                Properties.Settings.Default.GridIssuesSort[0] = (int)GridIssuesSort.FirstColumn;
                Properties.Settings.Default.GridIssuesSort[1] = (int)GridIssuesSort.FirstColumnSortOrder;
                Properties.Settings.Default.GridIssuesSort[2] = (GridIssuesSort.SecondColumn != null) ? (int)GridIssuesSort.SecondColumn : -1;
                Properties.Settings.Default.GridIssuesSort[3] = (GridIssuesSort.SecondColumnSortOrder != null) ? (int)GridIssuesSort.SecondColumnSortOrder : -1;

                // Tasks
                Properties.Settings.Default.GridTasksSort = new int[4];
                Properties.Settings.Default.GridTasksSort[0] = (int)GridTasksSort.FirstColumn;
                Properties.Settings.Default.GridTasksSort[1] = (int)GridTasksSort.FirstColumnSortOrder;
                Properties.Settings.Default.GridTasksSort[2] = (GridTasksSort.SecondColumn != null) ? (int)GridTasksSort.SecondColumn : -1;
                Properties.Settings.Default.GridTasksSort[3] = (GridTasksSort.SecondColumnSortOrder != null) ? (int)GridTasksSort.SecondColumnSortOrder : -1;
            }

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Sets application default values for the properties that can be changed by the user.
        /// </summary>
        public static void SetDefaultValues()
        {
            GridShowBorders = true;
            GridBorderColor = ColorTranslator.FromHtml("#C5C5C5");
            GridSelectionBackColor = ColorTranslator.FromHtml("#ACD4FD");
            GridSelectionForeColor = ColorTranslator.FromHtml("#000000");
            GridAlternatingRowColor = false;
            GridRowBackColor = Color.White;
            GridAlternateRowBackColor = Color.White;
            GridFont = new Font("Segoe UI", 8);
        }
    }
}
