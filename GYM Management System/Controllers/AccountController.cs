using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GYM_Management_System.Models;
using Rotativa;

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

        public ActionResult ClientBill(string search)
        {
            int i = 0;
            var client = from c in db.ClientBills select c;
            if (!String.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out i))
                {

                    // int a = Convert.ToInt32(search);
                   var a= db.Clients.Where(x => x.ClientIdNumber == i).FirstOrDefault();
                    if (a == null)
                    {
                        ViewBag.message = "Please Insert Valid Client ID Number ...";
                    }
                    else
                    {
                        int id = Convert.ToInt32(a.ClientId);
                        client = db.ClientBills.Where(h => h.ClientId == a.ClientId);
                    }
              
                        
                }
                else
                {
                    // client = db.Clients.Where(x => x.ClietName == search);
                    ViewBag.message = "Please Insert Valid Client ID Number ...";
                }
            }

             return View(client.ToList());
                //var i = db.ClientBills.ToList();
                //return Json(new { data=i},JsonRequestBehavior.AllowGet);
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
            ViewBag.payment = 00;
            return View(clientbill);
        }

        [HttpPost]
        public ActionResult BillPament([Bind(Include = "ClientBillId,ClientId,BillMonth,BillAmount,BillStatus, DueStatus")] ClientBill clientBill, int? payment)
        {
           
            int bill = Convert.ToInt32(clientBill.BillAmount);
            int due = Convert.ToInt32(clientBill.DueStatus);
            int paymentBill = Convert.ToInt32(payment);
            int er = 0;
            if (paymentBill == 0)
            {
                er++;
                ViewBag.paymentmessage = "Insert Payment Amount";
            }
            if (er > 0)
            {
                return View(clientBill);
            }
                // int presentstatus = bill - paymentBill;
                // clientBill.DueStatus = presentstatus;
            int id = Convert.ToInt32(clientBill.ClientBillId);
            int i;
            var status = db.ClientBills.Where(y=>y.ClientBillId==id).FirstOrDefault();
            if (status.BillStatus == false)
            {

                if (due == 0)
                {
                    if (bill >= paymentBill)
                    {
                         i = bill - paymentBill;
                        if (i == 0)
                        {
                            due = 0;
                            clientBill.DueStatus = due;
                            clientBill.BillStatus = true;
                        }
                        else
                        {
                            due = i;
                            clientBill.DueStatus = due;
                            clientBill.BillStatus = false;
                        }
                    }
                    else
                    {
                        ViewBag.error1 = "Payment bill must less than Current bill";
                        return View(clientBill);
                    }
                }
                else
                {
                    if (due >= paymentBill)
                    {
                         i  = due - paymentBill;
                        if (i  == 0)
                        {
                            due = 0;
                            clientBill.DueStatus = due;
                            clientBill.BillStatus = true;
                        }
                        else
                        {
                            due = i;
                            clientBill.DueStatus = due;
                            clientBill.BillStatus = false;
                        }
                    }
                    else
                    {
                        ViewBag.error2 = "Payment bill must less than Due Status bill";
                        return View(clientBill);
                    }
                }
            }
            else
            {
                ViewBag.Message = "This bill is already payment";
                return View(clientBill);
            }

            ClientBillTransection transection = new ClientBillTransection();
            transection.ClientBillId = clientBill.ClientBillId;
            transection.TransectionDate = DateTime.Now;
            transection.BillMonth = clientBill.BillMonth;
            transection.Amount = paymentBill;
            transection.BillStatus = clientBill.BillStatus;
            var a= Convert.ToInt32( db.ClientBillTransections.ToList().Max(e => Convert.ToInt64(e.InvoiceNumber)));
            int h = a+1;
            transection.InvoiceNumber = Convert.ToString(h);

            var id2 = db.ClientBillTransections.Where(x => x.ClientBillId == clientBill.ClientBillId && x.BillMonth == clientBill.BillMonth && x.BillStatus == false);
            if (id2 == null) 
            {

                ViewBag.Message = "This bill is already payment";
                return View(clientBill);
            }
            else
            {

                db.ClientBillTransections.Add(transection);
                db.Entry(status).State = EntityState.Detached;
                db.Entry(clientBill).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.transectionid = transection.TransectionId;
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

        public ActionResult BillTransection(String search) 
        {
            int i = 0;
            var client = from c in db.ClientBillTransections select c;
            if (!String.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out i))
                {

                    // int a = Convert.ToInt32(search);
                    var a = db.Clients.Where(x => x.ClientIdNumber == i).FirstOrDefault();
                   // int id = Convert.ToInt32(a.ClientId);
                   // var billid = db.ClientBills.Where(h => h.ClientId == a.ClientId);
                    if (a == null)
                    {
                        ViewBag.message = "Please Insert Valid Client ID Number ...";
                    }
                    else
                    {
                        var billid = db.ClientBills.Where(k => k.ClientId == a.ClientId).FirstOrDefault();

                        if (billid==null)
                        {
                            ViewBag.message2 = "There is no transection ...";
                        }
                        else
                        {
                            int id2 = Convert.ToInt32(billid.ClientBillId);
                            client = db.ClientBillTransections.Where(h => h.ClientBillId == id2);
                        }
                        
                        
                    }


                }
                else
                {
                    // client = db.Clients.Where(x => x.ClietName == search);
                    ViewBag.message = "Please Insert Valid Client ID Number ...";
                }
            }

            return View(client.ToList());
            //return View(db.ClientBillTransections.ToList());
        }

        public ActionResult Print()
        {
            var q = new ActionAsPdf("ClientBill");
            return q;
        }

       public ActionResult Report(String ReportType)
        {
            return View();
        }


    }
}