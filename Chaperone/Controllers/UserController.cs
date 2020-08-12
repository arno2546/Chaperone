using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult CreateGenUser()
        {
            return View("~/Views/User/CreateGenUser.cshtml");            
        }
        [HttpPost]
        public ActionResult CreateGenUser(User u)
        {
            if (ModelState.IsValid)
            {
                u.User_type = "Gen";
                u.status = "Active";
                ChaperoneEntities che = new ChaperoneEntities();
                che.Users.Add(u);
                che.SaveChanges();
                TempData["LogInMessage"] = "Account Successfully Created. Please Log in";
                return RedirectToAction("LogIn");
            }
            return View(u);
        }
        [HttpGet]
        public ActionResult CreateGuide()
        {
            return View("~/Views/User/CreateGuide.cshtml");
        }
        [HttpPost]
        public ActionResult CreateGuide(User u)
        {
            if (ModelState.IsValid)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                u.User_type = "Guide";
                u.status = "Active";
                che.Users.Add(u);
                che.SaveChanges();
                TempData["LogInMessage"] = "Account Successfully Created. Please Log in";
                return RedirectToAction("LogIn");
            }
            return View(u);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User u)
        {
            ChaperoneEntities che = new ChaperoneEntities();
            User verfUser = che.Users.Where(x => x.Email.Equals(u.Email)).FirstOrDefault();
            if(verfUser!= null)
            {
                if (u.Password == verfUser.Password && verfUser.status == "Active")
                {
                    //initialize SESSION components.......
                    Session["LoggedIn"] = 1;
                    Session["uname"] = verfUser.Name;
                    Session["uid"] = verfUser.Id;
                    Session["u_type"] = verfUser.User_type;
                    if (verfUser.User_type == "Gen")
                    {
                        return RedirectToAction("Index", "GenUser");
                    }
                    if (verfUser.User_type == "Guide")
                    {
                        return RedirectToAction("Index", "Guide");
                    }
                    if (verfUser.User_type == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                if (verfUser.status == "Banned")
                {
                    TempData["LogInMessage"] = "You are Banned. Please Contact Admin for further Queries";
                }
                else
                {
                    TempData["LogInMessage"] = "Invalide Username or Password";
                }

            }
            return RedirectToAction("LogIn");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}