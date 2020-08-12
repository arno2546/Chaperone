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
            if ((string)Session["u_type"] == "Guide" && (int)Session["LoggedIn"] == 1)
            {
                int i = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                User guide = che.Users.Where(x => x.Id == i && x.User_type == "Guide").FirstOrDefault();
                List<Request> requests = che.Requests.Where(x => x.GuideId == i).ToList();
                return View(requests);
            }
            return RedirectToAction("LogIn","User");
        }
        [HttpGet]
        public ActionResult Edit()
        {
            
            if ((string)Session["u_type"] == "Guide" && (int)Session["LoggedIn"] == 1)
            {
                int i = (int)Session["LoggedIn"];
                int id = (int)Session["uid"];
                string u_type = (string)Session["u_type"];
                ChaperoneEntities che = new ChaperoneEntities();
                User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
                return View(u);
            }
            return RedirectToAction("LogIn", "User");
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {
            if ((string)Session["u_type"] == "Guide" && (int)Session["LoggedIn"] == 1)
            {
                int i = (int)Session["LoggedIn"];
                int id = (int)Session["uid"];
                string u_type = (string)Session["u_type"];
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
            return RedirectToAction("LogIn", "User");
        }
        [HttpGet]
        public ActionResult AcceptRequest(int id)
        {
            if ((string)Session["u_type"] == "Guide" && (int)Session["LoggedIn"] == 1)
            {
                int guideId = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                Request r = che.Requests.Where(x => x.Id == id && x.GuideId == guideId && x.RequestState=="Pending").FirstOrDefault();
                if (r != null)
                {
                    r.RequestState = "Accepted";
                    che.SaveChanges();
                }
                return RedirectToAction("Index");                
            }
            return RedirectToAction("LogIn", "User");
        }

        [HttpGet]
        public ActionResult RejectRequest(int id)
        {
            if ((string)Session["u_type"] == "Guide" && (int)Session["LoggedIn"] == 1)
            {
                int guideId = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                Request r = che.Requests.Where(x => x.Id == id && x.GuideId == guideId && x.RequestState=="Pending").FirstOrDefault();
                if (r != null)
                {
                    r.RequestState = "Rejected";
                    che.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("LogIn", "User");
        }
    }
}