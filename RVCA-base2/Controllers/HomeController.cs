using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RVCA_base.Models;
using RVCA_base2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EFDataLayer;

namespace RVCA_base2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            HomeViewModel homeModelData = new HomeViewModel();
            using (rvcaEntities db = new rvcaEntities())
            {
                homeModelData.all_news = db.news.OrderByDescending(x=>x.publicationDate).Take(3).ToList();
                homeModelData.all_analytics = db.analytics.OrderByDescending(x=>x.publicationDate).Take(3).ToList();
            }
            return View(homeModelData);
        }

      
    }
}