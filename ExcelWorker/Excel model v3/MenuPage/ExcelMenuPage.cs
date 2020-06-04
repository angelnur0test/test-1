using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExcelWorker.Excel_model_v3
{
    public class ExcelMenuPage
    {
        const string menuSheetName = "Menu";
        RowDataTable menu = null;

        public ExcelMenuPage(ExcelWorkbook workbook)
        {
            var sheet = workbook.Worksheets[menuSheetName];

            if (sheet == null) { return; }

            var table = sheet.Tables.First();

            if (table == null) { return; }

            menu = new RowDataTable(table);

            DeleteRowsWithoutLevels();
            AddCorrectLevelColumn();
            AddFullPathColumn();
           
        }

        private void DeleteRowsWithoutLevels()
        {
            menu.dataTable.Rows.Cast<DataRow>().Where(r => (string)r["Level"] == "").ToList().ForEach(r => r.Delete());
        }

        void AddCorrectLevelColumn()
        {
            DataTable dataTable = menu.dataTable;
            string levelColumnName = "Level";

            if (!dataTable.Columns.Contains(levelColumnName)) { return; }

            var correctLevelColumnName = "CorrectLevel";

            var column = dataTable.Columns.Add(correctLevelColumnName, typeof(int));

            for (var rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++)
            {
                int i = rowNum;

                var levelCell = dataTable.Rows[i][levelColumnName].ToString();

                if (!int.TryParse(levelCell, out int curLevel)) continue;

                var correctLevel = 0;

                while (i >= 0 && curLevel > 0)
                {
                    i--;
                    levelCell = dataTable.Rows[i][levelColumnName].ToString();

                    if (!int.TryParse(levelCell, out int tmp)) continue;

                    if (tmp < curLevel)
                    {
                        curLevel = tmp;
                        correctLevel++;
                    }
                }

                dataTable.Rows[rowNum][correctLevelColumnName] = correctLevel;
            }
        }
        void AddFullPathColumn()
        {
            DataTable dataTable = menu.dataTable;
            string localPathColumnName = "Path";
            string levelColumnName = "CorrectLevel";

            if (!dataTable.Columns.Contains(localPathColumnName) || !dataTable.Columns.Contains(localPathColumnName)) { return; }

            var fullPathColumnName = "FullPath";

            var column = dataTable.Columns.Add(fullPathColumnName, typeof(string));

            for (var rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++)
            {
                int i = rowNum;

                var levelCell = (int)dataTable.Rows[i][levelColumnName];

                var localPathCell = (string)dataTable.Rows[i][localPathColumnName];

                int curLevel = levelCell;
                var fullPath = localPathCell;

                while (i >= 0 && curLevel > 0)
                {
                    i--;
                    levelCell = (int)dataTable.Rows[i][levelColumnName];
                    localPathCell = (string)dataTable.Rows[i][localPathColumnName];

                    if (levelCell < curLevel)
                    {
                        curLevel = levelCell;
                        fullPath = localPathCell + "/" + fullPath;
                    }
                }

                dataTable.Rows[rowNum][fullPathColumnName] = fullPath;

            }

        }

        public string[] GetStatPagesNames()
        {
            string[] result = menu.dataTable.Rows.OfType<DataRow>().Select(x => ((string)x["Pages"])).ToArray();
            result = result.Where(x => x != null && x != "").Distinct().ToArray();
            return result;
        }
        public string GetPageByFullPath(string fullPath)
        {
            var pageName = menu.dataTable.Rows.OfType<DataRow>().Where(x => ((string)x["FullPath"]) == fullPath)
                           .Select(x => ((string)x["Pages"])).FirstOrDefault();
            return pageName;
        }

        public List<MenuItem> GetMenu()
        {
            var list = new List<MenuItem>();

            foreach (DataRow r in menu.dataTable.Rows)
            {
                list.Add(new MenuItem()
                {
                    title = (string)r["Title"],
                    level = (int)r["CorrectLevel"],
                    link = (string)r["FullPath"],
                    hasStatPage=((string)r["Pages"] != "" && (string)r["Pages"] != null)
                });
            }

            return list;
        }

        public MenuTree GetMenuTree()
        {
            return new MenuTree(menu.dataTable);
        }

    }
}