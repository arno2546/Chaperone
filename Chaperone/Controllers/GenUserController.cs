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
        public List<User> Users;
        // GET: GenUser
        [HttpGet]
        public ActionResult Index()
        {          
           
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                return View("~/Views/User/GenUserDash.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Index(SearchModel s)
        {
            if (ModelState.IsValid)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                List<User> tempUsers = che.Users.ToList();
                Session["startDate"] = s.StartDate.ToString("dd/MM/yyyy");
                Session["endDate"] = s.EndDate.ToString("dd/MM/yyyy");
                Session["Location"] = s.searchString;
                Users = new List<User>();
                foreach (User u in tempUsers)
                {
                    if (u.User_type == "Guide")
                    {
                        if (u.Location.ToString().Contains(s.searchString.ToString()))
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.Female)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (u.Gender == "Female")
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.Male)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (u.Gender == "Male")
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.culture)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (Convert.ToBoolean(u.Culture))
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.nightlife)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (Convert.ToBoolean(u.NightLife))
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.sports)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (Convert.ToBoolean(u.Sports))
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.festival)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (Convert.ToBoolean(u.Festival))
                        {
                            Users.Add(u);
                        }
                    }
                }
                tempUsers = Users.ToList();
                if (s.food)
                {
                    Users = new List<User>();
                    foreach (User u in tempUsers.ToList())
                    {
                        if (Convert.ToBoolean(u.Food))
                        {
                            Users.Add(u);
                        }
                    }
                }
                return View(Users);
            }
            return View("~/Views/User/GenUserDash.cshtml", s);
        }
        [HttpGet]
        public ActionResult Edit()
        {
            
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
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
            
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                int id = (int)Session["uid"];
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

        [HttpGet]
        public ActionResult GuideProfile(int id)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
                return View(u);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ActionName("GuideProfile")]
        public ActionResult GuidedProfile(int id)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                int i = (int)Session["uid"];
                User guide = che.Users.Where(x => x.Id == id).FirstOrDefault();
                User tourist = che.Users.Where(x => x.Id == i).FirstOrDefault();
                Request check = che.Requests.Where(x => x.TouristId == i && x.GuideId == id && x.RequestState == "Pending").FirstOrDefault();
                if (check != null)
                {
                    return RedirectToAction("Index");
                }
                Request r = new Request();
                r.Location = (string)Session["Location"];
                r.GuideId = guide.Id;
                r.Contact = tourist.Contact;
                r.TouristId = tourist.Id;
                r.StartDate = (string)Session["startDate"];
                r.EndDate = (string)Session["endDate"];
                r.RequestState = "Pending";
                che.Requests.Add(r);
                che.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index","Home");

        }
    }
    
    


}
