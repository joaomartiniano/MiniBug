using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBug
{
    public class DataGridViewRowComparer : System.Collections.IComparer
    {
        private static int sortOrderModifier = 1;

        public DataGridViewRowComparer(SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == SortOrder.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        /* Issues: Campos
	            ID -> comparação numérica
	            Priority -> (imagem) (?)
	            Status -> (?)
	            Version -> string
	            Target Version -> string
	            Summary -> string
	            DateCreated -> ?
	            DateModified -> ?
         */

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            int CompareResult = 0;

            // Compare the first column
            
            switch (ApplicationSettings.GridIssuesSort.FirstColumn)
            {
                case IssueFieldsUI.ID:
                    int value1 = Int32.Parse(DataGridViewRow1.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.ID].Name].Value.ToString());
                    int value2 = Int32.Parse(DataGridViewRow2.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.ID].Name].Value.ToString());

                    if (value1 < value2) CompareResult = -1;
                    else if (value1 == value2) CompareResult = 0;
                    else CompareResult = 1;

                    break;

                case IssueFieldsUI.Version:
                    CompareResult = string.Compare(DataGridViewRow1.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Version].Name].Value.ToString(),
                                                   DataGridViewRow2.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Version].Name].Value.ToString());

                    break;

                case IssueFieldsUI.TargetVersion:
                    CompareResult = string.Compare(DataGridViewRow1.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.TargetVersion].Name].Value.ToString(),
                                                   DataGridViewRow2.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.TargetVersion].Name].Value.ToString());

                    break;

                case IssueFieldsUI.Summary:
                    CompareResult = string.Compare(DataGridViewRow1.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Summary].Name].Value.ToString(),
                                                   DataGridViewRow2.Cells[ApplicationSettings.GridIssuesColumns[IssueFieldsUI.Summary].Name].Value.ToString());

                    break;

                case IssueFieldsUI.DateCreated:

                    break;
            }

            // Compare the second column

            if (ApplicationSettings.GridIssuesSort.SecondColumn != null)
            {

            }

            return CompareResult * sortOrderModifier;
        }
       
    }
}
