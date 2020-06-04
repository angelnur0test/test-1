using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v2
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
            
            //data = GetRowData(row);
            borderWidth = 1;
            backgroundColor = backgroundColors[rowNumber % backgroundColors.Count()];
            borderColor = borderColors[rowNumber % borderColors.Count()];
        }

        public string GetRowLabel(DataTable dataTable, int rowNumber)
        {
            if (rowNumber >= 0 && rowNumber < dataTable.Rows.Count)
            {
                if (firstColumnIsLabels)
                {
                    return ((TableCell)dataTable.Rows[rowNumber].ItemArray[0]).text;
                }
                else
                {
                    return (rowNumber + 1).ToString();
                }

            }
            else
            {
                return null;
            }
        }

        public List<double?> GetRowData(List<string> row)
        {
            List<double?> data = new List<double?>();


            for (int i = startColumn; i < row.Count; i++)
            {
                if (double.TryParse(row[i], out double num))
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
