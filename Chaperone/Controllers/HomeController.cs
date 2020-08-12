using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class HomeController : Controller
    {
        public List<User> Users;
        // GET: Home
        public ActionResult Landing()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Home/Search.cshtml");
        }

        [HttpPost]
        public ActionResult Index(SearchModel s)
        {
            if (ModelState.IsValid)
            {
                ChaperoneEntities che = new ChaperoneEntities();
                List<User> tempUsers = che.Users.ToList();
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
            return View("~/Views/Home/Search.cshtml",s);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            ChaperoneEntities che = new ChaperoneEntities();
            User u = che.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(u);
        }
        
    }
}