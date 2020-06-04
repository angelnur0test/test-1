using ExcelWorker.Excel_model_v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v3
{
    public class DataFilters
    {
        public string tableName = null;
        public List<DataFilter> filtersList = null;

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

        public string SerializeAndEncript()
        {
            if (tableName == null) return null;

            var json = new JavaScriptSerializer().Serialize(this);
            var cryptText = Security.StringEncryptToBase64(json);
            return cryptText;

        }

        public static DataFilters DecriptAndDeserialize(string cryptText)
        {
            //  if (cryptText == null) return null;

            var json = Security.StringDecryptFromBase64(cryptText);
            var filters = new JavaScriptSerializer().Deserialize<DataFilters>(json);
            return filters;
        }

        public static DataFilters Deserialize(string text)
        {
            var json = text;
            var filters = new JavaScriptSerializer().Deserialize<DataFilters>(json);
            return filters;
        }

        public string GetFilters(bool skipEmpty = true)
        {
            string result = "";

            foreach (var f in filtersList)
            {
                if (skipEmpty == true && (f.condition == null || f.condition == ""))
                {
                    continue;
                }

                if (result != "") result += " AND ";

                if (f.condition.Contains("%"))
                {
                    result += $"[{f.columnName}] Like'{f.condition}'";
                }
                else if (f.condition.Contains("|"))
                {
                    var allConditions = f.condition.Split('|');
                    result += $"([{f.columnName}] Like '%{allConditions[0]}%'";
                    for (int i=1;i<allConditions.Length;i++)
                    {
                        if (string.IsNullOrEmpty(allConditions[i]))
                            continue;
                        result += $" OR [{f.columnName}] Like '%{allConditions[i]}%'";
                    }
                    result += ")";
                }
                else if (f.condition.Contains("*"))
                {
                    var allConditions = f.condition.Split('*');
                    if (string.IsNullOrEmpty(allConditions[0]))
                    {
                        result += $"[{f.columnName}]<='{allConditions[1]}'";
                    }
                    else if (string.IsNullOrEmpty(allConditions[1]))
                    {
                        result += $"[{f.columnName}]>='{allConditions[0]}'";
                    }
                    else
                        result += $"([{f.columnName}]>='{allConditions[0]}' AND [{f.columnName}]<='{allConditions[1]}')";                    
                }
                else
                {
                    result += $"[{f.columnName}]='{f.condition}'";
                }
            }

            return result;
        }
    }
}
