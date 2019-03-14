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
    public class ClientSiteClientController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: ClientSiteClient
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login","Login");
            }
            
        }
        public ActionResult ClientInfo()
        {
            if (Session["id"] != null)
            {
            int x = Convert.ToInt32(Session["id"]);
            return View(db.Clients.Where(y=>y.ClientId==x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public ActionResult ScheduleInfo()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.Schedules.Where(y => y.ClientId == x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult FoodPlan()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.FoodPlans.Where(y => y.ClientId == x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult AttendenceInfo()
        {
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                return View(db.Attendences.Where(y => y.ClientId == x).ToList());

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult About()
        {
            if (Session["id"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult Contact() 
        {
            if (Session["id"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult ClientInfoUpdate() 
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                if (Session["id"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client clientUpdate = db.Clients.Find(id);
                if (clientUpdate == null) 
                {

                    return HttpNotFound();
                }
                // ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
                return View(clientUpdate);

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult ClientInfoUpdate([Bind(Include = "ClientId,ClietName,ClientIdNumber,ClientContact,ClientMail,ClientAddress,ClientGymStart,ClientUserName,ClientPassword,ClientAdmitionfee")] Client client )
        {
            
            if (ModelState.IsValid)
            {
                
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientInfo");
            }
            return View();
        }

    }
}