using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBug
{
    public class GridIssuesSortSettings
    {
        public IssueFieldsUI FirstColumn { get; set; }
        public SortOrder FirstColumnSortOrder { get; set; }

        public IssueFieldsUI? SecondColumn { get; set; }
        public SortOrder? SecondColumnSortOrder { get; set; }

        public GridIssuesSortSettings(IssueFieldsUI firstColumn, SortOrder firstColumnSortOrder, IssueFieldsUI? secondColumn, SortOrder? secondColumnOrder)
        {
            FirstColumn = firstColumn;
            FirstColumnSortOrder = firstColumnSortOrder;
            SecondColumn = secondColumn;
            SecondColumnSortOrder = secondColumnOrder;
        }
    }
}
