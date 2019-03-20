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
    public class ProductController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Product 
      

        [HttpGet]
        public ActionResult ProductPlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProductPlan(ProductPlan productPlan)   
        {
            if (ModelState.IsValid)
            {
                db.ProductPlans.Add(productPlan);
                db.SaveChanges();
                return RedirectToAction("ProductPlanInfo");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ProductPlanInfo()
        {
            return View(db.ProductPlans.ToList());
        }

        [HttpGet]
        public ActionResult UpdateProductPlan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Servicess ServiceUpdate = db.Servicesses.Find(id);
            ProductPlan productPlan = db.ProductPlans.Find(id);
            if (productPlan == null)
            {
                return HttpNotFound();
            }
            return View(productPlan); 
        }

        [HttpPost]
        public ActionResult UpdateProductPlan([Bind(Include = "ProductPlanId,ProductName")] ProductPlan productPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductPlanInfo");
            }
            return View(productPlan);
        }

        [HttpGet]
        public ActionResult DeleteProductPlan(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Servicess ServiceUpdate = db.Servicesses.Find(id);
            ProductPlan productPlan = db.ProductPlans.Find(id);
            if (productPlan == null)
            {
                return HttpNotFound();
            }
            return View(productPlan);
        }

        [HttpPost]
        public ActionResult DeleteProductPlan(int id) 
        {
            ProductPlan productPlan = db.ProductPlans.Find(id);
            db.ProductPlans.Remove(productPlan);
            db.SaveChanges();
            return RedirectToAction("ProductPlanInfo");
        }


    }
}