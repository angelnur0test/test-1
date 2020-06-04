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
    public class Table
    {
        public string name;
        public int startRow;
        public int startColumn;
        public int endRow;
        public int endColumn;
        public DataTable dataTable;
        public bool firstColumnIsLabels;
        public bool lastRowIsTotal;


        public Table(DataTable dataTable)
        {
            this.dataTable = dataTable;
            name = "";
            startRow = 0;
            startColumn = 0;
            endRow = 0;
            endColumn = 0;
            firstColumnIsLabels = false;
            lastRowIsTotal = false;
        }


        public Table(ExcelTable excelTable)
        {
            dataTable = GetDataTableFromExcelTable(excelTable);
            firstColumnIsLabels = HasLabelsFirstColumn();
            lastRowIsTotal = HasTotalLastRow();
            name = excelTable.Name;

            startRow = excelTable.Address.Start.Row;
            startColumn = excelTable.Address.Start.Column;
            endRow = excelTable.Address.End.Row;
            endColumn = excelTable.Address.End.Column;
        }

        public string GetString(bool withFormulas = false)
        {
            string result = ConvertDataTableToString(dataTable, withFormulas);
            return result;
        }

        DataTable GetDataTableFromExcelTable(ExcelTable excelTable, bool getFirstColumnLevel = false)
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

                    if (getFirstColumnLevel)
                    {
                        row[columnLevelName] = new TableCell(sheet.Cells[rowNum, fromColumn].Style.Indent, "");
                    }
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

        public string ConvertDataTableToString(DataTable dataTable, bool withFormulas = false)
        {
            string headers = string.Join("; ", dataTable.Columns.OfType<DataColumn>().Select(x => x.ColumnName));
            string body = string.Join(Environment.NewLine, dataTable.Rows.OfType<DataRow>().Select(x => string.Join("; ", x.ItemArray.OfType<TableCell>().Select(y => y.GetString(withFormulas)))));
            return headers + Environment.NewLine + body;
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

        public bool HasColumn(string name)
        {
            return dataTable.Columns.OfType<DataColumn>().Any(x => x.ColumnName == name);
        }

        public string GetHeaderLabels(bool withFirstColumn = false)
        {
            int start;
            string labels = "";

            start = withFirstColumn == true ? 0 : 1;

            for (int i = start; i < dataTable.Columns.Count; i++)
            {
                DataColumn dc = dataTable.Columns[i];
                if (labels != "") { labels += ", "; }
                labels += "\"" + dc.ColumnName + "\"";
            }

            return labels;
        }

        public int GetRowsCount()
        {
            return dataTable.Rows.Count;
        }

        public int GetDataRowsCount()
        {
            if (HasTotalLastRow() == true)
            {
                return dataTable.Rows.Count - 1;
            }
            else
            {
                return dataTable.Rows.Count;
            }

        }

        public bool HasTotalLastRow()
        {
            int lastRow = dataTable.Rows.Count - 1;
            bool result = ((TableCell)dataTable.Rows[lastRow].ItemArray[0]).text == "Итог";
            return result;
        }

        public bool HasLabelsFirstColumn()
        {

            foreach (DataRow row in dataTable.Rows)
            {
                //странная проверка, надо переделать.
                //для таблиц с данными она лишняя.
                //может надо типы ячеек проверять или допилить numbers в tablecell
                string firstColumnValue = ((TableCell)row.ItemArray[0]).text;
                if (firstColumnValue.Length > 0)
                {
                    bool itisNumber = double.TryParse(firstColumnValue, out double num);
                    return !itisNumber;
                }
            }

            return false;
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

        public TableDataForJS GetTableDataForJS()
        {
            var tableData = new TableDataForJS(dataTable, lastRowIsTotal);
            return tableData;
        }




    }
}
