using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{

    public class ManagerAttendanceController : Controller
    {

        gym_managementEntities db = new gym_managementEntities();
        // GET: ManagerAttendance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttendenceView()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
            {
                return View(db.Attendences.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
                
            
        }

    }
}