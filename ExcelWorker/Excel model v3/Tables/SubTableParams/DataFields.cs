using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExcelWorker.Excel_model_v3
{
    public class DataFields
    {
        public List<string> fields = new List<string>();

        public DataFields()
        {

        }


        public DataFields(List<string> fields)
        {
            this.fields = fields;
        }

        public void Add(string fieldName)
        {
            fields.Add(fieldName);
        }

        public string SerializeAndEncript()
        {
            var json = new JavaScriptSerializer().Serialize(this);
            var cryptText = Security.StringEncryptToBase64(json);
            return cryptText;

        }

        public static DataFields DecriptAndDeserialize(string cryptText)
        {
            var json = Security.StringDecryptFromBase64(cryptText);
            var result = new JavaScriptSerializer().Deserialize<DataFields>(json);
            return result;
        }

        public static DataFields Deserialize(string text)
        {
            var json = text;
            var result = new JavaScriptSerializer().Deserialize<DataFields>(json);
            return result;
        }

        public void AddDetailsUrlField()
        {
            var name = "Details";
            if(!fields.Any(x => x == name))
            {
                fields.Insert(0, name);
            }
        }

    }
}
