using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    class RowDataTable
    {
        public string name;
        public DataTable dataTable;


        public RowDataTable(ExcelTable excelTable)
        {
            if (excelTable.Name == "Таблица144")
            {
                int i = 1;
            }
                dataTable = GetDataTableFromExcelTable(excelTable);
            //Это пиздец злобный хак, против кривых рук!
            //Нельзя так делать. В идеальном мире.
            if (excelTable.Name == "Таблица144")
                //            if (excelTable.Name == "Таблица143")
                name = "i";
            else
                name = excelTable.Name;
        }

        DataTable GetDataTableFromExcelTable(ExcelTable excelTable)
        {
            DataTable dataTable = new DataTable();

            var sheet = excelTable.WorkSheet;

            var fromRow = excelTable.Address.Start.Row;
            var toRow = excelTable.Address.End.Row;
            var fromColumn = excelTable.Address.Start.Column;
            var toColumn = excelTable.Address.End.Column;
            var headerRow = fromRow;
            var bodyFromRow = fromRow + 1;

            var headerRange = sheet.Cells[headerRow, fromColumn, headerRow, toColumn];

            foreach (var cell in headerRange)
            {
                var column = dataTable.Columns.Add(cell.Text, typeof(string));
            }

            for (var rowNum = bodyFromRow; rowNum <= toRow; rowNum++)
            {
                var row = dataTable.NewRow();
                for (var column = fromColumn; column <= toColumn; column++)
                {
                    var cell = sheet.Cells[rowNum, column];
                    int i = cell.Start.Column - fromColumn;
                    if (sheet.Name == "data-i" && cell.Address.StartsWith("HB"))
                        row[i] = cell.Value.ToString().Replace(" 12:00:00 AM","");
                    else
                        row[i] = cell.Text;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        internal string GetSubTableJS(DataFilters filters, DataFields columns)
        {
            try
            {
                DataView view = new DataView(dataTable);
                view.RowFilter = filters.GetFilters();
                var subTable = view.ToTable(false, columns.fields.ToArray());
                return new RowTableDataForJS(subTable).GetJson();
            }
            catch
            {
                return null;
            }
        }
    }
}
