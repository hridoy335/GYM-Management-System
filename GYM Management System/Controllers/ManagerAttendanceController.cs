using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View(db.Attendences.ToList());
        }

    }
}