using RVCA_base.Models;
using RVCA_base2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RVCA_base2.Controllers
{
    //сейчас сделано на скорую руку для демонстрации. Дальше надо будет переделать и не забыть о опроверке ролей пользователей при доступе к данным,
    //а то сейчас все могут увидеть и приватные данные.((
    public class DetailsController : Controller
    {
        public ActionResult Fund(int id)
        {
            FundData data = MyExcel.GetFundDataFromDataSource(id);

            return View(data);
        }

        public ActionResult Result(int id)
        {
            InvestmentResult data = MyExcel.GetInvestmentResult(id);
            return View(data);
        }

        public ActionResult Investment(int id)
        {
            //var tableName = "i";
            //DataRow row = MyExcel.GetDetails(tableName, id);

            //var fundsTmp = MyExcel.GetDetailsList("fi", $"[Номер инвестиции]='{id}'");
            //var fundsIds = String.Join(", ", fundsTmp.Select(x => $"'{x["Funds.Id"]}'").ToArray());
            //fundsIds = fundsIds != "" ? fundsIds : "NULL";
            //var funds = MyExcel.GetDetailsList("f_2", $"[Id] In ({fundsIds})");

            //ViewBag.funds = funds;

            InvestmentData data = MyExcel.GetInvestmentDataFromDataSource(id);


            return View(data);
        }

        public ActionResult Exit(int id)
        {
            var tableName = "e";
            DataRow row = MyExcel.GetDetails(tableName, id);

            var fundsTmp = MyExcel.GetDetailsList("fe", $"[Номер выхода]='{id}'");
            var fundsIds = String.Join(", ", fundsTmp.Select(x => $"'{x["Id"]}'").ToArray());
            fundsIds = fundsIds != "" ? fundsIds : "NULL";
            var funds = MyExcel.GetDetailsList("f_2", $"[Id] In ({fundsIds})");

            ViewBag.funds = funds;

            return View(row);
        }

        public ActionResult Company(int id)
        {
            var tableName = "co";
            DataRow row = MyExcel.GetDetails(tableName, id);

            //пока не буду делать связки

            return View(row);
        }

        public ActionResult Mc(int id)
        {
            //var tableName = "m";
            //DataRow row = MyExcel.GetDetails(tableName, id);

            var oldFunds = MyExcel.GetDetailsList("f_2", $"[Номер УК]='{id}' And [действующие]=0");
            var curFunds = MyExcel.GetDetailsList("f_2", $"[Номер УК]='{id}' And [действующие]=1");

            CabinetUkData data = MyExcel.GetDataFromDataSource(id);

            ViewBag.oldFunds = oldFunds;
            ViewBag.curFunds = curFunds;

            return View(data);
        }

        public ActionResult Investor(int id)
        {
            return DefaultDetais("allin", id);
        }

        public ActionResult InvestorInFunds(int id)
        {
            return DefaultDetais("allinf", id);
        }

        private ActionResult DefaultDetais(string tableName, int id)
        {
            DataRow row = MyExcel.GetDetails(tableName, id);
            return View(row);
        }
    }
}