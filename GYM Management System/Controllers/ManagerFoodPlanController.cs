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
    public class ManagerFoodPlanController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ManagerFoodPlan
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult FoodPlanAdd()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
            {
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
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
        public ActionResult FoodPlanAdd(FoodPlan foodPlan, int? ClientId, int? EmployeeId)
        {
            int er = 0;
            if (ClientId == null)
            {
                er++;
                ViewBag.erClient = "Select One Item";

            }
            if (EmployeeId == null)
            {
                er++;
                ViewBag.erEmployee = "Select One Item";
            }

            if (er > 0)
            {
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.FoodPlans.Add(foodPlan);
                db.SaveChanges();
                ViewBag.Messaage = "Data Insert Successfull";
                return RedirectToAction("FoodPlanList");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        public ActionResult FoodPlanList()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
            {
                return View(db.FoodPlans.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpGet]
        public ActionResult FoodPlanUpdate(int? id)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                // Client ClientUpdate = db.Clients.Find(id);
                //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
                //ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                FoodPlan foodPlan = db.FoodPlans.Find(id);

                if (foodPlan == null)
                {
                    return HttpNotFound();
                }
                return View(foodPlan);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }

        }

        [HttpPost]
        public ActionResult FoodPlanUpdate([Bind(Include = "FoodId,ClientId,EmployeeId,FoodPlan1,UpdateDate")]  FoodPlan foodPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodPlan).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Data Update Successfully";
                return RedirectToAction("FoodPlanList");
            }
            return View(foodPlan);

        }
    }
}