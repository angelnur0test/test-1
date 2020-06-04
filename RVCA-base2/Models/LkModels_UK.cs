using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RVCA_base2.Models
{
    public class InvestmentData
    {
        public int id { get; set; }
        public string ProjectName { get; set; }
        public string Otrasl { get; set; }
        public string Region { get; set; }

        public string Description { get; set; }
        public string InvestmentVolume { get; set; }

        public string Stage { get; set; }
        public string Round { get; set; }
        public string DealType { get; set; }
        public string InvestorName { get; set; }
        public string InvestorType { get; set; }

        public string Sindicated { get; set; }
        public string FollowOn {get;set;}

    }

    public class InvestmentResult
    {
        public string fundId { get; set; }
        public string fundName { get; set; }
        public string foundatationDate { get; set; }
        public string fundSizeAtFinalClosing { get; set; }
        public string activesUnderManagement { get; set; }
        public string dateOfData { get; set; }
        public string fundStatus { get; set; }
        public string numberOfInvestment { get; set; }
        public string numberOfExits { get; set; }
        public string numberOfCompaniesInPortfolio { get; set; }
        public string targetIRR { get; set; }
        public string factIRR { get; set; }
        public string dryPowder { get; set; }
        public string MedianValueOfInvesmentRound { get; set; }
        public string ManagementCompany { get; set; }
        public string ManagementCompanyId { get; set; }
        public string Strategy { get; set; }
    }
    public class FundData
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Jurisdiction { get; set; }
        public string WebSite { get; set; }
        public string InvestorType { get; set; }
        public string FundType { get; set; }
        public string FoundationDate { get; set; }
        public string PrefferedRegions { get; set; }
        public string PEVC_FundType { get; set; }
        public string WithGovernment { get; set; }
        public string SeedOrNot { get; set; }
        public string IKTOrNot { get; set; }
        public string CorpOrNot { get; set; }
        public string HeadOfficeCountry { get; set; }
        public string FundSpecialization { get; set; }
        public string GeographicFocus { get; set; }
        public string OtraslPreference { get; set; }
        public string StagePreference { get; set; }
    }

    public class FundCreditorData
    {
        public int id { get; set; }
        public string Title { get; set; }

        public string Type { get; set; }
        public string TypeOfLastCredit { get; set; }

        public string DateOfLastCredit { get; set; }

        public string DealCount { get; set; }

    }

    public class CabinetUkData
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Jurisdiction { get; set; }
        public string FoundationDate { get; set; }
        public string HeadOfficeCountry { get; set; }
        

        public CabinetUkData()
        {
        }

    }

    public class CabinetContact
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Contacts { get; set; }
    }

    public class UkFundData
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string TargetValue { get; set; }
        public string ClosingValue { get; set; }

    }

    public class MutualInvestor
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string InvesmentCount { get; set; }
        public string TotalInvestments { get; set; }
        public string CompnayList { get; set; }
    }

    public class UkPartnerData
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Otrasl { get; set; }
        public string Place { get; set; }
        public string Year { get; set; }
    }

    public class FundPartnerData
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string NumberOfDeals { get; set; }
        public string DealsNames { get; set; }
        public string Otrasli { get; set; }
        public string Location { get; set; }
        public string Contacts { get; set; }
    }

    public class FundInvestmentsListItem
    {
        public int id { get; set; }
        public string CompanyName { get; set; }
        public string FundName { get; set; }
        public string InvesmentYear { get; set; }
        public string Volume { get; set; }
        public string Otrasl { get; set; }
        public string Country { get; set; }

    }

    public class FundInvestmentsListItem2
    {
        public int id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyStage { get; set; }
        public string InvesmentYear { get; set; }
        public string Volume { get; set; }
        public string Otrasl { get; set; }
        public string Country { get; set; }

    }
    public class FundExitsListItem
    {
        public int id { get; set; }
        public string CompanyName { get; set; }
        public string FundName { get; set; }
        public string ExitYear { get; set; }
        public string ExitVolume { get; set; }
        public string Sposob { get; set; }
        public string Type { get; set; }

    }

    public class NewsListItem
    {
        public int id { get; set; }
        public string Header { get; set; }
        public string Url { get; set; }
    }
}