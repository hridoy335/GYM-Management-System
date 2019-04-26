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

        public ActionResult AttendenceView(string search, string FromDate, String ToDate)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
            {
                int i = 0;

                var attendance = from c in db.Attendences select c;
                if (!String.IsNullOrEmpty(search) && String.IsNullOrEmpty(FromDate) && String.IsNullOrEmpty(ToDate))
                {
                    if (int.TryParse(search, out i))
                    {

                        int a = Convert.ToInt32(search);
                        var client = db.Clients.Where(x => x.ClientIdNumber == i).FirstOrDefault();
                        if (client == null)
                        {
                            var employee = db.Employees.Where(x => x.Employee_ID == i).FirstOrDefault();
                            if (employee == null)
                            {
                                ViewBag.message = "No data found";
                            }
                            else
                            {
                                attendance = db.Attendences.Where(x => x.EmployeeId == employee.EmployeeId);
                            }
                        }
                        else
                        {
                            attendance = db.Attendences.Where(x => x.ClientId == client.ClientId);
                        }
                    }
                    else
                    {
                        var client = db.Clients.Where(x => x.ClietName == search).FirstOrDefault();
                        if (client == null)
                        {
                            var employee = db.Employees.Where(x => x.EmployeeName == search).FirstOrDefault();
                            attendance = db.Attendences.Where(x => x.EmployeeId == employee.EmployeeId);
                        }
                        else
                        {
                            attendance = db.Attendences.Where(x => x.ClientId == client.ClientId);
                        }

                    }


                }
                else if (!String.IsNullOrEmpty(FromDate) && !String.IsNullOrEmpty(ToDate) && String.IsNullOrEmpty(search))
                {
                    var ft = Convert.ToDateTime(FromDate).Date;
                    DateTime tt = Convert.ToDateTime(ToDate).Date;
                    if (ft <= tt)
                    {
                        attendance = db.Attendences.Where(x => x.AttendenceDate >= ft && x.AttendenceDate <= tt);
                    }
                    else
                    {
                        ViewBag.message = "Date is not correct format !!! ";
                    }
                    
                }

                //name,date
                else if (!String.IsNullOrEmpty(FromDate) && !String.IsNullOrEmpty(ToDate) && !String.IsNullOrEmpty(search))
                {
                    var ft = Convert.ToDateTime(FromDate).Date;
                    DateTime tt = Convert.ToDateTime(ToDate).Date;

                    if (int.TryParse(search, out i))
                    {
                        var client = db.Clients.Where(x => x.ClientIdNumber == i).FirstOrDefault();
                        if (client == null)
                        {
                            var employee = db.Employees.Where(x => x.Employee_ID == i).FirstOrDefault();
                            if (employee == null)
                            {
                                ViewBag.message = "No data found";
                            }
                            else
                            {
                                attendance = db.Attendences.Where(x => x.EmployeeId == employee.EmployeeId && x.AttendenceDate >= ft && x.AttendenceDate <= tt);
                            }
                        }
                        else
                        {
                            attendance = db.Attendences.Where(x => x.ClientId == client.ClientId && x.AttendenceDate >= ft && x.AttendenceDate <= tt);
                        }
                    }
                    else
                    {
                        var client = db.Clients.Where(x => x.ClietName == search).FirstOrDefault();
                        if (client == null)
                        {
                            var employee = db.Employees.Where(x => x.EmployeeName == search).FirstOrDefault();
                            if (employee == null)
                            {
                                ViewBag.message = "No data found";
                            }
                            else
                            {
                                attendance = db.Attendences.Where(x => x.EmployeeId == employee.EmployeeId && x.AttendenceDate >= ft && x.AttendenceDate <= tt);
                            }
                        }
                        else
                        {
                            attendance = db.Attendences.Where(x => x.ClientId == client.ClientId && x.AttendenceDate >= ft && x.AttendenceDate <= tt);
                        }

                    }
                    // attendance = db.Attendences.Where(x =>x.AttendenceDate >= ft && x.AttendenceDate < tt);
                }
                return View(attendance.ToList());
            }
            //  return View(db.Attendences.ToList());
        
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
                
            
        }

    }
}