using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v2
{
    public class DataFields
    {
        public List<string> fields = null;
        static string encryptKey = "MrrAnZSbKE";

        public DataFields()
        {

        }

        public DataFields(List<string> fields)
        {
            this.fields = fields;
        }

        public string SerializeAndEncript()
        {
            if (fields == null)
            {
                return null;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(this);
                var cryptText = Security.StringEncrypt(json, encryptKey);
                return cryptText;
            }
        }

        public static DataFields DecriptAndDeserialize(string cryptText)
        {
            var json = Security.StringDecrypt(cryptText, encryptKey);
            var result = new JavaScriptSerializer().Deserialize<DataFields>(json);
            return result;
        }

    }
}
