using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    static class DataTableHelper
    {
        public static string ConvertDataTableToString(DataTable dataTable, bool withFormula = false)
        {
            string headers = string.Join("; ", dataTable.Columns.OfType<DataColumn>().Select(x => x.ColumnName));
            string body = string.Join(Environment.NewLine, dataTable.Rows.OfType<DataRow>().Select(x => string.Join("; ", x.ItemArray.OfType<TableCell>().Select(y => y.GetString(withFormula)))));
            return headers + Environment.NewLine + body;
        }
    }
}
