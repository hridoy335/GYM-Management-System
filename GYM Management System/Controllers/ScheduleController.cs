using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class ScheduleController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScheduleRegistration()
        {
            ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        [HttpPost]
        public ActionResult ScheduleRegistration(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("SchedulInformation");
            }
            return View("ScheduleRegistration");
        }

        public ActionResult SchedulInformation()
        {
            return View(db.Schedules.ToList());
        }

        public ActionResult ScheduleTime()
        {
            return View(db.ScheduleTimes.ToList());
        }

       
        public ActionResult AddScheduleTime()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddScheduleTime(ScheduleTime scheduleTime)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleTimes.Add(scheduleTime);
                db.SaveChanges();
                return RedirectToAction("ScheduleTime");
            }
            return View("AddScheduleTime");
        }
    }

   
}