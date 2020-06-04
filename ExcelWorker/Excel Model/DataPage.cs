using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    public class DataPage
    {
        public string tableName;
        DataTable dataTable;
        public DataPage(ExcelWorksheet sheet)
        {
            var table = sheet.Tables.First();

            if(table != null)
            {
                tableName = table.Name;
                dataTable = ExcelReader.GetDataTableFromExcelTable(table);
            }

        }

        public string GetString()
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
