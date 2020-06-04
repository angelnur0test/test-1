using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{

    //первые три строки в каждом листе зарезервированы для таблицы настроек
    //все таблицы должны начинаться со столбца А
    public class StatWorkbook
    {
        public const string menuSheetName = "Menu";

        string excelFilePath;
        public DataTable menu = null;
        public List<StatPage> pages = null;
        public List<DataPage> dataPages = null;

        public object EcxelReader { get; private set; }

        public StatWorkbook(string excelFilePath)
        {
            this.excelFilePath = excelFilePath;

            var fi = new FileInfo(excelFilePath);

            if (fi.Exists == false) { return; }

            using (var package = new ExcelPackage(fi))
            {
                var workbook = package.Workbook;
                LoadMenu(workbook);
                LoadPages(workbook);
                LoadData(workbook);
            }
        }

        private void LoadData(ExcelWorkbook workbook)
        {
            dataPages = new List<DataPage>();

            var sheets = workbook.Worksheets.Where(x => x.Name.StartsWith("data-"));

            foreach (var sh in sheets)
            {
                var dataPage = new DataPage(sh);
                dataPages.Add(dataPage);
            }
        }

        private void LoadPages(ExcelWorkbook workbook)
        {
            pages = new List<StatPage>();

            foreach (DataRow row in menu.Rows)
            {
                var pageName = ((TableCell)row["Pages"]).Text();
                var sheet = workbook.Worksheets[pageName];

                if (sheet != null)
                {
                    var page = new StatPage(sheet);
                    pages.Add(page);
                }
            }
        }

        private void LoadMenu(ExcelWorkbook workbook)
        {
            var sheet = workbook.Worksheets[menuSheetName];

            if (sheet == null) { return; }

            var table = sheet.Tables.First();

            if (table == null) { return; }

            menu = ExcelReader.GetDataTableFromExcelTable(table, true);
            AddFullPathColumn(menu);
            AddCorrectLevelColumn(menu);
        }

        void AddCorrectLevelColumn(DataTable dataTable, string levelColumnName = "Level")
        {
            if (!dataTable.Columns.Contains(levelColumnName)) { return; }

            var correctLevelColumnName = "CorrectLevel";

            var column = dataTable.Columns.Add(correctLevelColumnName, typeof(TableCell));

            for (var rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++)
            {
                int i = rowNum;

                var levelCell = (TableCell)dataTable.Rows[i][levelColumnName];
                
                int curLevel = (int)levelCell.number;
                var correctLevel = 0;

                while (i >= 0 && curLevel > 0)
                {
                    i--;
                    levelCell = (TableCell)dataTable.Rows[i][levelColumnName];

                    if ((int)levelCell.number < curLevel)
                    {
                        curLevel = (int)levelCell.number;
                        correctLevel++;
                    }
                }

                dataTable.Rows[rowNum][correctLevelColumnName] = new TableCell(correctLevel, "");
            }
        }

        void AddFullPathColumn(DataTable dataTable, string localPathColumnName = "Path", string levelColumnName = "Level")
        {
            if (!dataTable.Columns.Contains(localPathColumnName) || !dataTable.Columns.Contains(localPathColumnName)) { return; }

            var fullPathColumnName = "FullPath";

            var column = dataTable.Columns.Add(fullPathColumnName, typeof(TableCell));

            for (var rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++)
            {
                int i = rowNum;

                var levelCell = (TableCell)dataTable.Rows[i][levelColumnName];
                var localPathCell = (TableCell)dataTable.Rows[i][localPathColumnName];

                int curLevel = (int)levelCell.number;
                var fullPath = localPathCell.text;

                while (i >= 0 && curLevel > 0)
                {
                    i--;
                    levelCell = (TableCell)dataTable.Rows[i][levelColumnName];
                    localPathCell = (TableCell)dataTable.Rows[i][localPathColumnName];

                    if ((int)levelCell.number < curLevel)
                    {
                        curLevel = (int)levelCell.number;
                        fullPath = localPathCell.text + "/" + fullPath;
                    }
                }

                dataTable.Rows[rowNum][fullPathColumnName] = new TableCell(fullPath, "");

            }

        }

        public StatPage GetStatpageByLink(string link)
        {
            var row = menu.Rows.OfType<DataRow>().Where(x => ((TableCell)x["FullPath"]).text == link).FirstOrDefault();

            if (row == null)
            {
                return null;
            }
            else
            {
                var pageName = ((TableCell)row["Pages"]).text;
                return pages.Where(x => x.pageName == pageName).FirstOrDefault();
            }
        }
    }
}
