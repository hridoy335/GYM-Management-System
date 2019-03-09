using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYM_Management_System.Controllers
{
    public class ClientSiteHomeController : Controller
    {
        // GET: ClientSiteHome
        public ActionResult Index()
        {
            return View();
        }
    }
}