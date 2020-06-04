using ExcelWorker.Excel_model_v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v2
{
    public class DataFilters
    {
        public string tableName = null;
        public List<DataFilter> filtersList = null;
        static string encryptKey = "MrrAnZSbKE";

        public DataFilters()
        {

        }

        public DataFilters(string formula)
        {
            filtersList = new List<DataFilter>();

            //COUNTIFS(f[Столбец qqq 2],"q",f[Столбец1],"й")
            //SUMIFS(f[Столбец4],f[Столбец qqq 2],  "q",f[Столбец1],  "й")
            Regex regex = new Regex(@"(\w*)\[([^\]]*?)\]\s*,\s*("".+?""|\d+|TRUE|FALSE)");

            MatchCollection matches = regex.Matches(formula);

            if (matches.Count > 0)
            {
                //предполагается, что все условия сожержат одну таблицу, поэтому берем ее название из первого условия
                tableName = matches[0].Groups[1].Value;

                foreach (Match m in matches)
                {
                    var condition = m.Groups[3].Value;
                    condition = condition.Replace("\"", "");
                    condition = condition.Replace("TRUE", "1");
                    condition = condition.Replace("FALSE", "0");

                    filtersList.Add(new DataFilter()
                    {
                        columnName = m.Groups[2].Value,
                        condition = condition
                    });
                }
            }
        }

        public string GetString()
        {
            string result = string.Join("; ", filtersList.Select(x => $"{tableName}{x.GetString()}"));
            return result;
        }

        public bool ItIsCorrectValue(string columnName, string value)
        {
            var filter = filtersList.Where(x => x.columnName == columnName).FirstOrDefault();

            if (filter == null) return true;

            return filter.condition == value;

        }

        public string SerializeAndEncript()
        {   if (tableName == null)
            {
                return null;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(this);
                var cryptText = Security.StringEncrypt(json, encryptKey);
                return cryptText;
            }
        }

        public static DataFilters DecriptAndDeserialize(string cryptText)
        {
            var json = Security.StringDecrypt(cryptText, encryptKey);
            var filters = new JavaScriptSerializer().Deserialize< DataFilters>(json);
            return filters;
        }


        public string SerializeToBase64()
        {
            if (tableName == null)
            {
                return null;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(this);
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(json);
                return System.Convert.ToBase64String(plainTextBytes);
            }
        }

        public static DataFilters DeserializeFroBase64(string base64EncodedData)
        {
            if (base64EncodedData == null)
            {
                return null;
            }
            else
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                var json = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                var filters = new JavaScriptSerializer().Deserialize<DataFilters>(json);
                return filters;
            }
        }


    }
}
