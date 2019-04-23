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
    public class ManagerExpenseController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ManagerExpense
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpenseAdd()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        [HttpPost]
        public ActionResult ExpenseAdd(Expense expense, int? EmployeeId)
        {
            int er = 0;
            if (EmployeeId == null)
            {
                ViewBag.Error = "Please select one item";
                er++;

            }
            if (er > 0)
            {
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Expenses.Add(expense);
                    db.SaveChanges();
                    return RedirectToAction("ExpenseList");
                    //ViewBag.Messag = "Insert the data";
                }

            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        [HttpGet]
        public ActionResult ExpenseUpdate(int? id)
        {

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", expense.EmployeeId);
            return View(expense);
        }

        [HttpPost]
        public ActionResult ExpenseUpdate([Bind(Include = "ExpenseId,ExpenseProductName,ExpenseProductQuantity,ExpenseProductAmount,ExpenseBuyDate,EmployeeId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ExpenseList");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View("ExpenseUpdate");

        }


        public ActionResult ExpenseList()
        {
            return View(db.Expenses.ToList());
        }
    }
}