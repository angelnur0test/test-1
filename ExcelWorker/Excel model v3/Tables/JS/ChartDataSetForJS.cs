using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class ChartDataSetForJS
    {
        string[] backgroundColors = new[]
        {
            "rgba(255, 99, 132, 0.4)",
            "rgba(54, 162, 235, 0.4)",
            "rgba(255, 206, 86, 0.4)",
            "rgba(75, 192, 192, 0.4)",
            "rgba(153, 102, 255, 0.4)",
            "rgba(255, 159, 64, 0.4)"
        };

        string[] borderColors = new[]
        {
            "rgba(255, 99, 132, 1)",
            "rgba(54, 162, 235, 1)",
            "rgba(255, 206, 86, 1)",
            "rgba(75, 192, 192, 1)",
            "rgba(153, 102, 255, 1)",
            "rgba(255, 159, 64, 1)"
        };

        public string label;
        public List<double?> data;
        public int borderWidth;
        public string backgroundColor;
        public string borderColor;
        bool firstColumnIsLabels;
        int startColumn;


        public ChartDataSetForJS(DataTable dataTable, int rowNumber, bool firstColumnIsLabels)
        {
            this.firstColumnIsLabels = firstColumnIsLabels;
            startColumn = this.firstColumnIsLabels ? 1 : 0;

            label = GetRowLabel(dataTable, rowNumber);
            data = GetRowData(dataTable, rowNumber);

            borderWidth = 1;
            backgroundColor = backgroundColors[rowNumber % backgroundColors.Count()];
            borderColor = borderColors[rowNumber % borderColors.Count()];
        }

        public string GetRowLabel(DataTable dataTable, int rowNumber)
        {
            if (firstColumnIsLabels)
            {
                return ((CellWithFormula)dataTable.Rows[rowNumber][0]).value;
            }
            else
            {
                return $"Ряд {(rowNumber + 1).ToString()}";
            }
        }

        public List<double?> GetRowData(DataTable dataTable, int rowNumber)
        {
            List<double?> data = new List<double?>();

            DataRow row = dataTable.Rows[rowNumber];

            for (int i = startColumn; i < dataTable.Columns.Count; i++)
            {
                if (double.TryParse(((CellWithFormula)row[i]).value, out double num))
                {
                    data.Add(num);
                }
                else
                {
                    data.Add(null);
                }
            }
            return data;

        }
    }
}
