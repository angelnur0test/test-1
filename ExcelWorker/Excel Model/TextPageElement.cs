using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    public class TextPageElement : BasePageElement
    {
        public string html;

        public TextPageElement(int startRow, int endRow, string html)
        {
            elemntTape = EElemntsType.Text;
            this.startRow = startRow;
            this.endRow = endRow;
            this.html = html;
        }

        public override string GetString()
        {
            return html;
        }
    }
}
