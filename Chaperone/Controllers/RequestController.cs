using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GenUserRequestIndex()
        {
            
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                int i = (int)Session["LoggedIn"];
                string u_type = (string)Session["u_type"];
                int id = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                List<Request> userRequests = che.Requests.Where(x => x.TouristId == id).ToList();
                return View(userRequests);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult closeRequest(int id)
        {
            
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                int i = (int)Session["LoggedIn"];
                string u_type = (string)Session["u_type"];
                int userId = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                Request r = che.Requests.Where(x => x.Id == id && x.RequestState == "Accepted").FirstOrDefault();
                if (r != null)
                {
                    r.RequestState = "Closed";
                    che.SaveChanges();
                }
                return RedirectToAction("GenUserRequestIndex");
                
            }
            return RedirectToAction("Index","Home");

        }
        [HttpGet]
        public ActionResult requestDone(int id)
        {

            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                int i = (int)Session["LoggedIn"];
                string u_type = (string)Session["u_type"];
                int userId = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                Request r = che.Requests.Where(x => x.Id == id && x.RequestState=="Accepted").FirstOrDefault();
                if (r != null)
                {
                    r.RequestState = "Done";
                    che.SaveChanges();
                }
                return RedirectToAction("GenUserRequestIndex");

            }
            return RedirectToAction("Index", "Home");

        }
    }
}