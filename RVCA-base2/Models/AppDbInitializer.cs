using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RVCA_base2.Models
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var roleAdmin = new IdentityRole { Name = "admin" };
            var roleUser = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@test.ru", UserName = "admin@test.ru" };
            string password = "qwe12345";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                userManager.AddToRole(admin.Id, roleUser.Name);
            }

            // создаем пользователей
            var testUser =  new ApplicationUser { Email = "test@test.ru", UserName = "test@test.ru" };
            password = "qwe12345";
            result = userManager.Create(testUser, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(testUser.Id, roleUser.Name);
            }

            base.Seed(context);
        }
    }
}