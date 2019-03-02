using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class ExpenseController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpenseAdd()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }
    }
}