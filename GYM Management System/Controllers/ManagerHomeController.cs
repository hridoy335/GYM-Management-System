using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class ManagerHomeController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ManagerHome
        public ActionResult Index()
        {
            //   return View();
            System.DateTime todaydate = new System.DateTime(System.DateTime.Today.Ticks);
            DateTime date = Convert.ToDateTime(todaydate.ToString("dd/MM/yyyy"));
            //var Atteendancedate = db.Attendences.Where(y => y.AttendenceDate == date).FirstOrDefault();
            List<Attendence> list = new List<Attendence>();
            var attendanceinfo = db.Attendences.ToList();

            foreach (var row in attendanceinfo)
            {
                DateTime att = Convert.ToDateTime(row.AttendenceDate);
                //var Atteendancedate = db.Attendences.Where(y => y.AttendenceDate == date).FirstOrDefault();
                // sample s = new sample();
                if (att == date)
                {
                    Attendence a = new Attendence();
                    a.ClientId = Convert.ToInt32(row.ClientId);
                    // a.Client.ClietName = db.Clients.FirstOrDefault(x => x.ClientId == a.ClientId).ClietName;
                    a.EmployeeId = Convert.ToInt32(row.EmployeeId);
                    a.FromTime = Convert.ToDateTime(row.FromTime);
                    a.AttendenceStatus = Convert.ToBoolean(row.AttendenceStatus);
                    a.AttendenceDate = Convert.ToDateTime(row.AttendenceDate);
                    //Attendence.Add(a);
                    //db.Attendences.Add(a);
                    // list.Add(a);
                    if (row.ClientId == null)
                    {
                        int empid = Convert.ToInt32(row.EmployeeId);
                        Employee v = db.Employees.FirstOrDefault(y => y.EmployeeId == empid);
                        a.Employee = v;
                        // a.Employee.EmployeeName = v.EmployeeName;
                        list.Add(a);
                    }
                    else
                    {
                        Client client = db.Clients.FirstOrDefault(x => x.ClientId == a.ClientId);
                        a.Client = client;

                        list.Add(a);
                    }
                }


            }
            ViewBag.list = list;
            return View();
        }
    }
}