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
        gym_managementEntities db = new gym_managementEntities();
       
        // GET: Clienti
        public ActionResult Index()
        {
            return View();
        }
        
        // Client Registration Start .....

        public ActionResult ClientRegistration()
        {
            //List<Servicess> serviceList = db.Servicesses.ToList();
            //serviceList.Add(new Servicess() { ServiceId = 0, ServiceName = "Select One" });
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
            ViewBag.ScheduleTimeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
            

        }

        [HttpPost]
        public ActionResult ClientRegistration(Client client, int? ServiceId, int? ClientIdNumber,string ClientPassword, int? EmployeeId, int? ScheduleTimeId)
        {
            int er = 0;
            if (ServiceId==null)
            {
                ViewBag.ename = "Select Service Name";
                er++;
            }
            if (ClientPassword == "")
            {
                er++;
                ViewBag.password = "Password Required";
            }
            if (EmployeeId == null)
            {
                er++;
                ViewBag.Employee = "Select Employee Name ";
            }
            if (ScheduleTimeId == null)
            {
                er++;
                ViewBag.ScheduleTime = "Select Schedule Name ";
            }
            bool isThisIdExist = db.Clients.ToList().Exists(a=>a.ClientIdNumber==client.ClientIdNumber);
            //int a = Convert.ToInt32(ClientIdNumber);
            //var idnumber = db.Clients.Where(x => x.ClientId == a).FirstOrDefault();
            if (isThisIdExist)
            {
                er++;
                ViewBag.idnumber = "This id is already here";
            }
            bool isThisUsenameExist = db.Clients.ToList().Exists(a => a.ClientUserName == client.ClientUserName);
            if (isThisUsenameExist)
            { 
                er++;
                ViewBag.username = "This Username is already here";
            }

            if (er > 0)
            {
                //idnumber = 0;
                ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
                ViewBag.ScheduleTimeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var id = db.Clients.Max(p=>p.ClientIdNumber);
                    client.ClientIdNumber = id + 1;
                    db.Clients.Add(client);

                    ClientServiceList list = new ClientServiceList();
                    list.ClientId = client.ClientId;
                    list.ServiceId =Convert.ToInt32( ServiceId);
                    list.Service_Status = true;
                    db.ClientServiceLists.Add(list);

                    ClientBill bill = new ClientBill();
                    bill.ClientId = client.ClientId;
                    bill.BillMonth = client.ClientGymStart;
                    bill.BillStatus = false;
                    var serviceprice = db.Servicesses.Where(x => x.ServiceId == ServiceId).FirstOrDefault().ServieAmount;
                    bill.BillAmount = Convert.ToInt16(serviceprice);
                    bill.DueStatus = 0;
                    db.ClientBills.Add(bill);

                    Schedule schedule = new Schedule();
                    schedule.EmployeeId = Convert.ToInt32(EmployeeId);
                    schedule.ScheduleTimeId = Convert.ToInt32(ScheduleTimeId);
                    schedule.ClientId = client.ClientId;
                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    //ViewBag.Message = "Registration Sucessful";
                    TempData["Success"] = "Registration Successfully!";
                   // ViewBag.clientid = client.ClientId;
                    return RedirectToAction(actionName: "ClientRegistrationprint", routeValues: new { id = client.ClientId });
                  //  return RedirectToAction("ClientInformation");
                    

                }
            }
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
            return View();
        }

        // Client Registration End .....

        public ActionResult ClientRegistrationprint(int?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Servicess ServiceUpdate = db.Servicesses.Find(id);
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // Client Information Start ....
        public ActionResult ClientInformation(string search)
        {
            int i = 0;
            var client = from c in db.Clients select c;
            if (!String.IsNullOrEmpty(search))
            {
                if (int.TryParse(search, out i))
                {
                  
                   // int a = Convert.ToInt32(search);
                    client = db.Clients.Where(x =>x.ClientIdNumber == i);
                }
                else
                {
                    client = db.Clients.Where(x => x.ClietName == search);
                }
            }
           



            return View(client.ToList());
        }

        // Client Information End ......
 

        // Client Update Start ......
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

        // Client Update End .....

        
        // Client Service Add Start .....
        public ActionResult ClientServicecAdd()
        {
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
            return View();
        }

        // Client Service Add End .....

        //Client Service List Start 
        public ActionResult ClientServiceList(String search)
        {

            // return View(db.ClientServiceLists.ToList());

            int i = 0;
            var client = from c in db.ClientServiceLists select c;
            if (!String.IsNullOrEmpty(search))
            {
               
                if (int.TryParse(search, out i))
                {
                    var id  = db.Clients.Where(x => x.ClientIdNumber == i).FirstOrDefault();
                    // int a = Convert.ToInt32(search);
                    // var  a = db.Clients.Find(i).ClientId;
                    //if (id.ClientIdNumber != 0)
                    //{
                    //    int ab =Convert.ToInt32( id.ClientIdNumber);
                    if (id == null)
                    {
                        ViewBag.message = "Please insert valid client id number ..";
                    }
                    else
                    {
                      int  b = Convert.ToInt32(id.ClientId);
                        client = db.ClientServiceLists.Where(a => a.ClientId == b);
                    }
                       
                        //client = client1;
                    //}
                 
                }
                else
                {
                 //   client = db.Clients.Where(x => x.ClietName == search);
                }
            }
            return View(client.ToList());
        }

        // Client Service List End .....

        // Client Service List Update Start ......

        public ActionResult ClientServiceListUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client ClientUpdate = db.Clients.Find(id);
            ClientServiceList ServiceUpdate = db.ClientServiceLists.Find(id);
            if (ServiceUpdate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName", ServiceUpdate.ServiceId);
            return View(ServiceUpdate);
        }

        [HttpPost]
        public ActionResult ClientServiceListUpdate([Bind(Include = "ClientServiceListId,ClientId,ServiceId,Service_Status")] ClientServiceList clientServiceList, int? ServiceId)
        {
            int er = 0;
            if(ServiceId== null)
            {
                er++;
                ViewBag.Message = "Select One Item";
            }
            if(er>0)
            {
                clientServiceList.Client = db.Clients.FirstOrDefault(a=>a.ClientId==clientServiceList.ClientId);
                ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
                return View(clientServiceList);
            }

            if (ModelState.IsValid)
            {
                db.Entry(clientServiceList).State = EntityState.Modified;
                //db.SaveChanges();

                //ClientBill bill = new ClientBill();
                //int ClientId = clientServiceList.ClientId;
                //bill.ClientId = ClientId;
                //int serviceid = clientServiceList.ServiceId;
                //int amount = db.Servicesses.Where(x => x.ServiceId == serviceid).FirstOrDefault().ServieAmount;
                //int billid = db.ClientBills.Where(x => x.ClientId == ClientId).FirstOrDefault().ClientBillId;
                //bill.ClientBillId = billid;
                //bill.BillAmount = amount;
                //db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientServiceList");
            }

            return View();
        }
        // Client Service List Update End ......


        // Client Service Status Update Start ......
        [HttpGet]
        public ActionResult ServiceStatusUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientServiceList ServiceStatusUpdate = db.ClientServiceLists.Find(id);
            if (ServiceStatusUpdate == null) 
            {
                return HttpNotFound();
            }
           
            return View(ServiceStatusUpdate);
        }
        
        [HttpPost]
        public ActionResult ServiceStatusUpdate([Bind(Include = "ClientServiceListId,ClientId,ServiceId,Service_Status")] ClientServiceList clientServiceList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientServiceList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientServiceList");
            }

            return View();
        }

        // Client Service Status Update End ......


        // New Service Add start .......

        [HttpGet]
        public ActionResult NewServiceAdd(int? id)
        {
            ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
            ViewBag.EmployeeId = new SelectList(db.Employees,"EmployeeId","EmployeeName");
            ViewBag.ScheduleTimeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client ServiceAdd = db.Clients.Find(id);
            if (ServiceAdd == null)
            {
                return HttpNotFound();
            }
            return View(ServiceAdd);           
        }

       
        [HttpPost]
        public ActionResult NewServiceAdd(Client client, int? ServiceId, int? ScheduleTimeId, int? EmployeeId)
        {
            int er = 0;
            if(ServiceId==null)
            {
                er++;
                ViewBag.Service = "Select One Item";
            }

            if(ScheduleTimeId==null)
            {
                er++;
                ViewBag.ScheduleTime = "Select One Item";
            }
            if(EmployeeId==null)
            {
                er++;
                ViewBag.Employee = "Select One Item";
            }
            if (er > 0)
            {
                ViewBag.ServiceId = new SelectList(db.Servicesses, "ServiceId", "ServiceName");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
                ViewBag.ScheduleTimeId = new SelectList(db.ScheduleTimes, "ScheduleTimeId", "ScheduleName");
                return View(client);
            }
            else
            {

                ClientServiceList list = new ClientServiceList();
                list.ClientId = client.ClientId;
                list.ServiceId = Convert.ToInt32(ServiceId);
                list.Service_Status = true;
                db.ClientServiceLists.Add(list);

                ClientBill bill = new ClientBill();
                bill.ClientId = client.ClientId;
                bill.BillMonth = client.ClientGymStart;
                bill.BillStatus = false;
                var serviceprice = db.Servicesses.Where(x => x.ServiceId == ServiceId).FirstOrDefault().ServieAmount;
                bill.BillAmount = Convert.ToInt16(serviceprice);
                bill.DueStatus = 0;
                db.ClientBills.Add(bill);

                Schedule schedule = new Schedule();
                schedule.ClientId = client.ClientId;
                schedule.EmployeeId = Convert.ToInt32(EmployeeId);
                schedule.ScheduleTimeId = Convert.ToInt32(ScheduleTimeId);
                db.Schedules.Add(schedule);
                db.SaveChanges();
                ViewBag.Message = "Service Add Sucessful";
                return RedirectToAction("ClientInformation"); 
            }
    
        }

        // New Service Add End ........
    }
}