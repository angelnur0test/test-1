using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelWorker
{
    public class TableFilters
    {
        public string tableName = null;
        public List<TableFilter> filtersList = null;

        public TableFilters(string formula)
        {
            filtersList = new List<TableFilter>();

            //COUNTIFS(f[Столбец qqq 2],"q",f[Столбец1],"й")
            //SUMIFS(f[Столбец4],f[Столбец qqq 2],  "q",f[Столбец1],  "й")
            Regex regex = new Regex(@"(\w*)\[([^\]]*?)\]\s*,\s*(?:""(.+?)""|(\d+?))");

            MatchCollection matches = regex.Matches(formula);

            if (matches.Count > 0)
            {
                //предполагается, что все условия сожержат одну таблицу, поэтому берем ее название из первого условия
                tableName = matches[0].Groups[1].Value;

                foreach (Match m in matches)
                {
                    filtersList.Add(new TableFilter()
                    {
                        columnName = m.Groups[2].Value,
                        condition = m.Groups[3].Value
                    });
                }
            }

            
        }

        public string GetString()
        {
            string result = string.Join("; ", filtersList.Select(x => $"{tableName}{x.GetString()}"));
            return result;
        }
    }
}
