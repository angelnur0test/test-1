using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v2
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
            //this.firstColumnIsLabels = firstColumnIsLabels;
            //this.lastRowIsTotal = lastRowIsTotal;

            //startColumn = this.firstColumnIsLabels ? 1 : 0;
            //dataRowsNumber = lastRowIsTotal ? data.body.Count - 1 : data.body.Count;

            //SetRowLabels(data.headers);

            //for (int rowNumber = 0; rowNumber < dataRowsNumber; rowNumber++)
            //{                
            //    datasets.Add(new ChartDataSetForJS(data.body[rowNumber], rowNumber, firstColumnIsLabels));
            //}
        }

        void SetRowLabels(List<string> headers)
        {
            for (int i = startColumn; i < headers.Count; i++)
            {
                labels.Add(headers[i]);
            }
        }

        public string GetJson()
        {
            string json = new JavaScriptSerializer().Serialize(this);
            return json;
        }
    }
}
