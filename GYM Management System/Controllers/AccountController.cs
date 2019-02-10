﻿using System;
using System.Collections.Generic;
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
            return View(clientbill);
        }
    }
}