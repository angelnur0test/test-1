using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v3
{
    class ChartDataForJS
    {
        public List<string> labels = new List<string>();
        public List<ChartDataSetForJS> datasets = new List<ChartDataSetForJS>();
        bool firstColumnIsLabels;
        bool lastRowIsTotal;
        int startColumn;
        int dataRowsNumber;

        public ChartDataForJS(DataTable dataTable, bool firstColumnIsLabels = false, bool lastRowIsTotal = false)
        {
            this.firstColumnIsLabels = firstColumnIsLabels;
            this.lastRowIsTotal = lastRowIsTotal;

            startColumn = this.firstColumnIsLabels ? 1 : 0;
            dataRowsNumber = lastRowIsTotal ? dataTable.Rows.Count - 1 : dataTable.Rows.Count;

            SetRowLabels(dataTable);

            for (int rowNumber = 0; rowNumber < dataRowsNumber; rowNumber++)
            {
                datasets.Add(new ChartDataSetForJS(dataTable, rowNumber, firstColumnIsLabels));
            }
        }

        void SetRowLabels(DataTable dataTable)
        {
            for (int i = startColumn; i < dataTable.Columns.Count; i++)
            {
                labels.Add(dataTable.Columns[i].ColumnName);
            }
        }

        public string GetJson()
        {
            string json = new JavaScriptSerializer().Serialize(this);
            return json;
        }
    }
}
