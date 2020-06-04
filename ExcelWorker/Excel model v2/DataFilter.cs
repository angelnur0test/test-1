using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v2
{
    public class DataFilter
    {
        public string columnName;
        public string condition;

        public string GetString()
        {
            return $"[{columnName}]={condition}";
        }
    }
}
