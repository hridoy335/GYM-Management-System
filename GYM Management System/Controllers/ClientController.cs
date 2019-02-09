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
    public class ClientController : Controller
    {
        Models.gym_managementEntities db = new Models.gym_managementEntities();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClientRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientRegistration(Client client)
        {
            if(ModelState.IsValid)
            {
                db.Clients.Add(client);
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
        public ActionResult ClientUpdate([Bind(Include = "ClientId,ClietName,ClientIdNumber,ClientContact,ClientMail,ClientAddress,ClientGymStart,ClientUserName,ClientPassword,ClientAdmitionfee")] Client ClientUpdate) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(ClientUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientInformation");
            }
            return View(ClientUpdate);
        }

    }
}