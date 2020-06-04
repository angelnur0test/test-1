using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class MenuTree
    {
        public List<MenuNode> subNodes = new List<MenuNode>();

        public MenuTree(DataTable menu)
        {
            //может сломаться если нумерация уровней плохая. надо улучшить
            int rowNumber = 0;

            while (rowNumber < menu.Rows.Count)
            {
                DataRow nextRow = menu.Rows[rowNumber];
                var nextLevel = (int)nextRow["CorrectLevel"];

                if (nextLevel != 0) return;

                var subNode = new MenuNode();
                subNode.InitNodes(menu, ref rowNumber);
                subNodes.Add(subNode);
            }
        }
    }
}
