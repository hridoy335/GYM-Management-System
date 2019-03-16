using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class ProductController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Product 
        public ActionResult ProductPlanInfo()
        {
            return View(db.ProductPlans.ToList());
        }

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
                return View("ProductPlanInfo");
            }
            else
            {
                return View();
            }
        }

        


    }
}