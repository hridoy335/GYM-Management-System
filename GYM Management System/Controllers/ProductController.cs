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

        public ActionResult ProductStorage()
        {
            return View(db.Storages.ToList());
        }

        public ActionResult ProductAdd()
        {
            ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View(); 
        }
         
        [HttpPost]
        public ActionResult ProductAdd([Bind(Include  = "productplanid,productquantity,prodyctbuyingdate,productexpiredate,productbuyparprice,productsellparprice,totalamount,employeeid")] ProductBuying productbuying, int? ProductPlanId, int? EmployeeId)
        {
            int er = 0;
            if (ProductPlanId == null)
            {
                er++;
                ViewBag.ProductPlanMessage = "ProductPlan Required";
            }
            if (EmployeeId == null)
            {
                er++;
                ViewBag.EmployeeMessage = "Employee Required";
            }
            if (ModelState.IsValid)
            {
                
              //  db.SaveChanges();
                Storage storage = new Storage();
                storage.ProductPlanId = productbuying.ProductPlanId;
                //storage.ProductQuantity = productbuying.ProductQuantity;
                storage.ProductExpireDate = productbuying.ProductExpireDate;
                var productsearch = db.Storages.Where(x=>x.ProductPlanId==productbuying.ProductPlanId).FirstOrDefault();
                if (productsearch == null)
                {
                    storage.ProductQuantity = productbuying.ProductQuantity;
                    db.Storages.Add(storage);
                   // db.Entry(storage).State = EntityState.Modified;
                    db.ProductBuyings.Add(productbuying);
                    db.SaveChanges();
                    return RedirectToAction("ProoductBuyingView");

                    //return View(productbuying);
                }
                else
                {
                    int quantity = productsearch.ProductQuantity;
                    int buyquantity = productbuying.ProductQuantity;
                    int presentQuantity = quantity + buyquantity;
                    storage.ProductQuantity = presentQuantity;
                    storage.StorageId = productsearch.StorageId;
                   // db.Storages.Add(storage);
                    
                    
                    db.ProductBuyings.Add(productbuying);

                    db.Entry(productsearch).State = EntityState.Detached;

                    db.Entry(storage).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ProoductBuyingView", "Product");
                }


              
            }
            ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }


        public ActionResult ProductAddInfo()
        {
            return View(db.ProductBuyings.ToList());
        }

        public ActionResult ProductAddUpdate(int? id)
        {
            //ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client ServiceAdd = db.Clients.Find(id);
            ProductBuying productBuying = db.ProductBuyings.Find(id);
            if (productBuying == null)
            {
                return HttpNotFound();
            }
            ViewBag.quantity = productBuying.ProductQuantity;
            ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View(productBuying);
        }

        [HttpPost]
        public ActionResult ProductAddUpdate([Bind(Include = "ProductButingId,ProductPlanId,ProductQuantity,ProdyctBuyingDate,ProductExpireDate,ProductBuyParPrice,productSellParPrice,TotalAmount,EmployeeId")] ProductBuying productBuying, int? quantity)
        {
            int present_quantity=0;
            int calculatequantity = 0;
            int productplanid = productBuying.ProductPlanId;
            var storagequantity = db.Storages.Where(x => x.ProductPlanId == productplanid).FirstOrDefault();
            Storage storage = new Storage();
            if (quantity > productBuying.ProductQuantity)
            {
                int a = productBuying.ProductQuantity;
                int b =Convert.ToInt32( quantity);
                calculatequantity = b-a;
                present_quantity = storagequantity.ProductQuantity - calculatequantity;
                storage.ProductQuantity = present_quantity;  //present quantity
                storage.ProductExpireDate = productBuying.ProductExpireDate;
                storage.ProductPlanId = productplanid;
                storage.StorageId = storagequantity.StorageId;
                
            }
            else
            {
                int c = productBuying.ProductQuantity;
                int d = Convert.ToInt32(quantity);
                calculatequantity = c - d;
                present_quantity = storagequantity.ProductQuantity + calculatequantity;
                storage.ProductQuantity = present_quantity;
                storage.ProductExpireDate = productBuying.ProductExpireDate;
                storage.ProductPlanId = productplanid;
                storage.StorageId = storagequantity.StorageId;
            }


            if (ModelState.IsValid)
            {
                db.Entry(storagequantity).State = EntityState.Detached;
                db.Entry(storage).State = EntityState.Modified;

                db.Entry(productBuying).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Data Update Successfully";
                return RedirectToAction("ProductAddInfo");
            }
            return View(productBuying);
        }

        public ActionResult ProductSell()
        {
            ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.clientid = new SelectList(db.Clients, "clientid", "ClietName");
            return View();
        }

        //[HttpPost]
        //public ActionResult ProductSell()
        //{
            
        //}
    }
}