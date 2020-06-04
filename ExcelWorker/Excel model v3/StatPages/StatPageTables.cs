using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class StatPageTables
    {
        List<StatTable> tables;

        public StatPageTables(ExcelWorksheet sheet, int startColumn = 2)
        {
            tables = new List<StatTable>();

            var excelTables = sheet.Tables.Where(x => x.Address.Start.Row >= 3 && x.Address.Start.Column == startColumn);

            Parallel.ForEach(excelTables, excelTable =>
            {
                tables.Add(new StatTable(excelTable));
            });
        }

        public List<int> GetTablesRowsNums()
        {
            var rows = new List<int>();

            foreach(var t in tables)
            {
                rows.AddRange(t.GetTableRowsNums());
            }

            return rows;
        }

        public List<int> GetStartRows()
        {
            var list = tables.Select(x => x.startRow).ToList();
            return list;
        }

        public StatTable GetTableByStartRow(int startRow)
        {
            var table = tables.Where(x => x.startRow == startRow).FirstOrDefault();
            return table;
        }

    }
}
