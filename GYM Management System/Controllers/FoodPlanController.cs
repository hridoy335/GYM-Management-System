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
    public class FoodPlanController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: FoodPlan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FoodPlanAdd() 
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        [HttpPost]
        public ActionResult FoodPlanAdd(FoodPlan foodPlan, int? ClientId, int? EmployeeId)
        {
            int er = 0;
            if(ClientId==null)
            {
                er++;
                ViewBag.erClient = "Select One Item";

            }
            if(EmployeeId==null)
            {
                er++;
                ViewBag.erEmployee = "Select One Item";
            }

            if(er>0)
            {
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClietName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }

            if(ModelState.IsValid)
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
            return View(db.FoodPlans.ToList());
        }
        [HttpGet]
        public ActionResult FoodPlanUpdate(int? id)
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