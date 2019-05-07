// Copyright(c) João Martiniano. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBug
{
    public class IssuesDataGridViewRowComparer : System.Collections.IComparer
    {
        private static int sortOrderModifierColumn1 = 1;
        private static int sortOrderModifierColumn2 = 1;

        public IssuesDataGridViewRowComparer(SortOrder sortOrder)
        {
            if (ApplicationSettings.GridIssuesSort.FirstColumnSortOrder == SortOrder.Descending)
            {
                sortOrderModifierColumn1 = -1;
            }
            else if (ApplicationSettings.GridIssuesSort.FirstColumnSortOrder == SortOrder.Ascending)
            {
                sortOrderModifierColumn1 = 1;
            }

            if (ApplicationSettings.GridIssuesSort.SecondColumnSortOrder != null)
            {
                if (ApplicationSettings.GridIssuesSort.SecondColumnSortOrder == SortOrder.Descending)
                {
                    sortOrderModifierColumn2 = -1;
                }
                else if (ApplicationSettings.GridIssuesSort.SecondColumnSortOrder == SortOrder.Ascending)
                {
                    sortOrderModifierColumn2 = 1;
                }
            }            
        }

        private int CompareValues(IssueFieldsUI fieldType, object field1, object field2)
        {
            switch (fieldType)
            {
                case IssueFieldsUI.ID:
                case IssueFieldsUI.Status:
                case IssueFieldsUI.Priority:
                    int value1 = Convert.ToInt32(field1);
                    int value2 = Convert.ToInt32(field2);

                    if (value1 < value2)
                    {
                        return -1;
                    }
                    else if (value1 == value2)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }

                case IssueFieldsUI.Version:
                case IssueFieldsUI.TargetVersion:
                case IssueFieldsUI.Summary:
                    return string.Compare(field1.ToString(), field2.ToString());

                case IssueFieldsUI.DateCreated:
                case IssueFieldsUI.DateModified:
                    DateTime dtValue1, dtValue2;
                    
                    if ((DateTime.TryParse(field1.ToString(), out dtValue1)) && (DateTime.TryParse(field2.ToString(), out dtValue2)))
                    {
                        if (dtValue1 < dtValue2)
                        {
                            return -1;
                        }
                        else if (dtValue1 == dtValue2)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }

                    return 0;

                default:
                    return 0;
            }
        }

        private object GetValueForField(IssueFieldsUI column, int id)
        {
            switch (column)
            {
                case IssueFieldsUI.ID:
                    return id;
                case IssueFieldsUI.Priority:
                    return Program.SoftwareProject.Issues[id].Priority;
                case IssueFieldsUI.Status:
                    return Program.SoftwareProject.Issues[id].Status;
                case IssueFieldsUI.Version:
                    return Program.SoftwareProject.Issues[id].Version;
                case IssueFieldsUI.TargetVersion:
                    return Program.SoftwareProject.Issues[id].TargetVersion;
                case IssueFieldsUI.Summary:
                    return Program.SoftwareProject.Issues[id].Summary;
                case IssueFieldsUI.DateCreated:
                    return Program.SoftwareProject.Issues[id].DateCreated;
                case IssueFieldsUI.DateModified:
                    return Program.SoftwareProject.Issues[id].DateModified;
                default:
                    return null;
            }
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            int CompareResult = 0;
            object field1, field2;

            int id1 = Convert.ToInt32(DataGridViewRow1.Cells[0].Value);
            int id2 = Convert.ToInt32(DataGridViewRow2.Cells[0].Value);

            // Get the values for the first and second fields, in the first column
            field1 = GetValueForField(ApplicationSettings.GridIssuesSort.FirstColumn, id1);
            field2 = GetValueForField(ApplicationSettings.GridIssuesSort.FirstColumn, id2);

            CompareResult = CompareValues(ApplicationSettings.GridIssuesSort.FirstColumn, field1, field2);

            // Regarding sort column 1, one of the rows is either lower or greater than the other
            if ((CompareResult == -1) || (CompareResult == 1))
            {
                return CompareResult * sortOrderModifierColumn1;
            }
            else
            {
                // Both rows are equal (regarding sort column 1)

                // If only sorting by one column, then both rows are equal
                if (ApplicationSettings.GridIssuesSort.SecondColumn == null)
                {
                    // Resort to ID to give some final order
                    return ((id1 < id2) ? -1 : 1);
                }

                // Get the values for the first and second fields, in the second column
                field1 = GetValueForField(ApplicationSettings.GridIssuesSort.SecondColumn.Value, id1);
                field2 = GetValueForField(ApplicationSettings.GridIssuesSort.SecondColumn.Value, id2);

                CompareResult = CompareValues(ApplicationSettings.GridIssuesSort.SecondColumn.Value, field1, field2);

                if ((CompareResult == -1) || (CompareResult == 1))
                {
                    return CompareResult * sortOrderModifierColumn2;
                }
                else
                {
                    // Both rows are equal: resort to ID to give some final order
                    return ((id1 < id2) ? -1 : 1);
                }
            }
        }
    }
}
