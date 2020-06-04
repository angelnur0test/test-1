using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWorker.Excel_model_v3
{
    public class ExcelDocument
    {
        public DateTime lastModifiedDate;
        public string filePath = null;
        public ExcelMenuPage menu = null;
        public ExcelStatPages statPages = null;
        public ExcelDataPages2 dataPages = null;
        

        public ExcelDocument(string filePath)
        {
            this.filePath = filePath;
            ReadFile();
        }

        void ReadFile()
        {

            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists == false) { return; }

            using (var package = new ExcelPackage(fileInfo))
            {
                //try
                {
                    var workbook = package.Workbook;

                    lastModifiedDate = workbook.Properties.Modified;

                    LoadMenu(workbook);
                    LoadStatPages(workbook);
                    LoadDataTables(workbook);
                }
                //catch
                {

                }
            }
        }

        public ExcelStatPage GetStatPageByLink(string fullPath)
        {
            var pageName = menu.GetPageByFullPath(fullPath);

            if (pageName == "") return null;

            var statPage = statPages.GetPage(pageName);

            return statPage;
        }

        private void LoadDataTables(ExcelWorkbook workbook)
        {
            dataPages = new ExcelDataPages2(workbook);
        }

        private void LoadStatPages(ExcelWorkbook workbook)
        {
            var pagesNames = menu.GetStatPagesNames();

            statPages = new ExcelStatPages(workbook, pagesNames);
        }

        private void LoadMenu(ExcelWorkbook workbook)
        {
            menu = new ExcelMenuPage(workbook);
        }

        public string GetSubDataTableJS(DataFilters filters, DataFields columns)
        {
            return dataPages.GetSubTableJS(filters, columns);
        }

        public DataRow GetDetails(string tableName, int id)
        {
            if (dataPages == null) return null;
            return dataPages.GetDetails(tableName, id);
        }

        public DataRow GetDetailByField(string tableName, string fieldName, string FieldValue)
        {
            if (dataPages == null) return null;
            return dataPages.GetDetailsByField(tableName, fieldName,FieldValue);
        }

        public string GetSearchResultFields(string tableName)
        {
            if (dataPages == null) return null;
            return dataPages.GetFields(tableName);
        }

        public DataRow[] GetDetailsList(string tableName, string condition)
        {
            if (dataPages == null) return null;
            return dataPages.GetDetailsList(tableName, condition);
        }
    }
}
