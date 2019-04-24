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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {

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
        public ActionResult ExpenseAdd(Expense expense, int? EmployeeId)
        {
            int er = 0;
            if(EmployeeId==null)
            {
                ViewBag.Error = "Please select one item";
                er++;
               
            }
            if (er>0)
            {
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }
            else
            {
                if(ModelState.IsValid)
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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
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
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View(db.Expenses.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }
    }
}