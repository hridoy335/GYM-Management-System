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
    public class ClientSiteEmployeeController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ClientSiteEmployee
        public ActionResult Index()
        {
            //if (Session["id"]== null)
            // {
                return View();
            // }

            //else
            //{
            //    return RedirectToAction("Login","Login");
            //}

        }
        public ActionResult EmployeInfo()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.Employees.Where(y => y.EmployeeId == x).ToList());
               
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
        }

        public ActionResult ScheduleInfo()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.Schedules.Where(y => y.EmployeeId == x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult AttendenceInfo()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.Attendences.Where(y => y.EmployeeId == x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult About()
        {
            if (Session["id"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Contact()
        {
            if (Session["id"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult FoodPlan()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.FoodPlans.Where(y => y.EmployeeId == x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
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
                return RedirectToAction("FoodPlan");
            }
            return View(foodPlan);
        }
    }
}