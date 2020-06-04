using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker
{
    abstract  public class BasePageElement
    {
        public EElemntsType elemntTape;
        public int startRow;
        public int endRow;

        abstract public string GetString();
    }

    public enum EElemntsType
    {
        Text,
        Table
    }
}
