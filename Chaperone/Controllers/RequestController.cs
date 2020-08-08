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
            int i = (int)Session["LoggedIn"];
            string u_type = (string)Session["u_type"];
            int id = (int)Session["uid"];
            if (i == 1 && u_type == "Gen")
            {
                ChaperoneEntities che = new ChaperoneEntities();
                List<Request> userRequests = che.Requests.Where(x => x.TouristId == id).ToList();
                return View(userRequests);
            }
            return RedirectToAction("Index");

        }
    }
}