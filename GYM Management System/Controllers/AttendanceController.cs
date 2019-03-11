using System;
using System.Collections.Generic;
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
             ViewBag.date = DateTime.Now.ToLongDateString();
             ViewBag.time = DateTime.Now.ToLongTimeString();
              
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

                   
                    attendence.EmployeeId = Convert.ToInt32(Session["id"]);
                    attendence.FromTime =Convert.ToDateTime( DateTime.Now.ToLongTimeString());
                    attendence.AttendenceStatus=true;
                    attendence.AttendenceDate =Convert.ToDateTime(DateTime.Now.ToLongDateString());
                    db.Attendences.Add(attendence);
                    db.SaveChanges();
                    ViewBag.employeemessage = "Attendance Successfull";
                    string a = employee.EmployeeName.ToString();
                    ViewBag.employeewelcome = "Welcome " + a;
                    return RedirectToAction("Attendance", "Attendance");
                }
            }

            else
            {
                Session["username"] = Client.ClientUserName.ToString();
                Session["id"] = Client.ClientId;
                //int cid = Convert.ToInt32(Session["id"]);
               // DateTime todaydate= Convert.ToDateTime(DateTime.Now.ToLongDateString());
                // Time out start
              //  var AttendanceValue = db.Attendences.Where(y => y.ClientId == cid && y.AttendenceDate == todaydate).FirstOrDefault();               
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
                
                // Time out end 

                //else
                //{
                //    attendence.AttendenceId = AttendanceValue.AttendenceId;
                //    attendence.ClientId = AttendanceValue.ClientId;
                //    attendence.FromTime = AttendanceValue.FromTime;
                   
                //    attendence.AttendenceStatus = true;
                //    attendence.AttendenceDate = AttendanceValue.AttendenceDate;

                //    db.Entry(attendence).State = EntityState.Modified;
                //    db.SaveChanges();
                //    return RedirectToAction("Attendence");
                //}
                
            }

           
        }

        public ActionResult AttendenceView()
        {
            return View(db.Attendences.ToList());
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