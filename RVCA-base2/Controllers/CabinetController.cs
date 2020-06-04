using ExcelWorker.Excel_model_v3;
using RVCA_base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using RVCA_base2.Models;

namespace RVCA_base2.Controllers
{
    public class CabinetController : Controller
    {
        // GET: Cabinet
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Uk(int id = 0, string additionalAction = null)
        {
            if (id == 0)
                id = 100;

            if (string.IsNullOrEmpty(additionalAction))
            {

                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                if (TempData["postHref"] != null)
                    ViewData["postHref"] = TempData["postHref"];
                else
                    ViewData["postHref"] = "NULL";
                return View("UkView", data);
            }
            else if (additionalAction == "editsummary")
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                return View("UkEditSummary", data);
            }
            else if (additionalAction == "editdetails")
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                return View("UkEditDetails", data);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ActionName("uk")]
        public ActionResult UkPost(int id = 0, string additionalAction = null)
        {
            string path = "/cabinet/uk/";
            if (id != 0)
                path = path + id.ToString() + "/";
            if (additionalAction == "editsummary")
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                TempData["postHref"] = "local#summary";
            }
            else if (additionalAction == "editsummary2")
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                TempData["postHref"] = "local#summary2";
            }
            else if (additionalAction == "editdetails")
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                TempData["postHref"] = "local#details";
            }
            else if (additionalAction == "editmaindetails")
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                TempData["postHref"] = "local#maindetails";
            }
            else
            {
                CabinetUkData data = MyExcel.GetDataFromDataSource(id);
                ViewData["postHref"] = "NULL";
                return View("UkView", data);
            }
            return Redirect(path);
        }
    }
}