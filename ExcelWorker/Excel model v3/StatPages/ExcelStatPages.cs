using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class ExcelStatPages
    {
        List<ExcelStatPage> pages;

        public ExcelStatPages(ExcelWorkbook workbook, string[] pagesNames)
        {
            pages = new List<ExcelStatPage>();

            Parallel.ForEach(pagesNames, name =>
            {
                var page = new ExcelStatPage(workbook, name);
                pages.Add(page);
            });
        }

        public ExcelStatPage GetPage(string pageName)
        {
            var page = pages.Where(x => x.name == pageName).FirstOrDefault();
            return page;
        }
    }
}