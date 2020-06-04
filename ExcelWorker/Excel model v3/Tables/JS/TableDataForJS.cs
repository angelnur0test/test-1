using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v3
{
    public class TableDataForJS
    {
        public List<List<string>> data;
        public List<List<string>> filters;
        public List<TableDataColumnForJS> columns;
        bool lastRowIsTotal;
        int dataRowsNumber;
        bool addIndexColumn;

        public TableDataForJS(DataTable dataTable, bool lastRowIsTotal = false, bool addIndexColumn = true)
        {
            this.lastRowIsTotal = lastRowIsTotal;
            this.addIndexColumn = addIndexColumn;
            dataRowsNumber = lastRowIsTotal ? dataTable.Rows.Count - 1 : dataTable.Rows.Count;

            SetColumns(dataTable, addIndexColumn);
            SetData(dataTable, addIndexColumn);
        }

        private void SetData(DataTable dataTable, bool addIndexColumn)
        {
            data = new List<List<string>>();
            filters = new List<List<string>>();
            int index = 1;

            for (int i = 0; i < dataRowsNumber; i++)
            {
                var list = new List<string>();
                var filtersList = new List<string>();

                if (addIndexColumn)
                {
                    list.Add(index.ToString());
                    filtersList.Add(null);
                }

                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    var cell = (CellWithFormula)dataTable.Rows[i][j];
                    list.Add(cell.value);
                    filtersList.Add(cell.filters.SerializeAndEncript());
                }

                this.data.Add(list);
                filters.Add(filtersList);
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

        public string GetFilters()
        {
            string json = new JavaScriptSerializer().Serialize(filters);
            return json;
        }

        public string GetJson()
        {
            string json = new JavaScriptSerializer().Serialize(this);
            return json;
        }
    }
}
