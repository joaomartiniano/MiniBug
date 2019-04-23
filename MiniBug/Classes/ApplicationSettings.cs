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
                new GridColumn { Name = "id", HeaderText = "ID", Visible = true, DisplayIndex = 0, SortOrder = 0, Description = "Unique numerical code of the issue" } },

            { IssueFieldsUI.Priority,
                new GridColumn { Name = "priority", HeaderText = "", Visible = true, DisplayIndex = 1, SortOrder = 0, Description = "Priority of an issue" } },

            { IssueFieldsUI.Status,
                new GridColumn { Name = "status", HeaderText = "Status", Visible = true, DisplayIndex = 2, SortOrder = 0, Description = "Current status of the issue" } },

            { IssueFieldsUI.Version,
                new GridColumn { Name = "version", HeaderText = "Version", Visible = true, DisplayIndex = 3, SortOrder = 0, Description = "Version where the issue was detected" } },

            { IssueFieldsUI.TargetVersion,
                new GridColumn { Name = "target", HeaderText = "Target", Visible = false, DisplayIndex = 4, SortOrder = 0, Description = "Version where the issue must be resolved" } },

            { IssueFieldsUI.Summary,
                new GridColumn { Name = "summary", HeaderText = "Summary", Visible = true, DisplayIndex = 5, SortOrder = 0, Description = "Brief summary of the issue" } },

            { IssueFieldsUI.DateCreated,
                new GridColumn { Name = "created", HeaderText = "Created", Visible = true, DisplayIndex = 6, SortOrder = 0, Description = "Date/time when the issue was created" } },

            { IssueFieldsUI.DateModified,
                new GridColumn { Name = "modified", HeaderText = "Modified", Visible = false, DisplayIndex = 7, SortOrder = 0, Description = "Date/time when the issue was last modified" } }
        };

        /// <summary>
        /// The columns of the tasks DataGridView.
        /// </summary>
        public static Dictionary<TaskFieldsUI, GridColumn> GridTasksColumns = new Dictionary<TaskFieldsUI, GridColumn>()
        {
            { TaskFieldsUI.ID,
                new GridColumn { Name = "id", HeaderText = "ID", Visible = true, DisplayIndex = 0, SortOrder = 0, Description = "Unique numerical code of the task" } },

            { TaskFieldsUI.Priority,
                new GridColumn { Name = "priority", HeaderText = "", Visible = true, DisplayIndex = 1, SortOrder = 0, Description = "Priority of a task" } },

            { TaskFieldsUI.Status,
                new GridColumn { Name = "status", HeaderText = "Status", Visible = true, DisplayIndex = 2, SortOrder = 0, Description = "Current status of the task" } },

            { TaskFieldsUI.TargetVersion,
                new GridColumn { Name = "target", HeaderText = "Target", Visible = false, DisplayIndex = 3, SortOrder = 0, Description = "Version where the task must be resolved" } },

            { TaskFieldsUI.Summary,
                new GridColumn { Name = "summary", HeaderText = "Summary", Visible = true, DisplayIndex = 4, SortOrder = 0, Description = "Brief summary of the task" } },

            { TaskFieldsUI.DateCreated,
                new GridColumn { Name = "created", HeaderText = "Created", Visible = true, DisplayIndex = 5, SortOrder = 0, Description = "Date/time when the task was created" } },

            { TaskFieldsUI.DateModified,
                new GridColumn { Name = "modified", HeaderText = "Modified", Visible = false, DisplayIndex = 6, SortOrder = 0, Description = "Date/time when the task was last modified" } }
        };

        /// <summary>
        /// Sort settings for the issues DataGridView.
        /// </summary>
        public static GridIssuesSortSettings GridIssuesSort = new GridIssuesSortSettings(IssueFieldsUI.ID, System.Windows.Forms.SortOrder.Ascending, null, null);

        // *** incluir sort settings para as tasks ***

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

            // Load the issues DataGridView sort settings
            if (Properties.Settings.Default.GridIssuesSort != null)
            {
                GridIssuesSort.FirstColumn = (IssueFieldsUI)Properties.Settings.Default.GridIssuesSort[0];
                GridIssuesSort.FirstColumnSortOrder = (System.Windows.Forms.SortOrder)Properties.Settings.Default.GridIssuesSort[1];
                GridIssuesSort.SecondColumn = (IssueFieldsUI)Properties.Settings.Default.GridIssuesSort[2];
                GridIssuesSort.SecondColumnSortOrder = (System.Windows.Forms.SortOrder)Properties.Settings.Default.GridIssuesSort[3];
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

            // Issues and tasks DataGridViews column order
            // *** (testar melhor, quando o refactoring e o sort estiverem concluídos)
            /*if ((settingsSelection == SaveSettings.All) || (settingsSelection == SaveSettings.ColumnOrder))
            {
                int i = 0;
                int[] order = new int[ApplicationSettings.GridIssuesColumns.Count()];

                // ********* em edição
                // *** falta: obter ordenados pela DisplayIndex???
                foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
                {
                    order[i++] = Convert.ToInt32(item.Key);
                }

                Properties.Settings.Default.GridIssuesColumnOrder = new int[ApplicationSettings.GridIssuesColumns.Count()];
                Properties.Settings.Default.GridIssuesColumnOrder = order;
            }*/

            // Save the issues and tasks DataGridView sort settings
            if ((settingsSelection == SaveSettings.All) || (settingsSelection == SaveSettings.ColumnOrderSort))
            {
                Properties.Settings.Default.GridIssuesSort = new int[4];
                Properties.Settings.Default.GridIssuesSort[0] = (int)GridIssuesSort.FirstColumn;
                Properties.Settings.Default.GridIssuesSort[1] = (int)GridIssuesSort.FirstColumnSortOrder;
                Properties.Settings.Default.GridIssuesSort[2] = (int)GridIssuesSort.SecondColumn;
                Properties.Settings.Default.GridIssuesSort[3] = (int)GridIssuesSort.SecondColumnSortOrder;

                // *** gravar para tasks
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
