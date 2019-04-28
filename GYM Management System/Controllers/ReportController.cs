using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;

using System.Data;

namespace GYM_Management_System.Controllers
{
    public class ReportController : Controller
    {
        gym_managementEntities db = new gym_managementEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profit(String FromDate, String ToDate)
        {
            DateTime ft = Convert.ToDateTime(FromDate).Date;
            DateTime tt = Convert.ToDateTime(ToDate).Date;
            DataTable dt = new DataTable();
          //  dt = db.ClientBillTransections.ToList();
            var i = db.ClientBillTransections.AsEnumerable().Where(x => x.TransectionDate>=ft && x.TransectionDate<=tt).Sum(x => x.Amount);
            var Text = db.ClientBillTransections.AsEnumerable().Sum(x=>x.Amount);
            var Text2 = db.Expenses.AsEnumerable().Sum(x => x.ExpenseProductAmount);
            // var total = db.ClientBillTransections.Where(r => r.Bid == x).Sum(r => r.Fee);
            int profit = Text - Text2;

            ViewBag.m = Text.ToString();
            ViewBag.m2 = Text2.ToString();
            ViewBag.m3 = profit.ToString();
            return View();
        }
    }
}