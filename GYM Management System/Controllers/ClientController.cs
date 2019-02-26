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
    public class ClientController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClientRegistration()
        {
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
            return View();
        }

        //[HttpPost]
        //public ActionResult ClientRegistration(Client client)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Clients.Add(client);
        //        db.SaveChanges();
        //        return RedirectToAction("ClientRegistration");
            
        //    }

        //    return View("ClientRegistration");
        //}

        [HttpPost]
        public ActionResult ClientRegistration(Client client, int ServiceId )
        {
            if (ModelState.IsValid)
            {
                //int id = client.ClientIdNumber;
                //client.ClientIdNumber = id + 1;
                db.Clients.Add(client);

                ClientServiceList list = new ClientServiceList();
                list.ClientId = client.ClientId;
                list.ServiceId = ServiceId;
                db.ClientServiceLists.Add(list);

                ClientBill bill = new ClientBill();
                bill.ClientId = client.ClientId;
                bill.BillMonth = client.ClientGymStart;
                bill.BillStatus = false;
                bill.BillAmount = "8787";
                db.ClientBills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("ClientRegistration");

            }

            return View("ClientRegistration");
        }


        public ActionResult ClientInformation()
        {
            return View(db.Clients.ToList());
        }

        [HttpGet]
        public ActionResult ClientUpdate(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client ClientUpdate = db.Clients.Find(id);
            if(ClientUpdate == null)
            {
                return HttpNotFound();
            }
            return View(ClientUpdate);
        }

        [HttpPost]
        public ActionResult ClientUpdate([Bind(Include = "ClientId,ClietName,ClientIdNumber,ClientContact,ClientMail,ClientAddress,ClientGymStart,ClientUserName,ClientPassword,ClientAdmitionfee")] Client client) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientInformation");
            }
            return View("ClientUpdate");
        }

        public ActionResult ClientServicecAdd()
        {
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
            return View();
        }

        public ActionResult ClientServiceList()
        {

            return View(db.ClientServiceLists.ToList());
        }
    }
}