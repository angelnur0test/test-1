using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    class CellWithFormula
    {
        public string value;
        public DataFilters filters;

        public CellWithFormula(string value, string formula)
        {
            this.value = value;

            filters = new DataFilters(formula);
        }
    }
}
