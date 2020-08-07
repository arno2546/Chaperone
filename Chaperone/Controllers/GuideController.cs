using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class GuideController : Controller
    {
        // GET: Guide
        public ActionResult Index()
        {
            int i = (int)Session["LoggedIn"];
            string u_type = (string)Session["u_type"];
            if (u_type =="Guide" && i==1)
            {
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult Edit()
        {
            int i = (int)Session["LoggedIn"];
            int id = (int)Session["uid"];
            string u_type = (string)Session["u_type"];
            if (u_type == "Guide" && i == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
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
            if (u_type == "Guide" && i == 1)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                User userToUpdate = che.Users.Where(x => x.Id == id).FirstOrDefault();
                userToUpdate.Name = u.Name;
                userToUpdate.Languages = u.Languages;
                userToUpdate.Gender = u.Gender;
                userToUpdate.Email = u.Email;
                userToUpdate.Password = u.Password;
                userToUpdate.Location = u.Location;
                userToUpdate.Contact = u.Contact;
                userToUpdate.Bio = u.Bio;
                userToUpdate.Culture = u.Culture;
                userToUpdate.Festival = u.Festival;
                userToUpdate.Food = u.Food;
                userToUpdate.NightLife = u.NightLife;
                userToUpdate.Sports = u.Sports;
                che.SaveChanges();
                return RedirectToAction("Index","Guide");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}