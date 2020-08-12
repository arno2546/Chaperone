using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                int id = (int)Session["uid"];
                List<Message> msgs = che.Messages.Where(m => m.To == "Gen" || m.To == "All").ToList();
                if (msgs == null || !msgs.Any())
                {
                    TempData["messageMessage"] = "Nothing to show here";
                }
                return View(msgs);
            }
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Guide")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                int id = (int)Session["uid"];
                List<Message> msgs = che.Messages.Where(m => m.To == "Guide" || m.To == "All").ToList();
                if (msgs == null || !msgs.Any())
                {
                    TempData["messageMessage"] = "Nothing to show here";
                }
                return View("~/Views/Message/IndexGuide.cshtml", msgs);
            }
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Admin")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                int id = (int)Session["uid"];
                List<Message> msgs = che.Messages.ToList();
                if (msgs == null || !msgs.Any())
                {
                    TempData["messageMessage"] = "Nothing to show here";
                }
                return View("~/Views/Message/IndexAdmin.cshtml", msgs);
            }
            return View("User", "LogIn");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Admin")
            {
                return View();
            }
            return RedirectToAction("LogIn", "User");
        }
        [HttpPost]
        public ActionResult Create(Message m)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Admin")
            {
                if (ModelState.IsValid)
                {
                    m.Form = (string)Session["uname"];
                    ChaperoneEntities che = new ChaperoneEntities();
                    che.Messages.Add(m);
                    che.SaveChanges();
                    TempData["messageMessage"] = "Message Sent";
                    return RedirectToAction("Index");
                }
                return View(m);
            }
            return RedirectToAction("LogIn", "User");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Admin")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                Message m = che.Messages.Where(x => x.Id == id).FirstOrDefault();
                return View();
            }
            return RedirectToAction("LogIn", "User");
        }
        [HttpPost]
        public ActionResult Edit(Message m, int id)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Admin")
            {
                if (ModelState.IsValid)
                {
                    ChaperoneEntities che = new ChaperoneEntities();
                    Message toUpdate = che.Messages.Where(x => x.Id == id).FirstOrDefault();
                    toUpdate.Message1 = m.Message1;
                    toUpdate.To = m.To;
                    che.SaveChanges();
                    TempData["messageMessage"] = "Message Updated";
                    return RedirectToAction("Index");
                }
                return View(m);
               
            }
            return RedirectToAction("LogIn", "User");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Admin")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                Message m = che.Messages.Where(x => x.Id == id).FirstOrDefault();
                che.Messages.Remove(m);
                che.SaveChanges();
                TempData["messageMessage"] = "Message Deleted";
                return RedirectToAction("Index");
            }
            return RedirectToAction("LogIn", "User");
        }
    }
}