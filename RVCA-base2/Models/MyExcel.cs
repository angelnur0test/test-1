using ExcelWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using ExcelWorker.Excel_model_v3;
using RVCA_base2.Models;

namespace RVCA_base.Models
{
    static public class MyExcel
    {
        static ExcelDocument excelDocument = null;

        public static ExcelDocument GetWorkbook()
        {
            return excelDocument;
        }

        public static void LoadExcel(string excelFilePath)
        {
            excelDocument = new ExcelDocument(excelFilePath);
        }
        
        public static List<MenuItem> GetMenu()
        {
            return excelDocument.menu.GetMenu();
        }

        public static MenuTree GetMenuTree()
        {
            return excelDocument.menu.GetMenuTree();
        }

        public static ExcelStatPage GetStatPageByLink(string link)
        {
            return excelDocument.GetStatPageByLink(link);
        }

        public static string GetSubDataTable(DataFilters filters, DataFields fields)
        {
            return excelDocument.GetSubDataTableJS(filters, fields);
        }

        public static DateTime GetLastModifiedDate()
        {
            return excelDocument.lastModifiedDate;
        }

        public static DataRow GetDetails(string tableName, int id)
        {
            return excelDocument.GetDetails(tableName, id);
        }

        public static DataRow GetDetailByField(string tableName, string fieldName,string fieldValue)
        {
            return excelDocument.GetDetailByField(tableName, fieldName,fieldValue.TrimStart(' ').TrimEnd(' '));
        }

        public static DataRow[] GetDetailsList(string tableName, string condition)
        {
            return excelDocument.GetDetailsList(tableName, condition);
        }

        public static string GetSearchResultFields(string tableName)
        {
            return excelDocument.GetSearchResultFields(tableName);
        }

        public static InvestmentResult GetInvestmentResult(int recordId)
        {
            DataRow ukAllData = MyExcel.GetDetails("f_3", recordId);
            InvestmentResult data = new InvestmentResult();
            data.fundId = ukAllData[0].ToString();
            data.fundName = ukAllData[3].ToString();
            data.foundatationDate = ukAllData[4].ToString();
            data.fundSizeAtFinalClosing = "0";
            data.activesUnderManagement = ukAllData[6].ToString();
            data.dateOfData = ukAllData[7].ToString();
            data.fundStatus = ukAllData[8].ToString();
            data.numberOfInvestment = ukAllData[9].ToString();
            data.numberOfExits = ukAllData[10].ToString();
            data.numberOfCompaniesInPortfolio = ukAllData[11].ToString();
            data.targetIRR = ukAllData[12].ToString();
            data.factIRR = ukAllData[13].ToString();
            data.dryPowder = ukAllData[14].ToString();
            data.MedianValueOfInvesmentRound = ukAllData[15].ToString();
            data.ManagementCompany = ukAllData[16].ToString();
            data.ManagementCompanyId = MapUKToId_HAckED(ukAllData[16].ToString(), 0).ToString();
            data.Strategy = ukAllData[5].ToString();
            return data;
        }

        public static FundData GetFundDataFromDataSource(int recordId)
        {
            DataRow ukAllData = MyExcel.GetDetails("f_2", recordId);
            FundData data = new FundData()
            {
                id = int.Parse(ukAllData[2].ToString()),
                Title = ukAllData[3].ToString(),
                Jurisdiction = ukAllData[31].ToString(),
                WebSite = ukAllData[27].ToString(),
                InvestorType = ukAllData[4].ToString(),
                FundType = ukAllData[5].ToString(),
                FoundationDate = ukAllData[6].ToString(),
                PrefferedRegions = ukAllData[14].ToString().Replace(";", "<br/>"),
                PEVC_FundType = ukAllData[18].ToString(),
                WithGovernment = ukAllData[20].ToString(),
                SeedOrNot = ukAllData[19].ToString(),
                IKTOrNot= ukAllData[22].ToString(),
                CorpOrNot= ukAllData[21].ToString(),
                HeadOfficeCountry = ukAllData[9].ToString(),
            };

            data.FundSpecialization = ukAllData[16].ToString().Replace(";", "<br/>");
            data.GeographicFocus = ukAllData[14].ToString().Replace(";", "<br/>");
            data.OtraslPreference = ukAllData[12].ToString().Replace(";", "<br/>");
            data.StagePreference = ukAllData[13].ToString().Replace(";", "<br/>");

            return data;
        }

        public static InvestmentData GetInvestmentDataFromDataSource(int recordId)
        {
            DataRow ukAllData = MyExcel.GetDetails("i", recordId);
            InvestmentData data = new InvestmentData()
            {
                id = int.Parse(ukAllData[5].ToString()),
                ProjectName = ukAllData[2].ToString(),
                Otrasl = ukAllData[7].ToString(),
                Region = ukAllData[9].ToString(),

                Description = ukAllData[42].ToString(),
                InvestmentVolume = ukAllData[13].ToString(),

                Stage = ukAllData[14].ToString(),
                Round = ukAllData[53].ToString(),
                DealType = ukAllData[18].ToString(),
                InvestorName = ukAllData[55].ToString(),
                InvestorType = ukAllData[57].ToString(),

                Sindicated = ukAllData[16].ToString(),
                FollowOn = ukAllData[15].ToString(),
            };
            return data;
        }

        public static CabinetUkData GetDataFromDataSource(int recordId)
        {
            DataRow ukAllData = MyExcel.GetDetails("m", recordId);
            CabinetUkData data = new CabinetUkData()
            {
                id = int.Parse(ukAllData[0].ToString()),
                Title = ukAllData[2].ToString(),
                Jurisdiction = ukAllData[4].ToString(),
                FoundationDate = ukAllData[5].ToString(),
                HeadOfficeCountry = ukAllData[6].ToString(),
            };

            return data;
        }

        public static int MapFundToId(string fundName,int proposedId)
        {
            fundName = fundName.Trim('\n');
            fundName = fundName.Trim(' ');
            int result = proposedId;
            switch (fundName)
            {
                case "Фонд содействия развитию венчурных инвестиций в малые предприятия в научно-технической сфере города Москвы":
                    return 100;
                case "Rusnano Sistema SICAR":
                case "RUSNANO SISTEMA Sicar":
                    return 101;
                case "Дальневосточный Фонд Высоких Технологий":
                    return 102;
                case "Baring Vostok Private Equity Fund IV + Baring Vostok Private Equity Fund IV Supplemental Fund":
                    return 103;
                case "Baring Vostok Private Equity Fund V + Baring Vostok Private Equity Fund V Supplemental Fund":
                    return 104;
            }
            return result;
        }

        public static int MapUKToId(string fundName, int proposedId)
        {
            int result = proposedId;
            switch (fundName)
            {
                case "Фонд содействия развитию венчурных инвестиций в малые предприятия в научно-технической сфере города Москвы":
                    return 100;
                case "ООО УК РОСНАНО":
                    return 101;
                case "Baring Vostok Capital Partners":
                    return 102;
            }
            return result;
        }

        public static int MapUKToId_HAckED(string fundName, int proposedId)
        {
            int result = proposedId;
            switch (fundName)
            {
                case "Фонд содействия развитию венчурных инвестиций в малые предприятия в научно-технической сфере города Москвы":
                    return 100;
                case "ООО УК РОСНАНО":
                case "RN Consulting S.A, \nООО \"Система Консалт\"":
                case "ООО «УК Дальневосточный фонд высоких технологий»":
                    return 101;
                case "Baring Vostok Capital Partners":
                    return 102;
            }
            return result;
        }
    }
};