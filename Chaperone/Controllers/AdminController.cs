using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                return View();
            }
            return View("Index","Home");
        }
        
        [HttpGet]
        public ActionResult Tourists()
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                List<User> tourists = che.Users.Where(x => x.User_type == "Gen").ToList();
                if (tourists != null)
                {
                    return View(tourists);
                }
            }
            return View("Index", "Home");
        }
        [HttpGet]
        public ActionResult deleteGenUser(int id)
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
                che.Users.Remove(u);
                che.SaveChanges();
                return RedirectToAction("Tourists");
            }
            return View("Index", "Home");
        }

        [HttpGet]
        public ActionResult Guides()
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                List<User>  Guides = che.Users.Where(x => x.User_type == "Guide").ToList();
                if (Guides != null)
                {
                    return View(Guides);
                }
            }
            return View("Index", "Home");
        }

        [HttpGet]
        public ActionResult deleteGuide(int id)
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
                che.Users.Remove(u);
                che.SaveChanges();
                return RedirectToAction("Guides");
            }
            return View("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                int id = (int)Session["uid"];
                User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
                return View(u);
                
            }
            return View("Index", "Home");
        }
        [HttpPost]
        public ActionResult Edit(User u)
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {                
                ChaperoneEntities che = new ChaperoneEntities();
                int id = (int)Session["uid"];
                User adminToUpdate = che.Users.Where(x => x.Id == id).FirstOrDefault();
                adminToUpdate.Name = u.Name;
                adminToUpdate.Email = u.Email;
                adminToUpdate.Contact = u.Contact;
                adminToUpdate.Password = u.Password;
                adminToUpdate.Location = u.Location;
                che.SaveChanges();
                return RedirectToAction("Edit");            
            }
            return View("Index", "Home");
        }
        [HttpGet]
        public ActionResult Admins()
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                List<User> Admins = che.Users.Where(x => x.User_type == "Admin").ToList();
                if (Admins != null)
                {
                    return View(Admins);
                }
            }
            return View("Index", "Home");
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                return View();                
            }
            return View("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(User u)
        {
            if ((string)Session["u_type"] == "Admin" && (int)Session["LoggedIn"] == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                u.User_type = "Admin";
                che.Users.Add(u);
                che.SaveChanges();
                return RedirectToAction("Admins");
            }
            return View("Index", "Home");
        }

        public ActionResult GetData()
        {
            ChaperoneEntities che = new ChaperoneEntities();
            int gen = che.Users.Where(x => x.User_type == "Gen").Count();
            int guide = che.Users.Where(x => x.User_type == "Guide").Count();
            int admin = che.Users.Where(x => x.User_type == "Admin").Count();
            Ratio obj = new Ratio();
            obj.Admin = admin;
            obj.Guide = guide;
            obj.Tourist = gen;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int Guide { get; set; }
            public int Tourist { get; set; }
            public int Admin { get; set; }
        }
    }
}