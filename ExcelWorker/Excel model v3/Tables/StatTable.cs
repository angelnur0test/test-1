using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class StatTable
    {
        public string name;
        public int startRow;
        public int startColumn;
        public int endRow;
        public int endColumn;
        public DataTable dataTable;
        public bool firstColumnIsLabels;
        public bool lastRowIsTotal;


        public StatTable(ExcelTable excelTable)
        {
            dataTable = GetDataTableFromExcelTable(excelTable);

            name = excelTable.Name;
            startRow = excelTable.Address.Start.Row;
            startColumn = excelTable.Address.Start.Column;
            endRow = excelTable.Address.End.Row;
            endColumn = excelTable.Address.End.Column;

            lastRowIsTotal = excelTable.ShowTotal;

            //считаем, что у многострочных таблиц всегда есть заголовки,
            //а у однострочных всегда нет
            if (lastRowIsTotal == true) firstColumnIsLabels = dataTable.Rows.Count - 1 > 1;
            else firstColumnIsLabels = dataTable.Rows.Count > 1;
        }

        public List<int> GetTableRowsNums()
        {
            var rows = new List<int>();

            for (int i = startRow; i <= endRow; i++)
            {
                rows.Add(i);
            }
            return rows;
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
                var column = dataTable.Columns.Add(cell.Text, typeof(CellWithFormula));
            }

            for (var rowNum = bodyFromRow; rowNum <= toRow; rowNum++)
            {
                var row = dataTable.NewRow();
                for (var column = fromColumn; column <= toColumn; column++)
                {
                    var cell = sheet.Cells[rowNum, column];
                    int i = cell.Start.Column - fromColumn;
                    row[i] = new CellWithFormula(cell.Text, cell.Formula);
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        public string GetChartDataForJs()
        {
           return new ChartDataForJS(dataTable, firstColumnIsLabels, lastRowIsTotal).GetJson();
        }

        public TableDataForJS GetTableDataForJs()
        {
            return new TableDataForJS(dataTable, firstColumnIsLabels, lastRowIsTotal);
        }

        public string GetFullChartData()
        {
            var chartData = new ChartDataForJS(dataTable, firstColumnIsLabels, lastRowIsTotal);
            string json = chartData.GetJson();
            return json;
        }

        public string GetFullTableData()
        {
            var tableData = new TableDataForJS(dataTable, lastRowIsTotal);
            string json = tableData.GetJson();
            return json;
        }
    }


}
