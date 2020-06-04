using OfficeOpenXml;
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
        public class Data
        {
            public List<string> headers = new List<string>();
            public List<List<string>> body = new List<List<string>>();
            public List<List<DataFilters>> filters = new List<List<DataFilters>>();

            public bool ContainsColumn(string name)
            {
                return headers.IndexOf(name) != -1;

            }

            public int ColumnIndex(string name)
            {
                return headers.IndexOf(name);

            }
        }

        public string name;
        public int startRow;
        public int startColumn;
        public int endRow;
        public int endColumn;
        public bool firstColumnIsLabels;

        public Data data;

        public Table(ExcelTable table, bool readFormulas = false)
        {
            data = new Data();

            name = table.Name;
            startRow = table.Address.Start.Row;
            startColumn = table.Address.Start.Column;
            endRow = table.Address.End.Row;
            endColumn = table.Address.End.Column;

            var r1 = table.Address.Start.Row;
            var c1 = table.Address.Start.Column;
            var r2 = table.Address.End.Row;
            var c2 = table.Address.End.Column;

            data.headers = ReadRange(table.WorkSheet, r1, c1, c2);

            int totalRow = table.ShowTotal ? 1 : 0;

            data.body = ReadRange(table.WorkSheet, r1 + 1, c1, r2 - totalRow, c2);


            if (readFormulas)
            {
                data.filters = ReadFilters(table.WorkSheet, r1 + 1, c1, r2 - totalRow, c2);
            }

            firstColumnIsLabels = HasLabelsFirstColumn();
        }

        List<List<string>> ReadRange(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            List<List<string>> result = new List<List<string>>();

            for (int r = r1; r <= r2; r++)
            {
                List<string> rowList = new List<string>();
                for (int c = c1; c <= c2; c++)
                {
                    rowList.Add(sheet.Cells[r, c].Text);
                }
                result.Add(rowList);
            }

            return result;

        }

        List<List<DataFilters>> ReadFilters(ExcelWorksheet sheet, int r1, int c1, int r2, int c2)
        {
            List<List<DataFilters>> result = new List<List<DataFilters>>();

            for (int r = r1; r <= r2; r++)
            {
                List<DataFilters> rowList = new List<DataFilters>();
                for (int c = c1; c <= c2; c++)
                {
                    string formula = sheet.Cells[r, c].Formula;
                    rowList.Add(new DataFilters(formula));
                }
                result.Add(rowList);
            }

            return result;

        }

        List<string> ReadRange(ExcelWorksheet sheet, int r, int c1, int c2)
        {
            List<string> rowList = new List<string>();
            for (int c = c1; c <= c2; c++)
            {
                rowList.Add(sheet.Cells[r, c].Text);
            }

            return rowList;
        }

        public string GetFullChartData()
        {
            //строку итогов не читаем из файла
            var chartData = new ChartDataForJS(data, firstColumnIsLabels, false);
            string json = chartData.GetJson();
            return json;
        }

        public string GetFullTableData()
        {
            //строку итогов не читаем из файла
            var tableData = new TableDataForJS(data, false);
            string json = tableData.GetJson();
            return json;
        }

        public TableDataForJS GetTableDataForJS()
        {
            var tableData = new TableDataForJS(data, false);
            return tableData;
        }

        private Data GetSubTable(DataFilters filters, DataFields columns)
        {

            Data subTable = new Data();

            //проверяем колонки для фильтрации и собираем их индексы
            List<int> filterColumnIndexes = new List<int>();

            foreach (var f in filters.filtersList)
            {
                var i = data.headers.IndexOf(f.columnName);

                if (i == -1)
                {
                    return null;
                }
                else
                {
                    filterColumnIndexes.Add(i);
                }
            }

            //собираем индексы столбцов
            List<int> columnsIndexes = new List<int>();

            foreach (var c in columns.fields)
            {
                var i = data.headers.IndexOf(c);

                if (i != -1)
                {
                    columnsIndexes.Add(i);
                    subTable.headers.Add(c);
                }
            }


            foreach (var row in data.body)
            {
                for (int i = 0; i < filterColumnIndexes.Count(); i++)
                {
                    if (row[filterColumnIndexes[i]] != filters.filtersList[i].condition)
                    {
                        break;
                    }
                    else
                    {
                        List<string> subRow = new List<string>();

                        for (int j = 0; j < columnsIndexes.Count; j++)
                        {
                            subRow.Add(row[columnsIndexes[j]]);
                        }

                        subTable.body.Add(subRow);
                    }
                }

            }

            return subTable;

        }

        public string GetJson()
        {
            string json = new JavaScriptSerializer().Serialize(data);
            return json;
        }

        public string GetSubTableJson(DataFilters filters, DataFields columns)
        {
            var subTable = GetSubTable(filters, columns);

            var tableData = new TableDataForJS(subTable, false);
            string json = tableData.GetJson();
            return json;

        }

        public bool HasLabelsFirstColumn()
        {

            foreach (var row in data.body)
            {
                //странная проверка, надо переделать.
                //для таблиц с данными она лишняя.

                string firstColumnValue = row[0];
                if (firstColumnValue.Length > 0)
                {
                    bool itisNumber = double.TryParse(firstColumnValue, out double num);
                    return !itisNumber;
                }
            }

            return false;
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


    }
}
