    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Index()
        {
            var clientTicket = db.ClientTicket.Include(c => c.Wristband).Include(c => c.TicketInPriceList);
            return View(clientTicket.ToList());
        }

        // GET: ClientTickets/Details/5
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
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => d.Attraction.Name == "Aquapark");

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name");
            }
            else
            {
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => d.Attraction.Name != "Aquapark");

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
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => d.Attraction.Name == "Aquapark");

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name", clientTicket.IdTicketInPriceList);
            }
            else
            {
                var tickets = db.TicketInPriceList.Select(n => n).Where(d => d.Attraction.Name != "Aquapark");

                ViewBag.IdTicketInPriceList = new SelectList(tickets, "Id", "Name", clientTicket.IdTicketInPriceList);
            }

            return View(clientTicket);
        }




        // GET: ClientTickets
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
        public ActionResult TicketsForTicketType(int? id)
        {
            var clientTicket = db.ClientTicket.Include(c => c.Wristband).Include(c => c.TicketInPriceList).Where(n => n.IdTicketInPriceList == id);
            return View(clientTicket.ToList());
        }

    }
}
