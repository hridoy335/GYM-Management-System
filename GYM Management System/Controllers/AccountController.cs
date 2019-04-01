﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class AccountController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientBill()
        {
            return View(db.ClientBills.ToList());
        }

        [HttpGet]
        public ActionResult BillPament(int?id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientBill clientbill = db.ClientBills.Find(id);
            if (clientbill == null)
            {
                return HttpNotFound();
            }
           // ViewBag.ClientId = new SelectList(db.ClientBills, "ClientId", "ClietName");
            return View(clientbill);
        }

        [HttpPost]
        public ActionResult BillPament([Bind(Include = "ClientBillId,ClientId,BillMonth,BillAmount,BillStatus, DueStatus")] ClientBill clientBill)
        {
            int bill = Convert.ToInt32(clientBill.BillAmount);
            int due = Convert.ToInt32(clientBill.DueStatus);
            int presentstatus = bill - due;
            clientBill.DueStatus = presentstatus;

            ClientBillTransection transection = new ClientBillTransection();
            transection.ClientBillId = clientBill.ClientBillId;
            transection.TransectionDate = DateTime.Now;
            transection.BillMonth = clientBill.BillMonth;
            transection.Amount = due;
            
            var id = db.ClientBillTransections.Where(x=>x.ClientBillId==clientBill.ClientBillId && x.BillMonth==clientBill.BillMonth && x.BillStatus==false);
            if (id == null)
            {

                ViewBag.Message = "This bill is already payment";
                return View(clientBill);
            }
            else
            {
                
                db.ClientBillTransections.Add(transection);
                db.Entry(clientBill).State = EntityState.Modified;
                db.SaveChanges();

                //ViewBag.transectionid = transection.TransectionId;
                return RedirectToAction(actionName: "PrintTransectionInfo", routeValues: new { id = transection.TransectionId });

            }
        }
        [HttpGet]
        public ActionResult PrintTransectionInfo(int?id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Servicess ServiceUpdate = db.Servicesses.Find(id);
            ClientBillTransection clientBillTransection = db.ClientBillTransections.Find(id);
            if (clientBillTransection == null)
            {
                return HttpNotFound();
            }
            return View(clientBillTransection);
        }

        [HttpGet]
        public ActionResult CreateBill()
        {
            return View();
        }

       // [HttpPost]
        public ActionResult BillCreate(string button)
        {
            System.DateTime todaydate = new System.DateTime(System.DateTime.Today.Ticks);
            DateTime Current_date = Convert.ToDateTime(todaydate.ToString("dd/MM/yyyy"));

            //var billdate = db.Clients.Where(y => y.ClientGymStart == date).FirstOrDefault();
            var clientinfo = db.Clients.ToList();

            List<ClientBill> list = new List<ClientBill>();
            ViewBag.list = list;
             //client = new Client();
           // TimeSpan ts = new TimeSpan();
            foreach (var gymstart in clientinfo)
            {
                //DateTime currentdays = 
                //TimeSpan ts = new TimeSpan();
               // ts = date.Subtract(client.ClientGymStart);
                
                var gymstartdat = gymstart.ClientGymStart.ToShortDateString();
                int clientid = gymstart.ClientId;
                string clientname = gymstart.ClietName;
                var search_active_service = db.ClientServiceLists.Where(a=>a.ClientId==clientid).FirstOrDefault();
                if (search_active_service == null)
                {
                    continue;
                }
                else
                {
                    DateTime day = Convert.ToDateTime(gymstartdat);
                    int i =Convert.ToInt32( (Current_date - day).TotalDays);
                    int service_id = search_active_service.ServiceId;
                    var service = db.Servicesses.Where(b => b.ServiceId == service_id).FirstOrDefault();
                    int service_price = service.ServieAmount;
                    int service_day = service.ServiceDay;
                    var Last_client_bill = db.ClientBills.Where(c => c.ClientId == clientid).OrderByDescending(d=>d.BillMonth).FirstOrDefault();

                   

                    ClientBill clientBill = new ClientBill();
                    if (Last_client_bill == null)
                    {
                        int totaldays = i / service_day;
                        if (totaldays == 1)
                        {
                            clientBill.ClientId = clientid;
                            clientBill.BillAmount = service_price;
                            clientBill.BillStatus = false;
                            clientBill.DueStatus = 0;
                            clientBill.BillMonth = Current_date;
                            db.ClientBills.Add(clientBill);
                            db.SaveChanges();
                            list.Add(clientBill);
                        }
                    }
                    else
                    {
                        DateTime last_timeBill_date = Convert.ToDateTime(Last_client_bill.BillMonth);
                        int totaldays2 = Convert.ToInt32((Current_date - last_timeBill_date).TotalDays);
                        int j = totaldays2 / service_day;
                        if (j == 1)
                        {
                            clientBill.ClientId = clientid;
                            clientBill.BillAmount = service_price;
                            clientBill.BillStatus = false;
                            clientBill.DueStatus = 0;
                            clientBill.BillMonth = Current_date;
                            db.ClientBills.Add(clientBill);
                            db.SaveChanges();
                            list.Add(clientBill);
                        }


                    }

                }

            }

            //return RedirectToAction("NewBill","Account");
            return View();
        }

        public ActionResult BillTransection() 
        {
            return View(db.ClientBillTransections.ToList());
        }

    }
}