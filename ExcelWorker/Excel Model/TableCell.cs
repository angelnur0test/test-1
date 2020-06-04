using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    public class TableCell
    {
        public string text;
        public double? number;
        public string formula;

        public TableCell(double number, string formula)
        {
            this.text = number.ToString();
            this.number = number;
            this.formula = formula;
        }

        public TableCell(int number, string formula)
        {
            this.text = number.ToString();
            this.number = number;
            this.formula = formula;
        }

        public TableCell(string text, string formula)
        {
            this.text = text;
            this.number = null;
            this.formula = formula;
        }

        public string GetString(bool withFormula = false)
        {
            string result;

            if (withFormula)
            {
                var filters = new TableFilters(formula);
                result = $"{text} ({filters.GetString()})";
            }
            else
            {
                result = text;
            }
            return result;
        }

        public string Text()
        {
            return text;
        }
    }
}
