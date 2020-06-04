using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v3
{
    public class RowTableDataForJS
    {
        public List<List<string>> data;
        public List<TableDataColumnForJS> columns;
        public int DetailsLinkColumnNumber = 0;
        bool lastRowIsTotal;
        int dataRowsNumber;
        bool addIndexColumn;

        public RowTableDataForJS(DataTable dataTable, bool lastRowIsTotal = false, bool addIndexColumn = true)
        {
            this.lastRowIsTotal = lastRowIsTotal;
            this.addIndexColumn = addIndexColumn; ;
            dataRowsNumber = lastRowIsTotal ? dataTable.Rows.Count - 1 : dataTable.Rows.Count;

            SetColumns(dataTable, addIndexColumn);
            SetData(dataTable, addIndexColumn);

            var column = columns.FindIndex(x => x.title.Contains("Название"));
            DetailsLinkColumnNumber = column > 0 ? column : 0;

        }

 

        private void SetData(DataTable dataTable, bool addIndexColumn)
        {
            data = new List<List<string>>();
            int index = 1;

            for (int i = 0; i < dataRowsNumber; i++)
            {
                var list = new List<string>();

                if (addIndexColumn)
                {
                    list.Add(index.ToString());
                }

                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    list.Add((string)dataTable.Rows[i][j]);
                }

                this.data.Add(list);
                index++;
            }
        }

        private void SetColumns(DataTable dataTable, bool addIndexColumn)
        {
            columns = new List<TableDataColumnForJS>();

            if (addIndexColumn)
            {
                columns.Add(new TableDataColumnForJS() { title = "№" });
            }

            foreach (DataColumn column in dataTable.Columns)
            {
                columns.Add(new TableDataColumnForJS() { title = column.ColumnName });
            }
        }

        public string GetData()
        {
            string json = new JavaScriptSerializer().Serialize(data);
            return json;
        }

        public string GetColumns()
        {
            string json = new JavaScriptSerializer().Serialize(columns);
            return json;
        }

        public string GetJson()
        {
            string json = new JavaScriptSerializer().Serialize(this);
            return json;
        }
    }
}
