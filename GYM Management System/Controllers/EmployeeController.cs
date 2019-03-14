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
    public class EmployeeController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //Employee Part start...
        public ActionResult EmployeeRegistration()
        {
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeRegistration(Employee employee,int? DesignationId, string Employee_Password)
        {
            int er = 0;
            if (DesignationId == null)
            {
                er++;
                ViewBag.Designation = "Select One Item";
            }
            if (Employee_Password == "") 
            {
                er++;
                ViewBag.password = "Password Required";
            }
            bool isThisIdExist = db.Employees.ToList().Exists(a => a.Employee_ID == employee.Employee_ID);
            if (isThisIdExist)
            {
                er++;
                ViewBag.idnumber = "This id is already here";
            }

            bool isThisUsenameExist = db.Employees.ToList().Exists(a => a.Employe_UserName == employee.Employe_UserName);
            if (isThisUsenameExist)
            {
                er++;
                ViewBag.username = "This Username is already here";
            }
            if (er > 0)
            {
                ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
                return View();
            }
            else
            {

                if(ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    ViewBag.Employee = "Employee Registration Successfully";
                    return RedirectToAction("EmployeeInformation");
                }

            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            return View();
        }

        public ActionResult EmployeeInformation()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult EmployeeUpdate(int?id)
        {
           
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee EmployeeUpdate = db.Employees.Find(id);
            if (EmployeeUpdate == null)
            {

                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            return View(EmployeeUpdate); 
        }

        [HttpPost]
        public ActionResult EmployeeUpdate([Bind(Include = "EmployeeId,EmployeeName,DesignationId,Employee_ID,Employee_Contact,Employee_Mail,Employee_Address,Employe_UserName,Employee_Password")] Employee employee,int? DesignationId)
        {
            int er = 0;
            if(DesignationId==null)
            {
                er++;
                ViewBag.Designation = "Select One Item";
            }
            if (er>0)
            {
                ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
                return View(employee);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("EmployeeInformation");
                }
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", employee.DesignationId);
            return View(); 
        }


        //[HttpPost]
        //public ActionResult EmployeeUpdate()
        //{

        //    return View();
        //}
        //Employee Part end...


        //Designation Part Start ...

        public ActionResult DesignationAdd() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult DesignationAdd(Designation designation)
        {
            if(ModelState.IsValid)
            {
                db.Designations.Add(designation);
                db.SaveChanges();
                return RedirectToAction("DesignationAdd");
            }
            return View();
        }

        public ActionResult DesignationInformation()
        {
            return View(db.Designations.ToList());
        }

        [HttpGet]
        public ActionResult UpdateDesignation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation UpdateDesignation = db.Designations.Find(id);
            if (UpdateDesignation == null)
            {
                return HttpNotFound();
            }
            return View(UpdateDesignation);
        }

        [HttpPost]
        public ActionResult UpdateDesignation([Bind(Include = "DesignationId,DesignationName")] Designation UpdateDesignation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(UpdateDesignation).State = EntityState.Modified;
                db.SaveChanges(); 
                return RedirectToAction("DesignationInformation");
            }
            return View(UpdateDesignation);
        }
        //[HttpPost]
        //public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName,DesignationId,Employee_ID,Employee_Contact,Employee_Mail,Employee_Address,Employe_UserName,Employee_Password")] Employee UpdateDesignation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(UpdateDesignation).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("EmployeeInformation");
        //    }
        //    ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", UpdateDesignation.DesignationId);
        //    return View(UpdateDesignation);
        //}


        //Designation part end...
    }
}