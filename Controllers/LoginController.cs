using FlowerStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerStoreMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(UserTable user)
        {
            using (FlowersDBEntities db = new FlowersDBEntities())
            {
                var check = db.UserTables.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
                if (check == null)
                {
                    user.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", user);
                }
                //user exists
                Session["user.Id"] = user.Id;
                Session["user.Email"] = user.Email;
                Session["user.Name"] = user.UserName;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}