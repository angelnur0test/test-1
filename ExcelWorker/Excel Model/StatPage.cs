
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExcelWorker
{
    public class StatPage
    {

        public string pageName;
        public DataTable settings;
        public List<BasePageElement> elements;

        public StatPage(ExcelWorksheet sheet)
        {
            elements = new List<BasePageElement>();
            pageName = sheet.Name;

            if (sheet == null) { return; }

            ReadSettings(sheet);
            ReadTables(sheet);
            ReadTexts(sheet);
  
            elements = elements.OrderBy(x => x.startRow).ToList();
        }

        private void ReadSettings(ExcelWorksheet sheet)
        {
            settings = null;
            var table = sheet.Tables.Where(x => x.Address.Start.Row == 1 && x.Address.Start.Column == 1).FirstOrDefault();

            if(table != null)
            {
                settings = ExcelReader.GetDataTableFromExcelTable(table);
            }
        }

        void ReadTables(ExcelWorksheet sheet)
        {
            var tables = sheet.Tables.Where(x => x.Address.Start.Row >= 3 && x.Address.Start.Column == 1);

            //read all tables
            foreach (var t in tables)
            {
                var dataTable = ExcelReader.GetDataTableFromExcelTable(t);
                var tablePageElement = new TablePageElement(t.Address.Start.Row, t.Address.End.Row, dataTable);
                elements.Add(tablePageElement);
            }
        }

        void ReadTexts(ExcelWorksheet sheet)
        {
            //read all other cells
            var lastRowNumber = sheet.Dimension.End.Row;

            if (lastRowNumber > 3)
            {
                var textCells = sheet.Cells[3, 1, lastRowNumber, 1].Where(c => c.Text.Length > 0
                    && sheet.Tables.Where(t => t.Address.Start.Row <= c.Start.Row && c.Start.Row <= t.Address.End.Row).Count() == 0);

                foreach (var c in textCells)
                {
                    var textPageElement = new TextPageElement(c.Start.Row, c.End.Row, c.Text);
                    elements.Add(textPageElement);
                }
            }
        }

        public string GetString()
        {
            var result = "";

            if(settings != null)
            {
                result+="Settings:\n" + DataTableHelper.ConvertDataTableToString(settings) + "\n";
            }

            foreach (var e in elements)
            {
                result += e.GetString() + "\n";
            }
            return result;
        }
    }
}