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
    public class ServiceController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ServiceAdd(Servicess servicess)
        {
            if(ModelState.IsValid)
            {
                db.Servicesses.Add(servicess);
                db.SaveChanges();
                return RedirectToAction("ServiceAdd");
            }
            return View();
        }

        public ActionResult ServiceInformation()
        {
            return View(db.Servicesses.ToList());
        }
        [HttpGet]
        public ActionResult ServiceUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicess ServiceUpdate = db.Servicesses.Find(id);
            if (ServiceUpdate == null)
            {
                return HttpNotFound();
            }
            return View(ServiceUpdate);

        }

        [HttpPost]
        public ActionResult ServiceUpdate([Bind(Include = "ServiceId,ServiceName,ServiceDay,ServieAmount")] Servicess servicess)
        {

            if (ModelState.IsValid)
            {
                db.Entry(servicess).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ServiceInformation");
            }
            return View("ServiceUpdate");
        }
    }
}