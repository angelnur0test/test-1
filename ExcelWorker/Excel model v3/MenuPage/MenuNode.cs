using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class MenuNode
    {
        public MenuItem item;
        public List<MenuNode> subNodes= new List<MenuNode>();

        public void InitNodes(DataTable menu, ref int rowNumber)
        {
            if (rowNumber >= menu.Rows.Count) return;

            DataRow row = menu.Rows[rowNumber];

            item = new MenuItem()
            {
                title = (string)row["Title"],
                level = (int)row["CorrectLevel"],
                link = (string)row["FullPath"],
                hasStatPage = ((string)row["Pages"] != "" && (string)row["Pages"] != null)
            };

            rowNumber++;

            while (rowNumber < menu.Rows.Count)
            {
                DataRow nextRow = menu.Rows[rowNumber];
                var nextLevel = (int)nextRow["CorrectLevel"];

                if (nextLevel <= item.level) return;
                var subNode = new MenuNode();
                subNode.InitNodes(menu, ref rowNumber);
                subNodes.Add(subNode);
            }
        }
    }
}
