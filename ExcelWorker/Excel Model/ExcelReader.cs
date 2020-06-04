using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    static class ExcelReader
    {
        public static DataTable GetDataTableFromExcelTable(ExcelTable excelTable, bool getFirstColumnLevel = false)
        {
            DataTable dataTable = new DataTable();

            var sheet = excelTable.WorkSheet;
            var columnLevelName = "Level";

            var fromRow = excelTable.Address.Start.Row;
            var toRow = excelTable.Address.End.Row;
            var fromColumn = excelTable.Address.Start.Column;
            var toColumn = excelTable.Address.End.Column;
            var headerRow = fromRow;
            var bodyFromRow = fromRow + 1;

            var headerRange = sheet.Cells[headerRow, fromColumn, headerRow, toColumn];

            foreach (var cell in headerRange)
            {
                var column = dataTable.Columns.Add(cell.Text, typeof(TableCell));
            }

            if (getFirstColumnLevel)
            {
                var column = dataTable.Columns.Add(columnLevelName, typeof(TableCell));
            }

            for (var rowNum = bodyFromRow; rowNum <= toRow; rowNum++)
            {
                var row = dataTable.NewRow();
                for(var column = fromColumn; column<=toColumn; column++)
                {
                    var cell = sheet.Cells[rowNum, column];
                    int i = cell.Start.Column - fromColumn;
                    row[i] = new TableCell(cell.Text, cell.Formula);
                }

                var firstCellTextLength = sheet.Cells[rowNum, 1].Text.Length;
                if (firstCellTextLength > 0)
                {
                    dataTable.Rows.Add(row);

                    if (getFirstColumnLevel)
                    {
                        if (sheet.Cells[rowNum, 1].Text.Length > 0)
                        {
                            row[columnLevelName] = new TableCell(sheet.Cells[rowNum, 1].Style.Indent, "");
                        }
                        else
                        {
                            row[columnLevelName] = new TableCell("", "");
                        }
                    }
                }
            }

            return dataTable;
        }

        
    }
}
