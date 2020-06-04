using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExcelWorker.Excel_model_v2
{
    public class ExcelMenu
    {
        const string menuSheetName = "Menu";
        Table menu;

        public ExcelMenu(ExcelWorkbook workbook)
        {
            var sheet = workbook.Worksheets[menuSheetName];

            if (sheet == null) { return; }

            var table = sheet.Tables.First();

            if (table == null) { return; }

            menu = new Table(table);

            AddCorrectLevelColumn();
            AddFullPathColumn();
        }

        void AddCorrectLevelColumn()
        {

            string levelColumnName = "Level";
            var data = menu.data;

            var levelColumnIndex = data.ColumnIndex(levelColumnName);

            if (!data.ContainsColumn(levelColumnName)) { return; }

            var correctLevelColumnName = "CorrectLevel";

            data.headers.Add(correctLevelColumnName);

            for (var rowNum = 0; rowNum < data.body.Count; rowNum++)
            {
                int i = rowNum;

                var levelCell = data.body[i][levelColumnIndex];

                bool b = int.TryParse(levelCell, out int curLevel);
                if (!b) curLevel = 0;

                var correctLevel = 0;

                while (i >= 0 && curLevel > 0)
                {
                    i--;
                    levelCell = data.body[i][levelColumnIndex];
                    bool b2 = int.TryParse(levelCell, out int localLevel);

                    if (b2 && localLevel < curLevel)
                    {
                        curLevel = localLevel;
                        correctLevel++;
                    }
                }
                data.body[rowNum].Add(correctLevel.ToString());
            }
        }
        void AddFullPathColumn()
        {
            string localPathColumnName = "Path";
            string levelColumnName = "CorrectLevel";

            var data = menu.data;
            if (!data.ContainsColumn(localPathColumnName) || !data.ContainsColumn(localPathColumnName)) { return; }

            var levelColumnIndex = data.ColumnIndex(levelColumnName);
            var localPathColumnIndex = data.ColumnIndex(localPathColumnName);

            var fullPathColumnName = "FullPath";

            data.headers.Add(fullPathColumnName);

            for (var rowNum = 0; rowNum < data.body.Count; rowNum++)
            {
                int i = rowNum;

                var levelCell = data.body[i][levelColumnIndex];
                var localPathCell = data.body[i][localPathColumnIndex];

                bool b = int.TryParse(levelCell, out int curLevel);
                if (!b) curLevel = 0;

                var fullPath = localPathCell;

                while (i >= 0 && curLevel > 0)
                {
                    i--;
                    levelCell = data.body[i][levelColumnIndex];
                    localPathCell = data.body[i][localPathColumnIndex];

                    bool b2 = int.TryParse(levelCell, out int localLevel);
                    if (!b2) localLevel = 0;

                    if (localLevel < curLevel)
                    {
                        curLevel = localLevel;
                        fullPath = localPathCell + "/" + fullPath;
                    }
                }
                data.body[rowNum].Add(fullPath);
            }
        }

        public string[] GetStatPagesNames()
        {
            var data = menu.data;
            var columnIndex = data.ColumnIndex("Pages");
            string[] result = data.body.Select(x => x[columnIndex]).ToArray();
            result = result.Where(x => x != null && x != "").Distinct().ToArray();
            return result;
        }
        public string GetPageByFullPath(string fullPath)
        {
            var data = menu.data;
            var fullPathColumnIndex = data.ColumnIndex("FullPath");
            var pagesColumnIndex = data.ColumnIndex("Pages");

            var row = data.body.Where(x => x[fullPathColumnIndex] == fullPath).FirstOrDefault();
            if(row != null)
            {
                return row[pagesColumnIndex];
            }
            else
            {
                return null;
            }    
        }

        public List<MenuItem> GetMenu()
        {
            var list = new List<MenuItem>();

            var data = menu.data;

            string titleColumnName = "Title";
            string correctLevelColumnName = "CorrectLevel";
            string fullPathColumnName = "FullPath";

            var titleColumnIndex = data.ColumnIndex(titleColumnName);
            var correctLevelColumnIndex = data.ColumnIndex(correctLevelColumnName);
            var fullPathColumnIndex = data.ColumnIndex(fullPathColumnName);

            

            foreach (var r in menu.data.body)
            {
                list.Add(new MenuItem()
                {
                    title = r[titleColumnIndex],
                    level = int.Parse(r[correctLevelColumnIndex]),
                    link = r[fullPathColumnIndex]
                });
            }

            return list;
        }

    }
}