using Chaperone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chaperone.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpGet]
        public ActionResult Index()
        {
            if ((string)Session["u_type"] == "Gen" && (int)Session["LoggedIn"] == 1) 
            {
                return View();
            }
            return RedirectToAction("LogIn","User");                 
        }

        [HttpPost]
        public ActionResult Index(PayModel p)
        {
            if ((string)Session["u_type"] == "Gen" && (int)Session["LoggedIn"] == 1)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index","GenUser");
                }
                return View(p);
            }
            return RedirectToAction("LogIn", "User");
        }
    }
}