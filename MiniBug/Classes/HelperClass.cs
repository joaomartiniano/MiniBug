using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBug
{
    public enum OperationType { None = 0, New, Edit, Delete };

    public static class HelperClass
    {
        public static void DebugDisplayIndex(string message)
        {
            Console.WriteLine(message);
            foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                Console.WriteLine("    Item: {0} - Display index: {1}", item.Value.Name, item.Value.DisplayIndex);
            }
        }
    }
}
