using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class AttendanceController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Attendance
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
                    ViewBag.employee = "Login Fail";
                    return View("Attendance");
                }
                else
                {
                    Session["username"] = employee.Employe_UserName.ToString();
                    Session["id"] = employee.EmployeeId.ToString();
                    Session["time"] = DateTime.Now;

                   
                    attendence.EmployeeId = Convert.ToInt32(Session["id"]);
                    attendence.FromTime =Convert.ToDateTime( DateTime.Now.ToLongTimeString());
                    attendence.AttendenceStatus=true;
                    attendence.AttendenceDate =Convert.ToDateTime( DateTime.Now.ToLongTimeString());
                    db.Attendences.Add(attendence);
                    db.SaveChanges();
                    ViewBag.employeemessage = "Attendance Successfull";
                    return RedirectToAction("Attendance", "Attendance");
                }
            }

            else
            {
                Session["username"] = Client.ClientUserName.ToString();
                Session["id"] = Client.ClientId;
                attendence.ClientId = Convert.ToInt32( Session["id"]);
                attendence.FromTime= Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                attendence.AttendenceStatus = true;
                attendence.AttendenceDate= Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                db.Attendences.Add(attendence);
                db.SaveChanges();
                ViewBag.clientmessage = "Attendance Successfull";
                return RedirectToAction("Attendance");
            }

           
        }
    }
}