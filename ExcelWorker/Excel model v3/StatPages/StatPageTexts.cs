using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class StatPageTexts
    {
        List<StatText> texts = new List<StatText>();

        public StatPageTexts(ExcelWorksheet sheet, List<int> tablesRows, int startColumn = 2)
        {
            //read all other cells
            var lastRowNumber = sheet.Dimension.End.Row;

            if (lastRowNumber > 3)
            {
                var textRows = sheet.Cells[3, startColumn, lastRowNumber, startColumn].Where(c => c.Text.Length > 0
                && !tablesRows.Any(x => x == c.Start.Row)).Select(x => x.Start.Row);

                texts = new List<StatText>();

                foreach (var row in textRows)
                {
                    string text = sheet.Cells[row, startColumn].Text;
                    string type = sheet.Cells[row, startColumn - 1].Text;
                    texts.Add(new StatText(text, type, row));
                }
            }
        }

        public List<int> GetStartRows()
        {
            var list = texts.Select(x => x.startRow).ToList();
            return list;
        }

        public StatText GetTextByStartRow(int startRow)
        {
            var text = texts.Where(x => x.startRow == startRow).FirstOrDefault();
            return text;
        }
    }
}
