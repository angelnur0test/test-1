using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class ExcelDataPages
    {
        List<RowDataTable> tables;

        public ExcelDataPages(ExcelWorkbook workbook)
        {
            tables = new List<RowDataTable>();

            var dataSheets = workbook.Worksheets.Where(x => x.Name.StartsWith("data-"));

            Parallel.ForEach(dataSheets, sheet =>
            {
                var t = sheet.Tables.First();
                tables.Add(new RowDataTable(t));
            });
        }

        public string GetSubTableJS(DataFilters filters, DataFields columns)
        {
            RowDataTable table = tables.Where(x => x.name == filters.tableName).FirstOrDefault();

            if (table == null)
            {
                return null;
            }
            else
            {
                return table.GetSubTableJS(filters, columns);
            }
        }

       
    }
}