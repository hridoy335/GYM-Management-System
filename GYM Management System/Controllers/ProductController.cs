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
    public class ProductController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Product 
      

        [HttpGet]
        public ActionResult ProductPlan()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View(db.ProductPlans.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult UpdateProductPlan(int? id)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
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
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
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
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                return View(db.Storages.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ProductAdd()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
               // ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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
            //if (EmployeeId == null)
            //{
            //    er++;
            //    ViewBag.EmployeeMessage = "Employee Required";
            //}
            if (ModelState.IsValid)
            {
                int ab = Convert.ToInt32(Session["id"]);
                productbuying.EmployeeId = ab;

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
                    return RedirectToAction("ProductAddInfo");

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
                    return RedirectToAction("ProductAddInfo");
                }


              
            }
            ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View(productbuying);
        }


        public ActionResult ProductAddInfo(String FromDate, String ToDate)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                var buyinfo = from c in db.ProductBuyings select c;
                if (!String.IsNullOrEmpty(FromDate) && !String.IsNullOrEmpty(ToDate))
                {
                    var ft = Convert.ToDateTime(FromDate).Date;
                    DateTime tt = Convert.ToDateTime(ToDate).Date;
                    if (ft > tt)
                    {
                        ViewBag.meaasge = "Date is not right format ...";
                    }

                    else
                    {
                        buyinfo = db.ProductBuyings.Where(x => x.ProdyctBuyingDate >= ft && x.ProdyctBuyingDate <= tt);
                    }
                    
                }
               return View(buyinfo.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ProductAddUpdate(int? id)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
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
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
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

        [HttpGet]
        public ActionResult ProductSell()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
               // ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                ViewBag.clientid = new SelectList(db.Clients, "clientid", "ClietName");
                return View();
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }



        [HttpPost]
        public ActionResult ProductSell([Bind(Include = "ProductPlanId,ProductQuantity,TotalAmount,EmployeeId,clientid,SellDate")] Sell sell, int? ProductPlanId, int? EmployeeId,int? clientid,int? ProductQuantity)
        {
            int er = 0;
            if (ProductPlanId == null)
            {
                er++; ;
                ViewBag.EmployeeIdMessage = "ProductPlan Namre Required";
            }
            if (clientid == null)
            {
                er++; ;
                ViewBag.clientidMessage = "Client Namre Required";
            }

            //if (EmployeeId == null)
            //{
            //    er++; ;
            //    ViewBag.EmployeeIdMessage = "Employee Namre Required"; 
            //}

            if (ProductQuantity==null)
            {
                er++;
                ViewBag.ProductQuantity = "Quantity Required";
            }
            int id = Convert.ToInt32(sell.ProductPlanId);
            var product_storage = db.Storages.Where(x => x.ProductPlanId == id).FirstOrDefault();
            int quantity = sell.ProductQuantity;
           
            if (er > 0)
            {

                ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                ViewBag.clientid = new SelectList(db.Clients, "clientid", "ClietName");
                return View(sell);
            }
        


            if (ModelState.IsValid)
            {
                if (product_storage.ProductQuantity < quantity)
                {
                    er++;
                    ViewBag.Product_Storage = "Sorry.This product is not available";
                    ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
                    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                    ViewBag.clientid = new SelectList(db.Clients, "clientid", "ClietName");
                    return View(sell);
                }
                int present_quantiy = product_storage.ProductQuantity - quantity;
                int ab = Convert.ToInt32(Session["id"]);
                sell.EmployeeId = ab;
              //  sell.SellDate = DateTime.Now.Date;

                Storage storage = new Storage();
                storage.StorageId = product_storage.StorageId;
                storage.ProductPlanId = product_storage.ProductPlanId;
                storage.ProductQuantity = present_quantiy;
                storage.ProductExpireDate = product_storage.ProductExpireDate;

                db.Entry(product_storage).State = EntityState.Detached;
                db.Entry(storage).State = EntityState.Modified;
                db.Sells.Add(sell);
                db.SaveChanges();
                return RedirectToAction("ProductSellInfo");
            }

            ViewBag.ProductPlanId = new SelectList(db.ProductPlans, "ProductPlanId", "ProductName");
           // ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.clientid = new SelectList(db.Clients, "clientid", "ClietName");
            return View(sell);
            //return View();
        }

        public ActionResult ProductSellInfo(String FromDate, String ToDate)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 1)
            {
                var sellinfo = from c in db.Sells select c;
                if (!String.IsNullOrEmpty(FromDate) && !String.IsNullOrEmpty(ToDate))
                {
                    var ft = Convert.ToDateTime(FromDate).Date;
                    DateTime tt = Convert.ToDateTime(ToDate).Date;
                    if (ft > tt)
                    {
                        ViewBag.meaasge = "Date is not right format ...";
                    }

                    else
                    {
                        sellinfo = db.Sells.Where(x => x.SellDate >= ft && x.SellDate <= tt);
                    }

                }
                return View(sellinfo.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }


    }
}