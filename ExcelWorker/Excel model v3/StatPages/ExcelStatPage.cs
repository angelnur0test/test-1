using OfficeOpenXml;
using System.Collections.Generic;

namespace ExcelWorker.Excel_model_v3
{
    public class ExcelStatPage
    {
        public string name { private set; get; }

        public StatPageSettings settings;
        public StatPageTables tables;
        public StatPageTexts texts;

        public ExcelStatPage(ExcelWorkbook workbook, string name)
        {
            this.name = name;

            var sheet = workbook.Worksheets[name];


            settings = new StatPageSettings(sheet);
            tables = new StatPageTables(sheet);

            var tablesRows = tables.GetTablesRowsNums();
            texts = new StatPageTexts(sheet, tablesRows);
        }

        public string GetFieldsFromSettings()
        {
            return settings.GetFieldsToShowInRowData();
        }
    }
}