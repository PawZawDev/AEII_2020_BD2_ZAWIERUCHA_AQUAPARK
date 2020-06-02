using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aquapark.Models;

namespace Aquapark.Controllers
{
    public class ClientTicketsController : Controller
    {
        private Entities db = new Entities();

        // GET: ClientTickets
        [Authorize(Roles = "Admin,Manager,SuperManager")]
        public ActionResult Index(string attractionNameSearch, int? ticketTypeSearch, DateTime? dateSearch,int? isActiveSearch,int? wasPaidSearch)
        {
            ViewBag.searchDate = dateSearch;

            Dictionary<int, string> ticketTypeList = new Dictionary<int, string>();
            ticketTypeList.Add(1, "Entries");
            ticketTypeList.Add(2, "Duration");

            var ticketTypeSearchList = ticketTypeList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            ViewBag.ticketTypeSearchList = new SelectList(ticketTypeSearchList, "Id", "Value");


            Dictionary<int, string> isActiveList = new Dictionary<int, string>();
            isActiveList.Add(1, "Inactive");
            isActiveList.Add(2, "All");

            var isActiveSearchList = isActiveList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            ViewBag.isActiveSearchList = new SelectList(isActiveSearchList, "Id", "Value");


            Dictionary<int, string> wasPaidList = new Dictionary<int, string>();
            wasPaidList.Add(1, "Yes");
            wasPaidList.Add(2, "All");

            var wasPaidSearchList = wasPaidList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            ViewBag.wasPaidSearchList = new SelectList(wasPaidSearchList, "Id", "Value");


            var clientTicket = db.ClientTicket.Include(c => c.Wristband).Include(c => c.TicketInPriceList).Select(n => n);


            if(attractionNameSearch!=null)
            {
                clientTicket = clientTicket.Where(n => n.TicketInPriceList.Name.Contains(attractionNameSearch));
            }


            if (ticketTypeSearch == 1)
            {
                clientTicket = clientTicket.Where(n => n.EntriesLeft != null);
            }
            if (ticketTypeSearch == 2)
            {
                clientTicket = clientTicket.Where(n => n.EntriesLeft == null);
            }

            if (dateSearch != null)
            {
                clientTicket = clientTicket.Where(n => n.ActivationDate <= dateSearch).Where(n => (n.ExpirationDate >= dateSearch));
            }

            if (wasPaidSearch == 1)
            {
                clientTicket = clientTicket.Where(n => n.WasPaid == true);
            }
            if (wasPaidSearch == null)
            {
                clientTicket = clientTicket.Where(n => n.WasPaid == false);
            }

            if (isActiveSearch == 1)
            {
                clientTicket = clientTicket.Where(n => n.IsActive == false);
            }
            if (isActiveSearch == null)
            {
                clientTicket = clientTicket.Where(n => n.IsActive == true);
            }

            clientTicket.OrderBy(n => n.TicketInPriceList.Name);

            if (Request.IsAjaxRequest())
                return PartialView("IndexSearch", clientTicket.ToList());

            return View(clientTicket.ToList());
        }

        // GET: ClientTickets/Details/5
        [Authorize(Roles = "Admin,Manager,SuperManager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientTicket clientTicket = db.ClientTicket.Find(id);
            if (clientTicket == null)
            {
                return HttpNotFound();
            }
            return View(clientTicket);
        }

        // GET: ClientTickets/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id");
            ViewBag.IdTicketInPriceList = new SelectList(db.TicketInPriceList, "Id", "Name");
            return View();
        }

        // POST: ClientTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsActive,EntriesLeft,WasPaid,ActivationDate,ExpirationDate,IdTicketInPriceList,IdWristband")] ClientTicket clientTicket)
        {
            if (ModelState.IsValid)
            {
                db.ClientTicket.Add(clientTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientTicket.IdWristband);
            ViewBag.IdTicketInPriceList = new SelectList(db.TicketInPriceList, "Id", "Name", clientTicket.IdTicketInPriceList);
            return View(clientTicket);
        }

        // GET: ClientTickets/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientTicket clientTicket = db.ClientTicket.Find(id);
            if (clientTicket == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientTicket.IdWristband);
            ViewBag.IdTicketInPriceList = new SelectList(db.TicketInPriceList, "Id", "Name", clientTicket.IdTicketInPriceList);
            return View(clientTicket);
        }

        // POST: ClientTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,EntriesLeft,WasPaid,ActivationDate,ExpirationDate,IdTicketInPriceList,IdWristband")] ClientTicket clientTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientTicket.IdWristband);
            ViewBag.IdTicketInPriceList = new SelectList(db.TicketInPriceList, "Id", "Name", clientTicket.IdTicketInPriceList);
            return View(clientTicket);
        }

        // GET: ClientTickets/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientTicket clientTicket = db.ClientTicket.Find(id);
            if (clientTicket == null)
            {
                return HttpNotFound();
            }
            return View(clientTicket);
        }

        // POST: ClientTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientTicket clientTicket = db.ClientTicket.Find(id);
            db.ClientTicket.Remove(clientTicket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }





        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult CreateTicket(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ClientTicket clientTicket = new ClientTicket();
            clientTicket.IdWristband = id ?? 0;

            if (db.Wristband.Find(clientTicket.IdWristband).IsUsed == false)
            {
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => d.Attraction.Name.Contains("Aquapark")).Where(n => n.EndDate == null);

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name");
            }
            else
            {
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => !d.Attraction.Name.Contains("Aquapark")).Where(n => n.EndDate == null);

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name");
            }

            return View(clientTicket);
        }

        // POST: ClientTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicket([Bind(Include = "Id,IsActive,EntriesLeft,WasPaid,ActivationDate,ExpirationDate,IdTicketInPriceList,IdWristband")] ClientTicket clientTicket)
        {
            clientTicket.IsActive = true;
            clientTicket.ActivationDate = DateTime.Now;
            clientTicket.WasPaid = false;

            clientTicket.EntriesLeft = db.TicketInPriceList.Find(clientTicket.IdTicketInPriceList).Entries;
            clientTicket.ExpirationDate = clientTicket.ActivationDate;
            clientTicket.ExpirationDate = clientTicket.ExpirationDate.Add(db.TicketInPriceList.Find(clientTicket.IdTicketInPriceList).Duration);



            if (ModelState.IsValid)
            {
                db.Wristband.Find(clientTicket.IdWristband).IsUsed = true;

                db.ClientTicket.Add(clientTicket);
                db.SaveChanges();
                return RedirectToAction("IndexWristband", new { id = clientTicket.IdWristband });
            }

            //ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientTicket.IdWristband);

            if (db.Wristband.Find(clientTicket.IdWristband).IsUsed == false)
            {
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => d.Attraction.Name.Contains("Aquapark")).Where(n => n.EndDate == null);

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name", clientTicket.IdTicketInPriceList);
            }
            else
            {
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => !d.Attraction.Name.Contains("Aquapark")).Where(n => n.EndDate == null);

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name", clientTicket.IdTicketInPriceList);
            }

            return View(clientTicket);
        }




        // GET: ClientTickets
        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult IndexWristband(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var clientTicket = db.ClientTicket.Include(c => c.Wristband).Include(c => c.TicketInPriceList).Where(n => n.IdWristband == id);

            ViewBag.IdWristband = id;

            return View(clientTicket.ToList());
        }


        // GET: ClientTickets/TicketsForTicketType
        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult TicketsForTicketType(int? id)
        {
            var clientTicket = db.ClientTicket.Include(c => c.Wristband).Include(c => c.TicketInPriceList).Where(n => n.IdTicketInPriceList == id);
            return View(clientTicket.ToList());
        }


        // id - idWristband
        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult GetTicketsOnWristband(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }


            var clientTickets = db.ClientTicket.Where(c => c.IdWristband == id);

            if (clientTickets.Where(n => n.WasPaid == false).Count() > 0)
                ViewBag.AreThereTicketsToPay = true;
            else ViewBag.AreThereTicketsToPay = false;

            ViewBag.idWristband = id;
            return View(clientTickets.ToList());
        }

        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult GetTicketsToPay(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }

            var clientTickets = db.ClientTicket.Where(c => c.IdWristband == id).Where(c => c.WasPaid == false);

            decimal moneyToPay = 0;
            foreach (var ticket in clientTickets)
            {
                moneyToPay += ticket.TicketInPriceList.Price;
            }

            ViewBag.moneyToPay = moneyToPay;
            ViewBag.idWristband = id;
            return View(clientTickets.ToList());
        }

        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult PayTickets(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }

            var clientTickets = db.ClientTicket.Where(c => c.IdWristband == id).Where(c => c.WasPaid == false);

            foreach (var ticket in clientTickets)
            {
                ticket.WasPaid = true;
                db.Entry(ticket).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("GetTicketsOnWristband",new { id = id });
        }

        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult ReturnWristband(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }

            var clientTickets = db.ClientTicket.Where(c => c.IdWristband == id);

            foreach (var ticket in clientTickets)
            {
                if (!ticket.WasPaid)
                {
                    return RedirectToAction("GetTicketsToPay", new { id = id });
                }
            }
            foreach (var ticket in clientTickets)
            {
                ticket.IdWristband = null;
                db.Entry(ticket).State = EntityState.Modified;
            }
            wristband.IsUsed = false;

            db.SaveChanges();
            return RedirectToAction("Index","Wristbands");
        }


    }
}
