using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LeaveManagerTest3.Models;

namespace LeaveManagerTest3.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Identity model)
        {
            using (var context = new LeaveSystemDatabaseEntities1())
            {
                bool isValid =context.User.Any(x=>x.Username==model.Username && x.Password==model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index","Employees");
                }
                ModelState.AddModelError("","Invalid credentials");
                return View();
            }
           
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using(var context = new LeaveSystemDatabaseEntities1())
            {
                context.User.Add(model);
                context.SaveChanges();  
            }
            return RedirectToAction("Login");
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}