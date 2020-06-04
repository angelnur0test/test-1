using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v2
{
    public class Table2
    {
        public string tableName;
        public string sheetName;
        public int startRow;
        public int startColumn;
        public int endRow;
        public int endColumn;

        public bool firstColumnIsLabels;
        public bool lastRowIsTotal;

        public DataTable data;
        public DataTable filters;


        public Table2(ExcelTable excelTable, bool readFilters = false)
        {
            startRow = excelTable.Address.Start.Row;
            startColumn = excelTable.Address.Start.Column;
            endRow = excelTable.Address.End.Row;
            endColumn = excelTable.Address.End.Column;
            tableName = excelTable.Name;
            sheetName = excelTable.WorkSheet.Name;

            GetDataFromExcelTable(excelTable);
            GetFiltersFromExcelTable(excelTable);


            firstColumnIsLabels = excelTable.ShowFirstColumn;
            lastRowIsTotal = excelTable.ShowTotal;
        }

        private void GetFiltersFromExcelTable(ExcelTable excelTable)
        {
            throw new NotImplementedException();
        }

        DataTable GetDataFromExcelTable(ExcelTable excelTable)
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
                var column = dataTable.Columns.Add(cell.Text, typeof(TableCell));
            }

            for (var rowNum = bodyFromRow; rowNum <= toRow; rowNum++)
            {
                var row = dataTable.NewRow();
                for (var column = fromColumn; column <= toColumn; column++)
                {
                    var cell = sheet.Cells[rowNum, column];
                    int i = cell.Start.Column - fromColumn;
                    row[i] = new TableCell(cell.Text, cell.Formula);
                }

                var firstCellTextLength = sheet.Cells[rowNum, fromColumn].Text.Length;
                if (firstCellTextLength > 0)
                {
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        internal Table GetSubTable(DataFilters filters, DataFields columns)
        {
            //проверяем колонки для фильтрации
            foreach (var f in filters.filtersList)
            {
                if (!HasColumn(f.columnName)) return null;
            }

            //проверяем колооки для вывода (нужно ли???)
            foreach (var c in columns.fields)
            {
                if (!HasColumn(c)) return null;
            }

            var subTable = dataTable.Copy();

            List<DataRow> badRows = new List<DataRow>();

            //удаляем линие строки
            foreach (DataRow r in subTable.Rows)
            {
                for (int i = 0; i < r.ItemArray.Count(); i++)
                {
                    TableCell c = (TableCell)r.ItemArray[i];
                    string columnName = subTable.Columns[i].ColumnName;

                    if (filters.ItIsCorrectValue(columnName, c.text) == false)
                    {
                        badRows.Add(r);
                        break;
                    }
                }
            }

            foreach (var br in badRows)
            {
                subTable.Rows.Remove(br);
            }

            //удаляем лишние столбцы

            List<DataColumn> badColumns = new List<DataColumn>();

            foreach (DataColumn c in subTable.Columns)
            {
                if (!columns.fields.Any(x => x == c.ColumnName))
                {
                    badColumns.Add(c);
                }
            }

            foreach (var bc in badColumns)
            {
                subTable.Columns.Remove(bc);
            }

            return new Table(subTable);
        }

        /// <summary>
        /// возвращает номера строк на листе, которые занимает таблица
        /// </summary>
        /// <returns></returns>
        public List<int> GetTableRowsNums()
        {
            var rows = new List<int>();

            for (int i = startRow; i <= endRow; i++)
            {
                rows.Add(i);
            }
            return rows;
        }

        public bool HasColumn(string name)
        {
            return data.Columns.OfType<DataColumn>().Any(x => x.ColumnName == name);
        }

        public string GetFullChartData()
        {
            throw new NotImplementedException();
            //var chartData = new ChartDataForJS(dataTable, firstColumnIsLabels, lastRowIsTotal);
            //string json = chartData.GetJson();
            //return json;
        }

        public string GetFullTableData()
        {
            throw new NotImplementedException();
            //var tableData = new TableDataForJS(dataTable, lastRowIsTotal);
            //string json = tableData.GetJson();
            //return json;
        }

        public TableDataForJS GetTableDataForJS()
        {
            throw new NotImplementedException();

            //var tableData = new TableDataForJS(dataTable, lastRowIsTotal);
            //return tableData;
        }




    }
}
