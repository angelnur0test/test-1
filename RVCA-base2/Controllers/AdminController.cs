using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using RVCA_base.Models;
using RVCA_base2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RVCA_base2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        public ActionResult Index() => View();

        public ActionResult UpdateStat() => View();


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = "test.xlsx";
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }

            MyExcel.LoadExcel(Server.MapPath("~/Files/test.xlsx"));
            return RedirectToAction("Index", "Stat");
        }

        public string test()
        {

            return "eeee!";

        }

        public ActionResult Users()
        {
            var UsersContext = new ApplicationDbContext();
            var list = UsersContext.Users.ToList();

            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var rolesList = roleManager.Roles.ToList();

            List<MyUser> users = new List<MyUser>();

            foreach( var item in list)
            {
                var user = new MyUser();
                user.userId = item.Id;
                user.userName = item.UserName;
                user.userRoles = new List<string>();

                foreach(var role in item.Roles)
                {
                    var roleDb = rolesList.FirstOrDefault(r => r.Id == role.RoleId);
                    if (roleDb != null)
                    {
                        user.userRoles.Add(roleDb.Name);
                    }
                }

                users.Add(user);
            }

            return View(users);
        }

        public string GetAllRoles()
        {
            var UsersContext = new ApplicationDbContext();

            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var rolesList = roleManager.Roles.ToList();

            var names = rolesList.Select(x => x.Name).ToList();
            var json = new JavaScriptSerializer().Serialize(names);
            return json;
        }

        public string GetUserRoles(string id)
        {
            var UsersContext = new ApplicationDbContext();

            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var rolesList = roleManager.Roles.ToList();

            var user = UsersContext.Users.FirstOrDefault(x => x.Id == id);

            var names = rolesList.Where(x => user.Roles.Any(r => r.RoleId == x.Id)).ToList();
            var json = new JavaScriptSerializer().Serialize(names);
            return json;
        }

        public void AddRole(string userName, string roleName)
        {
            var UsersContext = new ApplicationDbContext();

            var context = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var user = UsersContext.Users.FirstOrDefault(x => x.UserName == userName);

            if (user != null)
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }

        public void RemoveFromRole(string userName, string roleName)
        {
            var UsersContext = new ApplicationDbContext();

            var context = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var user = UsersContext.Users.FirstOrDefault(x => x.UserName == userName);

            if (user != null)
            {
                userManager.RemoveFromRole(user.Id, roleName);
            }
        }
    }
}