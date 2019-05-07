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
    public class GridTasksSortSettings : IEquatable<GridTasksSortSettings>
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

        public override bool Equals(object obj)
        {
            return this.Equals(obj as GridTasksSortSettings);
        }

        public bool Equals(GridTasksSortSettings s)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(s, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, s))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != s.GetType())
            {
                return false;
            }

            // Return true if all fields match.
            bool test1 = (FirstColumn == s.FirstColumn) && (FirstColumnSortOrder == s.FirstColumnSortOrder);
            bool test2 = (SecondColumn == s.SecondColumn) && (SecondColumnSortOrder == s.SecondColumnSortOrder);

            return test1 && test2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + FirstColumn.GetHashCode();
                hash = hash * 23 + FirstColumnSortOrder.GetHashCode();
                hash = hash * 23 + ((SecondColumn == null) ? 0 : SecondColumn.GetHashCode());
                hash = hash * 23 + ((SecondColumnSortOrder == null) ? 0 : SecondColumnSortOrder.GetHashCode());
                return hash;
            }
        }
    }
}
