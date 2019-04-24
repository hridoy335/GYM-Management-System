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
    public class ManagerServiceController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ManagerService
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ServiceAdd()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
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
        public ActionResult ServiceAdd(Servicess servicess)
        {
            if (ModelState.IsValid)
            {
                db.Servicesses.Add(servicess);
                db.SaveChanges();
                return RedirectToAction("ServiceInformation");
            }
            return View();
        }

        public ActionResult ServiceInformation()
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
            {
                return View(db.Servicesses.ToList());
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult ServiceUpdate(int? id)
        {
            int ab = Convert.ToInt32(Session["id"]);
            int bc = Convert.ToInt32(Session["Designation"]);
            if (ab != 0 && bc == 2)
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
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }

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
            return View(servicess);
        }

    }
    }