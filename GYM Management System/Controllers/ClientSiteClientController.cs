using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}