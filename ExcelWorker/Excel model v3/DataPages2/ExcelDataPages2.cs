using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class ExcelDataPages2
    {
        List<ExcelDataPage2> pages;

        public ExcelDataPages2(ExcelWorkbook workbook)
        {
            pages = new List<ExcelDataPage2>();

            var dataSheets = workbook.Worksheets.Where(x => x.Name.StartsWith("data-"));

            //foreach (ExcelWorksheet sheet in dataSheets)
            //{
            //    pages.Add(new ExcelDataPage2(sheet));
            //}
            Parallel.ForEach(dataSheets, sheet =>
            {
                pages.Add(new ExcelDataPage2(sheet));
            });
        }

        public DataRow GetDetails(string tableName, int id)
        {
            
            var page = GetPageByName(tableName);

            if (page == null) return null;

            string filter = $"[id]='{id}'";
            if (tableName=="f_3")
                filter = $"[Id ФОНДА]='{id}'";

            DataRow row = page.table.dataTable.Select(filter).FirstOrDefault();
            
            return row;
        }

        public DataRow GetDetailsByField(string tableName, string fieldName, string fieldValue)
        {
            var page = GetPageByName(tableName);

            if (page == null) return null;

            string filter = $"[{fieldName}]='{fieldValue}'";

            DataRow row = page.table.dataTable.Select(filter).FirstOrDefault();

            return row;
        }

        public string GetFields(string tableName)
        {
            var page = GetPageByName(tableName);

            if (page == null) return null;

            var fields = page.settings.GetFieldsToShowInRowData();

            return fields;
        }

        internal string GetSearchTableResult(DataFilters filters, DataFields columns)
        {
            var page = GetPageByName(filters.tableName);

            if (page == null) return null;

            RowDataTable table = page.table;

            if (table == null)
            {
                return null;
            }
            else
            {
                return table.GetSubTableJS(filters, columns);
            }
        }

        private ExcelDataPage2 GetPageByName(string tableName)
        {
            var page = pages.FirstOrDefault(x => x.table.name == tableName);

            return page;
        }

        public string GetSubTableJS(DataFilters filters, DataFields columns)
        {
            var page = GetPageByName(filters.tableName);

            if (page == null) return null;

            RowDataTable table = page.table;

            if (table == null)
            {
                return null;
            }
            else
            {
                string json = table.GetSubTableJS(filters, columns);
                return json;
            }
        }

        public DataTable GetTableByName(string tableName)
        {
            var page = GetPageByName(tableName);

            if (page == null) return null;

            return page.table.dataTable;
        }

        internal DataRow[] GetDetailsList(string tableName, string condition)
        {
            var page = GetPageByName(tableName);

            if (page == null) return null;

            DataRow[] rows = page.table.dataTable.Select(condition).ToArray();

            return rows;
        }
    }
}