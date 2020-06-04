using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RVCA_base2.Models
{
    public static class DetailsHelter
    {
        public static string GetField(DataRow row, string fieldName)
        {
            string result = "";
            if (row.Table.Columns.Contains(fieldName) == true)
            {
                result = row[fieldName].ToString();
            }
            return result;
        }
        public static string GetFieldWithNoData(DataRow row, string fieldName)
        {
            string result = "";
            if (row.Table.Columns.Contains(fieldName) == true)
            {
                result = row[fieldName].ToString();
                result = result != "" ? result : "Нет данных";
            }
            return result;
        }

        public static string GetFieldsToBrList(DataRow row, string[] fields)
        {
            string result = "";
            string val;
            foreach( var f in fields)
            {
                if (row.Table.Columns.Contains(f) == true)
                {
                    val = row[f].ToString();

                    if (val != null && val != "")
                    {
                        result += result != "" ? ",<br />" : "";
                        result += row[f].ToString();
                    }
                }
            }

            return result;
        }
    }
}