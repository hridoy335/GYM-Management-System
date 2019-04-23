using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class ManagerEmployeeController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ManagerEmployee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeInformation(String search)
        {
            int i = 0;
            var employee = from c in db.Employees select c;
            if (!String.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out i))
                {

                    // int a = Convert.ToInt32(search);
                    employee = db.Employees.Where(x => x.Employee_ID == i);
                }
                else
                {
                    employee = db.Employees.Where(x => x.EmployeeName == search);
                }
            }
            return View(employee.ToList());
        }

        public ActionResult DesignationInformation()
        {
            return View(db.Designations.ToList());
        }
    }
}