using RVCA_base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RVCA_base2.Controllers
{
    [Authorize]
    public class StatController : Controller
    {
        // GET: Stat
        public ActionResult Index()
        {
            var menu = MyExcel.GetMenuTree();
            ViewBag.lastModifiedDate = MyExcel.GetLastModifiedDate();

            return View(menu);
        }

        public ActionResult Mobile()
        {
            return View();
        }

        public ActionResult Funds()
        {
            return View();
        }

        public ActionResult Exits()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Mobile")]
        public ActionResult MobilePost()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (string.IsNullOrEmpty(Request[key]))
                    continue;
                var value = Request[key];
                parameters.Add(key, value);
            }
            ViewBag.SearchParmeters = parameters;

            string resultPage = string.Empty;

            var filter_fundType = Request["filter_fundType"];
            var filter_capitalSource = Request["filter_capitalSource"];
            var filter_isCorporate = Request["filter_isCorporate"];
            var filter_isSeed = Request["filter_isSeed"];
            var filter_fundSize = Request["filter_fundSize"];
            var filter_Otrasl = Request["filter_Otrasl"];
            var filter_Regions = Request["filter_Regions"];

            switch (filter_fundType)
            {
                case "PEVC":
                    if (filter_capitalSource == "1"&&filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                    {
                        resultPage = "investors/pe-and-vc-funds/volume-and-number";
                    }
                    break;
                case "PE":
                    if (filter_capitalSource=="1")
                    {
                        //фонды, объем число
                        if (filter_isCorporate=="2"&& filter_isSeed=="2"&& filter_fundSize=="2"&& filter_Otrasl=="2"&& filter_Regions=="2")
                        {
                            resultPage = "investors/pe-and-vc-funds/pe-funds/volume-and-number";
                        }
                        //По диапазонам размеров фондов
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "1" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/pe-funds/fund-sizes";
                        }
                        //По отраслевым предпочтениям
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "1" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/pe-funds/industry-preference";
                        }
                        //По регионам
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "1")
                        {
                            resultPage = "investors/pe-and-vc-funds/pe-funds/regions";
                        }
                        //Корпоративные / Независимые
                        else if (filter_isCorporate == "3" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/pe-funds/corporate";
                        }
                    }
                    else if (filter_capitalSource=="3")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/pe-funds/government-capital";
                        }                        
                        //else if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        //{
                        //    //Посевные непосевные
                        //}                        
                    }
                    break;
                case "VC":
                    if (filter_capitalSource == "1")
                    {
                        //фонды, объем число
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/volume-and-number";
                        }
                        //По диапазонам размеров фондов
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "1" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/fund-sizes";
                        }
                        //По отраслевым предпочтениям
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "1" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/industry-preference";
                        }
                        //По регионам
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "1")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/regions";
                        }
                        //Корпоративные / Независимые
                        else if (filter_isCorporate == "3" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate";                            
                        }
                        //Посевные / Не посевные
                        else if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/seed";
                        }
                        //ТУТ НИЧИНАЮТСЯ КОРПОРАТИВНЫЕ ФОНДЫ
                        //фонды, объем число
                        if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate-vc-funds/volume-and-number";
                        }
                        //по диапазону размеров
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_fundSize == "1" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate-vc-funds/fund-sizes";
                        }
                        //по отраслевым предпочтениям
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "1" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate-vc-funds/industry-preference";
                        }
                        //по регионам
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "1")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate-vc-funds/regions";
                        }
                        //посевные-непосевные
                        else if (filter_isCorporate == "1" && filter_isSeed == "1" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate-vc-funds/seed";
                        }
                    }
                    else if (filter_capitalSource == "2")
                    {
                        //госкапитал, обьем и число
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/vc-funds-with-government-capital/volume-and-number";
                        }
                        //госкапитал. По диапазонам размеров фондов
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "1" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/vc-funds-with-government-capital/fund-sizes";
                        }
                        //госкапитал. По отраслевым предпочтениям
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "1" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/vc-funds-with-government-capital/industry-preference";
                        }
                        //госкапитал. По регионам
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "1")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/vc-funds-with-government-capital/regions";
                        }
                        //госкапитал. Корпоративные Независимые
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "1" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/vc-funds-with-government-capital/corporate";
                        }
                        //госкапитал. Посевные непосевные
                        else if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "1")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/vc-funds-with-government-capital/seed";
                        }
                    }
                    else if (filter_capitalSource=="3")
                    {
                        //Источники капитала (с госкапиталом / Частные)
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/government-capital";
                        }
                        //Источники капитала (с госкапиталом / Частные)
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_fundSize == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investors/pe-and-vc-funds/vc-funds/corporate-vc-funds/government-capital";
                        }
                    }
                    break;
            }
            if (!string.IsNullOrEmpty(resultPage))
            {
                var page = MyExcel.GetStatPageByLink(resultPage);
                return View(page);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Funds")]
        public ActionResult FundsPost()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (string.IsNullOrEmpty(Request[key]))
                    continue;
                var value = Request[key];
                parameters.Add(key, value);
            }
            ViewBag.SearchParmeters = parameters;

            string resultPage = string.Empty;

            var filter_fundType = Request["filter_fundType"];
            var filter_capitalSource = Request["filter_capitalSource"];
            var filter_isCorporate = Request["filter_isCorporate"];
            var filter_isSeed = Request["filter_isSeed"];
            var filter_Stage = Request["filter_Stage"];
            var filter_Otrasl = Request["filter_Otrasl"];
            var filter_Regions = Request["filter_Regions"];

            switch (filter_fundType)
            {
                case "PEVC":
                    if (filter_capitalSource == "1")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/volume-and-number";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "1" && filter_Stage == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/industries";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "1" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/stages";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2"  && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/regions";
                        }
                        else if (filter_isCorporate == "3" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/with-corporate-funds";
                        }
                    }
                    else if(filter_capitalSource == "3")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/with-funds-with-government-capital";
                        }
                    }
                    break;
                case "PE":
                    if (filter_capitalSource == "1")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/pe-investments/volume-and-number";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "1" && filter_Stage == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/pe-investments/industries";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "1" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/pe-investments/stages";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/pe-investments/regions";
                        }
                        else if (filter_isCorporate == "3" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/pe-investments/with-corporate-funds";
                        }
                    }
                    else if (filter_capitalSource == "3")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/pe-investments/with-funds-with-government-capital";
                        }
                    }
                    break;
                case "VC":
                    //Фонды с участием гос. капитала и частные
                    if (filter_capitalSource == "1")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/volume-and-number";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "1" && filter_Stage == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/industries";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "1" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/stages";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/regions";
                        }
                        else if (filter_isCorporate == "3" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/with-corporate-funds";
                        }

                        //VC инвестиции с участием корпоративных фондов
                        if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/corporate-funds/volume-and-number";
                        }
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_Otrasl == "1" && filter_Stage == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/corporate-funds/industries";
                        }
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "1" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/corporate-funds/stages";
                        }
                        else if (filter_isCorporate == "1" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/corporate-funds/regions";
                        }

                        //VC инвестиции с участием посевных фондов
                        if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/seed-funds/volume-and-number";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_Otrasl == "1" && filter_Stage == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/seed-funds/industries";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_Otrasl == "2" && filter_Stage == "1" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/seed-funds/stages";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "1" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/seed-funds/regions";
                        }

                    }
                    //Фонды с участием гос. капитала
                    else if (filter_capitalSource == "2")
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/funds-with-government-capital/volume-and-number";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "1" && filter_Stage == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/funds-with-government-capital/industries";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "1" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/funds-with-government-capital/stages";
                        }
                        else if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Otrasl == "2" && filter_Stage == "2" && filter_Regions == "1")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/funds-with-government-capital/regions";
                        }
                    }
                    //С участием фондов с гос. участием  (и без гос. участия)
                    else
                    {
                        if (filter_isCorporate == "2" && filter_isSeed == "2" && filter_Stage == "2" && filter_Otrasl == "2" && filter_Regions == "2")
                        {
                            resultPage = "investments/pe-and-vc-investments/vc-investments/with-funds-with-government-capital";
                        }
                    }

                    break;
            }
            if (!string.IsNullOrEmpty(resultPage))
            {
                var page = MyExcel.GetStatPageByLink(resultPage);
                return View(page);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Exits")]
        public ActionResult ExitsPost()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (string.IsNullOrEmpty(Request[key]))
                    continue;
                var value = Request[key];
                parameters.Add(key, value);
            }
            ViewBag.SearchParmeters = parameters;

            string resultPage = string.Empty;

            var filter_fundType = Request["filter_fundType"];
            var filter_Otrasl = Request["filter_Otrasl"];
            var filter_Exit = Request["filter_Exit"];
            switch (filter_fundType)
            {
                case "PEVC":
                    if (filter_Otrasl=="2"& filter_Exit == "2")
                    {
                        resultPage = "exits/pe-and-vc-funds/volume-and-number";
                    }
                    else if (filter_Otrasl == "1" & filter_Exit == "2")
                    {
                        resultPage = "exits/pe-and-vc-funds/industries";
                    }
                    else if (filter_Otrasl == "2" & filter_Exit == "1")
                    {
                        resultPage = "exits/pe-and-vc-funds/ways";
                    }
                    break;
                case "PE":
                    if (filter_Otrasl == "2" & filter_Exit == "2")
                    {
                        resultPage = "exits/pe-and-vc-funds/pe-funds/volume-and-number";
                    }
                    else if (filter_Otrasl == "1" & filter_Exit == "2")
                    {
                        resultPage = "exits/pe-and-vc-funds/pe-funds/industries";
                    }
                    else if (filter_Otrasl == "2" & filter_Exit == "1")
                    {
                        resultPage = "exits/pe-and-vc-funds/pe-funds/ways";
                    }
                    break;
                case "VC":
                    if (filter_Otrasl == "2" & filter_Exit == "2")
                    {
                        resultPage = "exits/pe-and-vc-funds/vc-funds/volume-and-number";
                    }
                    else if (filter_Otrasl == "1" & filter_Exit == "2")
                    {
                        resultPage = "exits/pe-and-vc-funds/vc-funds/industries";
                    }
                    else if (filter_Otrasl == "2" & filter_Exit == "1")
                    {
                        resultPage = "exits/pe-and-vc-funds/vc-funds/ways";
                    }
                    break;
            }
            if (!string.IsNullOrEmpty(resultPage))
            {
                var page = MyExcel.GetStatPageByLink(resultPage);
                return View(page);
            }
            return View();
        }

        public ActionResult Page(string pageRoute)
        {
            var page = MyExcel.GetStatPageByLink(pageRoute);
            return View(page);
        }
    }
}