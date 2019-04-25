using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBug
{
    public class GridTasksSortSettings
    {
        public TaskFieldsUI FirstColumn { get; set; }
        public SortOrder FirstColumnSortOrder { get; set; }

        public TaskFieldsUI? SecondColumn { get; set; }
        public SortOrder? SecondColumnSortOrder { get; set; }

        public GridTasksSortSettings(TaskFieldsUI firstColumn, SortOrder firstColumnSortOrder, TaskFieldsUI? secondColumn, SortOrder? secondColumnOrder)
        {
            FirstColumn = firstColumn;
            FirstColumnSortOrder = firstColumnSortOrder;
            SecondColumn = secondColumn;
            SecondColumnSortOrder = secondColumnOrder;
        }
    }
}
