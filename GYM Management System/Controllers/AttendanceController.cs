﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class AttendanceController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Attendance
        [HttpGet]
        public ActionResult Attendance()
        {
            ViewBag.date = DateTime.Now.Date;
            ViewBag.time = DateTime.Now.TimeOfDay;

            //ViewBag.a= DateTime.Today;

            return View();
        }
        [HttpPost]
        public ActionResult Attendance(string username, string password)
        {
            ViewBag.date = DateTime.Now.ToLongDateString();
            ViewBag.time = DateTime.Now.ToLongTimeString();
            int er = 0;
            if (username == "")
            {
                er++;
                ViewBag.username = "Username Required";
            }

            if (password == "")
            {
                er++;
                ViewBag.password = "Password Required";
            }

            if (er > 0)
            {
                return View();
            }
            var Client = db.Clients.Where(x => x.ClientUserName == username && x.ClientPassword == password).FirstOrDefault();
            Attendence attendence = new Attendence();

            

            if (Client== null)
            {
                var employee = db.Employees.Where(y => y.Employe_UserName == username && y.Employee_Password == password).FirstOrDefault();
                if(employee== null)
                {
                    ViewBag.loginfail = "Login Fail";
                    return View("Attendance");
                }
                else
                {
                    Session["username"] = employee.Employe_UserName.ToString();
                    Session["id"] = employee.EmployeeId.ToString();
                    //Session["time"] = DateTime.Now;
                    System.DateTime todaydate = new System.DateTime(System.DateTime.Today.Ticks);
                    DateTime date = Convert.ToDateTime(todaydate.ToString("dd/MM/yyyy"));
                    int id = Convert.ToInt32(Session["id"]);
                    var attendancedate = db.Attendences.Where(y => y.AttendenceDate == date && y.EmployeeId==id ).FirstOrDefault();
                    if(attendancedate== null)
                    {

                    attendence.EmployeeId = id;
                        attendence.FromTime = DateTime.Now;
                    attendence.AttendenceStatus=true;
                        attendence.AttendenceDate = DateTime.Now;
                    db.Attendences.Add(attendence);
                    db.SaveChanges();
                    ViewBag.employeemessage = "Attendance Successfull";
                    string a = employee.EmployeeName.ToString();
                    ViewBag.employeewelcome = "Welcome " + a;
                    return RedirectToAction("Attendance", "Attendance");

                    }

                    else
                    {
                        ViewBag.emessage = "Attendance already done";
                        return View();
                    }

                }
            }

            else
            {
                Session["username"] = Client.ClientUserName.ToString();
                Session["id"] = Client.ClientId;
                 int cid = Convert.ToInt32(Session["id"]);

                System.DateTime todaydate1  = new System.DateTime(System.DateTime.Today.Ticks);
                DateTime date1 = Convert.ToDateTime(todaydate1.ToString("dd/MM/yyyy")); 
                var AttendanceValue = db.Attendences.Where(y => y.ClientId == cid && y.AttendenceDate == date1).FirstOrDefault();
                if (AttendanceValue == null)
                {
                    attendence.ClientId = Convert.ToInt32(Session["id"]);
                    attendence.FromTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                    attendence.AttendenceStatus = true;
                    attendence.AttendenceDate = Convert.ToDateTime(DateTime.Now.ToLongDateString());
                    db.Attendences.Add(attendence);
                    db.SaveChanges();
                    ViewBag.clientmessage = "Attendance Successfull";
                    string b = Client.ClietName.ToString();
                    ViewBag.clientwelcome = "Welcome " + b;
                    return RedirectToAction("Attendance");
                }

                else
                {
                    ViewBag.cmessage = "Attendance already done";
                    return View();

                }
                
            }

           
        }

        public ActionResult AttendenceView()
        {

            //System.DateTime todaydate = new System.DateTime(System.DateTime.Today.Ticks);
            //DateTime date = Convert.ToDateTime(todaydate.ToString("dd/MM/yyyy"));
            //var Atteendancedate = db.Attendences.Where(y => y.AttendenceDate == date).FirstOrDefault();

            //foreach (DataRow row in Atteendancedate.Rows)
            //{
            //    // sample s = new sample();
            //    Attendence a = new Attendence();
            //    a.ClientId = Convert.ToInt32(row["ClientId"]);
            //    a.EmployeeId = Convert.ToInt32(row["EmployeeId"]);
            //    a.FromTime = Convert.ToDateTime(row["FromTime"]);
            //    a.AttendenceStatus = Convert.ToBoolean(row["AttendenceStatus"]);
            //    a.AttendenceDate = Convert.ToDateTime(row["AttendenceDate"]);
            //    //Attendence.Add(a);
            //    db.Attendences.Add(a);

            //}
            //return View(model);
            return View(db.Attendences.ToList());
        }

        public ActionResult TodayAttendance()
        {
            System.DateTime todaydate = new System.DateTime(System.DateTime.Today.Ticks);
            DateTime date = Convert.ToDateTime(todaydate.ToString("dd/MM/yyyy"));
            var Atteendancedate = db.Attendences.Where(y => y.AttendenceDate == date).FirstOrDefault();
            List<Attendence> list = new List<Attendence>();
            ViewBag.list = list;
            foreach (DataRow row in Atteendancedate.Rows)
            {
                // sample s = new sample();
                Attendence a = new Attendence();
                a.ClientId = Convert.ToInt32(row["ClientId"]);
                a.EmployeeId = Convert.ToInt32(row["EmployeeId"]);
                a.FromTime = Convert.ToDateTime(row["FromTime"]);
                a.AttendenceStatus = Convert.ToBoolean(row["AttendenceStatus"]);
                a.AttendenceDate = Convert.ToDateTime(row["AttendenceDate"]);
                //Attendence.Add(a);
                //db.Attendences.Add(a);
                list.Add(a);

            }
            return View();
        }

        //[HttpGet]
        //public ActionResult AttendenceEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //   // FoodPlan foodPlan = db.FoodPlans.Find(id);
        //    Attendence attendance = db.Attendences.Find(id);

        //    if (attendance == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //if (attendance.ClientId == null)
        //    //{
        //    //    attendance.ClientId ==;
        //    //}
        //    return View(attendance);
        //}


        //[HttpPost]
        //public ActionResult AttendenceEdit([Bind(Include = "AttendenceId,ClientId,EmployeeId,FromTime,AttendenceStatus,AttendenceDate")] Attendence attendence)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(attendence).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("AttendenceView");
        //    }

        //    return View(attendence);
        //}
    }
}