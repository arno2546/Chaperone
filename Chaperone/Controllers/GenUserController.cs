using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class GenUserController : Controller
    {
        // GET: GenUser
        public ActionResult Index()
        {
            int i = (int)Session["LoggedIn"];
            string u_type = (string)Session["u_type"];
            if (i == 1 && u_type == "Gen")
            {
                return View("~/Views/User/GenUserDash.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int i = (int)Session["LoggedIn"];
            string u_type = (string)Session["u_type"];
            if (i == 1 && u_type == "Gen")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                int id = (int)Session["uid"];
                User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
                return View(u);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {
            int i = (int)Session["LoggedIn"];
            int id = (int)Session["uid"];
            string u_type = (string)Session["u_type"];
            if (u_type == "Gen" && i == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                User userToUpdate = che.Users.Where(x => x.Id == id).FirstOrDefault();
                userToUpdate.Name = u.Name;
                userToUpdate.Email = u.Email;
                userToUpdate.Password = u.Password;
                userToUpdate.Location = u.Location;
                userToUpdate.Contact = u.Contact;
                che.SaveChanges();
                return RedirectToAction("Index", "GenUser");
            }
            return RedirectToAction("Index", "Home");
        }
    }


}
