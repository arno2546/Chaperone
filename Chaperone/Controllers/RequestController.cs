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
        [HttpGet]
        public ActionResult CreateReview(int id)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                return View();
            }
            return RedirectToAction("LogIn", "User");
        }

        [HttpPost]
        public ActionResult CreateReview(int id,Review r)
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                if (ModelState.IsValid)
                {
                    ChaperoneEntities che = new ChaperoneEntities();                   
                    Request req = che.Requests.Where(x => x.Id == id).FirstOrDefault();
                    int reviewedId = req.GuideId;
                    int reviewerId = (int)Session["uid"];
                    r.ReviewedId = reviewedId;
                    r.ReviewerId = reviewerId;
                    Review verf = che.Reviews.Where(x => x.ReviewedId == reviewedId && x.ReviewerId == reviewerId).FirstOrDefault();
                    if (verf !=null) 
                    {
                        TempData["ReviewMessage"] = "Already Reviewed Onced";
                        return RedirectToAction("Index", "Review");
                    }
                    che.Reviews.Add(r);
                    che.SaveChanges();
                    TempData["ReviewMessage"] = "Thank You for your Review";
                    return RedirectToAction("Index","Review");
                }
                return View(r);
            }
            return RedirectToAction("LogIn", "User");
        }
    }
}