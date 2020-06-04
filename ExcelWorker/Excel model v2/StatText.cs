using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v2
{
    public class StatText
    {
        public string text;
        public string type;
        public int startRow;

        public StatText(string text, string type, int row)
        {
            this.text = text;
            this.type = type;
            this.startRow = row;
        }

        public string GetString()
        {
            return $"{text} ({type})";
        }
    }
}
