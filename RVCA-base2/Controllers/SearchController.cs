using RVCA_base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RVCA_base2.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Funds()
        {
            return DefaultView("f_2");
        }

        public ActionResult Results()
        {
            return DefaultView("f_3");
        }

        public ActionResult Investments()
        {
            return DefaultView("i");
        }

        public ActionResult Exits()
        {
            return DefaultView("e");
        }

        public ActionResult Companies()
        {
            return DefaultView("co");
        }

        public ActionResult Mcs()
        {
            return DefaultView("m");
        }

        public ActionResult Investors()
        {
            return DefaultView("allin");
        }

        public ActionResult InvestorsInFunds()
        {
            return DefaultView("allinf");
        }

        private ActionResult DefaultView(string tableName)
        {
            ViewBag.searchResultFields = MyExcel.GetSearchResultFields(tableName);
            ViewBag.tableName = tableName;
            return View();
        }

    }
}