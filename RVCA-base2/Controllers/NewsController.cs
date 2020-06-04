using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDataLayer;

namespace RVCA_base2.Controllers
{
    public class NewsController : Controller
    {       
        // GET: News
        public ActionResult Index()
        {
            string tag = string.Empty;
            if (Request["tag"]!=null)
            {
                tag = Request["tag"];
            }
            using (rvcaEntities db = new rvcaEntities())
            {
                List<news> newsCollection = new List<news>();
                if (string.IsNullOrEmpty(tag))
                    newsCollection = db.news.OrderByDescending(x=>x.publicationDate).ToList();
                else
                    newsCollection = db.news.OrderByDescending(x => x.publicationDate).Where(x => x.newsTag == tag).ToList();
                return View("Index", newsCollection);
            }
        }

        public ActionResult View(int id)
        {
            using (rvcaEntities db = new rvcaEntities())
            {
                news newsToShow = db.news.FirstOrDefault(x => x.id == id);
                if (newsToShow == null)
                    newsToShow = new news();
                return View("ViewNews", newsToShow);
            }                
        }
    }

    public class NewsItem
    {
        public int id { get; set; }
        public string publishDate { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}