﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        public ActionResult UpdateSchedule(int? id)
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
            ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
            //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientId");
            //ViewBag.EmployeeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");

            return View(schedule1);
        }

        [HttpPost]
        public ActionResult UpdateSchedule([Bind(Include = "ScheduleId,ScheduleTimeId,ClientId,EmployeeId")] Schedule schedule, int? ScheduleTimeid)
        {
            int er = 0;
            if(ScheduleTimeid==null) 
            {
                er++;
                ViewBag.ScheduleTime = "Select One Item";
            }
            if (er > 0)
            {
                //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientId");
                //ViewBag.EmployeeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                ViewBag.ScheduleTimeid = new SelectList(db.ScheduleTimes, "ScheduleTimeid", "ScheduleName");
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