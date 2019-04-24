using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult ScheduleRegistration(Schedule schedule, int? ScheduleTimeid,int? ClientId, int? EmployeeId)
        {
            int er = 0;
            if (ScheduleTimeid==null)
            {
                er++;
            }
            if (ClientId == null)
            {
                er++;
            }
            if (EmployeeId == null)
            {
                er++;
            }
            if (er > 0)
            {
                ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }

            if (ModelState.IsValid)
            {
                int clientid = schedule.ClientId;
                var id = db.Schedules.Where(x=>schedule.ClientId==clientid).FirstOrDefault();
                if (id == null)
                {
                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    return RedirectToAction("SchedulInformation");
                }
                else
                {
                    ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                    ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
                    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                    ViewBag.error = "Already have a schedule";
                    return View(schedule);
                }

               
            }
            return View("ScheduleRegistration");

        }

        public ActionResult SchedulInformation()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View(db.Schedules.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ScheduleTime()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View(db.ScheduleTimes.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

       
        public ActionResult AddScheduleTime()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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

        public ActionResult UpdateSchedule(int? id)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Schedule schedule1 = db.Schedules.Find(id);
                if (schedule1 == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName", schedule1.ScheduleTimeId);
                //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientId");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", schedule1.EmployeeId);

                return View(schedule1);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateSchedule([Bind(Include = "ScheduleId,ScheduleTimeId,ClientId,EmployeeId")] Schedule schedule, int? ScheduleTimeid,int? EmployeeId)
        {
            int er = 0;
            if (ScheduleTimeid == null)
            {
                er++;
                ViewBag.ScheduleTime = "Select One Item";
            }
            if (EmployeeId == null)
            {
                er++;
                ViewBag.ScheduleTime = "Select One Item";
            }
            if (er > 0)
            {
                //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientId");
                //ViewBag.EmployeeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeid", "ScheduleName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View(schedule); 
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(schedule).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SchedulInformation");
                }
            }

            return View();
        }

        public ActionResult UpdateScheduleTime(int? id)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ScheduleTime scheduleTime = db.ScheduleTimes.Find(id);

                if (scheduleTime == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");

                return View(scheduleTime);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult UpdateScheduleTime([Bind(Include = "ScheduleTimeId,ScheduleName,StartTime,EndTime")] ScheduleTime scheduleTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ScheduleTime");
            }
            return View(scheduleTime);
        }

    }

   
}