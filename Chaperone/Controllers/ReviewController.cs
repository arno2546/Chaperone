using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            if ((int)Session["LoggedIn"] == 1 && (string)Session["u_type"] == "Gen")
            {
                int id = (int)Session["uid"];
                ChaperoneEntities che = new ChaperoneEntities();
                List<Review> reviews = che.Reviews.Where(x => x.ReviewerId == id).ToList();
                if(reviews==null|| !reviews.Any())
                {
                    TempData["ReviewMessage"] = "You haven't Reviewed anyone yet :(";
                }
                return View(reviews);
            }
            return RedirectToAction("LogIn", "User");
        }
        
    }
}