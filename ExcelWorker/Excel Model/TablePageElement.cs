using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    public class TablePageElement : BasePageElement
    {
        public DataTable dataTable;

        public TablePageElement(int startRow, int endRow, DataTable dataTable)
        {
            elemntTape = EElemntsType.Table;
            this.startRow = startRow;
            this.endRow = endRow;
            this.dataTable = dataTable;
        }

        public override string GetString()
        {
            try
            {
                return DataTableHelper.ConvertDataTableToString(dataTable);
            }
            catch
            {
                return "";
            }
        }
    }
}
