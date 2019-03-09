﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

namespace GYM_Management_System.Controllers
{
    public class LoginController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Login
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            int er = 0;
            if (username=="")
            {
                er++;
                ViewBag.username = "Username required";
            }
            if (password == "")
            {
                er++;
                ViewBag.password = "Password required";
            }
            if(er>0)
            {
                return View();
            }

            var Login = db.Clients.Where(x => x.ClientUserName == username && x.ClientPassword == password).FirstOrDefault();

            if (Login == null)
            {
                var tea = db.Employees.Where(x => x.Employe_UserName == username && x.Employee_Password == password).FirstOrDefault();
                if (tea == null)
                {
                    ViewBag.message = "Login fail";
                    return View("Login");
                }
                else
                {
                    Session["username"] = tea.Employe_UserName.ToString();
                    Session["id"] = tea.EmployeeId.ToString();
              

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {

                Session["id"] = Login.ClientId.ToString();
                Session["Name"] = Login.ClietName.ToString();

                return RedirectToAction("Index", "ClientSiteClient");
            }


        }

      
    }
}