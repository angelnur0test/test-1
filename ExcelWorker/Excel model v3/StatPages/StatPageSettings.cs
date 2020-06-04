using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace ExcelWorker.Excel_model_v3
{
    public class StatPageSettings
    {

        RowDataTable settings;

        public StatPageSettings(ExcelWorksheet sheet)
        {
            settings = null;
            var table = sheet.Tables.Where(x => x.Address.Start.Row == 1).FirstOrDefault();

            if (table != null)
            {
                settings = new RowDataTable(table);
            }
        }

        public string GetFieldsToShowInRowData()
        {
            if (settings == null || settings.dataTable.Columns.Contains("Fields") == false)
            {
                return null;
            }
            else
            {
                string fields = settings.dataTable.Rows[0]["Fields"].ToString();
                fields = fields.Trim();

                if (fields.Length == 0)
                {
                    return null;
                }
                else
                {
                    var list = fields.Split('\n').ToList();

                    DataFields result = new DataFields();


                    foreach(var fieldName in list)
                    {
                        result.Add(fieldName);
                    }

                    return result.SerializeAndEncript();
                }
            }             
        }
    }
}
