using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDataLayer;

namespace RVCA_base2.Controllers
{
    public class AnalyticsController : Controller
    {        
        // GET: Analytics
        public ActionResult Index()
        {
            using (rvcaEntities db = new rvcaEntities())
            {
                List<analytic> analyticCollection = new List<analytic>();
                analyticCollection = db.analytics.OrderByDescending(x => x.publicationDate).ToList();
                return View("Index", analyticCollection);
            }
        }

        public ActionResult View(int id)
        {
            using (rvcaEntities db = new rvcaEntities())
            {
                analytic item = db.analytics.FirstOrDefault(x => x.id == id);
                if (item == null)
                    item = new analytic();
                return View("ViewAnalytics", item);
            }
        }
    }

}