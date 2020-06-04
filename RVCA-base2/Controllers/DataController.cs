using ExcelWorker.Excel_model_v3;
using RVCA_base.Models;
using System.Web.Mvc;

namespace RVCA_base2.Views
{
    [Authorize]
    public class DataController : Controller
    {
        [HttpGet]
        public string GetTableByFilters(string fields, string filters)
        {
            var curFilters = DataFilters.DecriptAndDeserialize(filters);
            var curFields = DataFields.DecriptAndDeserialize(fields);

            curFields.AddDetailsUrlField();

            FilterCorrector(curFilters, curFields);

            var tableJS = MyExcel.GetSubDataTable(curFilters, curFields);

            return tableJS;
        }

        [HttpGet]
        public string GetSearchTableByFilters(string fields, string filters)
        {
            var curFilters = DataFilters.Deserialize(filters);
            var curFields = DataFields.DecriptAndDeserialize(fields);

            curFields.AddDetailsUrlField();

            FilterCorrector(curFilters, curFields);

            var tableJS = MyExcel.GetSubDataTable(curFilters, curFields);

            return tableJS;
        }

        private void FilterCorrector(DataFilters filters, DataFields fields)
        {
            //вопрос того, какие списки показывать должен быть завязан на ролях. Пока показываем только публичные данные.
            if (filters.tableName == "i")
            {
                //для инвестиций
                filters.filtersList.Add(new DataFilter() { columnName = "Публичная", condition = "1" });
                filters.filtersList.Add(new DataFilter() { columnName = "Есть совокупность", condition = "0" });

                // есть фонд???

                filters.filtersList.Add(new DataFilter() { columnName = "Страна", condition = "Россия" });
            }
            else if (filters.tableName == "e")
            {
                //для выходов
                filters.filtersList.Add(new DataFilter() { columnName = "Публичный", condition = "1" });
                filters.filtersList.Add(new DataFilter() { columnName = "Страна", condition = "Россия" });
            }
            else if (filters.tableName == "f")
            {
                //для фондов
            }
            else if (filters.tableName == "f_2")
            {
                //для поиска фондов
                filters.filtersList.Add(new DataFilter() { columnName = "Фонды", condition = "1" });
            }

            else if (filters.tableName == "co")
            {
                //для поиска компаний
               // filters.filtersList.Add(new DataFilter() { columnName = "Неизвестная компания", condition = "0" });
            }
            else if (filters.tableName == "m")
            {
                //для поиска ук
            }
        }

    }
}