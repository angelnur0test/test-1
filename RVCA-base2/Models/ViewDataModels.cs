using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EFDataLayer;

namespace RVCA_base2.Models
{
    public class HomeViewModel
    {
        public List<news> all_news { get; set; }
        public List<analytic> all_analytics { get; set; }
    }
}