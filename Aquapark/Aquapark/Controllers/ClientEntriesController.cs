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
    public class ClientEntriesController : Controller
    {
        private Entities db = new Entities();

        // GET: ClientEntries
        [Authorize(Roles = "Admin,Manager,SuperManager")]
        public ActionResult Index()
        {
            var clientEntry = db.ClientEntry.Include(c => c.EntryGate).Include(c => c.Wristband);
            return View(clientEntry.ToList());
        }

        // GET: ClientEntries/Details/5
        [Authorize(Roles = "Admin,Manager,SuperManager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntry clientEntry = db.ClientEntry.Find(id);
            if (clientEntry == null)
            {
                return HttpNotFound();
            }
            return View(clientEntry);
        }

        // GET: ClientEntries/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var entryGates = db.EntryGate.Select(n => new
            {
                Id = n.Id,
                Description = n.Attraction.Name + " " + n.Id
            });

            ViewBag.IdEntryGate = new SelectList(entryGates, "Id", "Description");
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id");
            return View();
        }

        // POST: ClientEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,IsGoesInside,IdEntryGate,IdWristband")] ClientEntry clientEntry)
        {

            clientEntry.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ClientEntry.Add(clientEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var entryGates = db.EntryGate.Select(n => new
            {
                Id = n.Id,
                Description = n.Attraction.Name + " " + n.Id
            });

            ViewBag.IdEntryGate = new SelectList(entryGates, "Id", "Description", clientEntry.IdEntryGate);
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientEntry.IdWristband);
            return View(clientEntry);
        }

        // GET: ClientEntries/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntry clientEntry = db.ClientEntry.Find(id);
            if (clientEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEntryGate = new SelectList(db.EntryGate, "Id", "Id", clientEntry.IdEntryGate);
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientEntry.IdWristband);
            return View(clientEntry);
        }

        // POST: ClientEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,IsGoesInside,IdEntryGate,IdWristband")] ClientEntry clientEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEntryGate = new SelectList(db.EntryGate, "Id", "Id", clientEntry.IdEntryGate);
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientEntry.IdWristband);
            return View(clientEntry);
        }

        // GET: ClientEntries/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEntry clientEntry = db.ClientEntry.Find(id);
            if (clientEntry == null)
            {
                return HttpNotFound();
            }
            return View(clientEntry);
        }

        // POST: ClientEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientEntry clientEntry = db.ClientEntry.Find(id);
            db.ClientEntry.Remove(clientEntry);
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



        // GET: ClientEntries/Create
        [Authorize(Roles = "Admin,SuperManager")]
        public ActionResult Simulation()
        {
            var entryGates = db.EntryGate.Select(n => new
            {
                Id = n.Id,
                Description = n.Attraction.Name + " " + n.Id,
                IsActive = n.IsActive
            });

            ViewBag.NoTicketsForAttraction = false;

            ViewBag.IdEntryGate = new SelectList(entryGates.Where(n => n.IsActive == true), "Id", "Description");           
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id");
            return View();
        }

        // POST: ClientEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Simulation([Bind(Include = "Id,Date,IsGoesInside,IdEntryGate,IdWristband")] ClientEntry clientEntry)
        {
            bool activeTicket = false;

            var entryGate = db.EntryGate.Find(clientEntry.IdEntryGate);


            var clientTickets = db.ClientTicket.Select(n => n).
                Where(n => n.IdWristband == clientEntry.IdWristband).
                Where(n => n.TicketInPriceList.Attraction.Id == entryGate.IdAttraction);

            foreach (ClientTicket ticket in clientTickets)
            {
                if (ticket.ExpirationDate < DateTime.Now)
                {
                    ticket.IsActive = false;
                    db.Entry(ticket).State = EntityState.Modified;
                    continue;
                }
                else if (ticket.EntriesLeft != null)
                {
                    if (ticket.EntriesLeft <= 0)
                    {
                        ticket.IsActive = false;
                        db.Entry(ticket).State = EntityState.Modified;
                        continue;
                    }

                    ticket.EntriesLeft -= 1;
                    db.Entry(ticket).State = EntityState.Modified;
                }
                activeTicket = true;
                break;
            }

            clientEntry.Date = DateTime.Now;


            if (ModelState.IsValid && activeTicket) 
            {
                db.ClientEntry.Add(clientEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var entryGates = db.EntryGate.Select(n => new
            {
                Id = n.Id,
                Description = n.Attraction.Name + " " + n.Id,
                 IsActive = n.IsActive
            });

            ViewBag.NoTicketsForAttraction = true;

            ViewBag.IdEntryGate = new SelectList(entryGates.Where(n => n.IsActive == true), "Id", "Description", clientEntry.IdEntryGate);            
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientEntry.IdWristband);
            return View(clientEntry);
        }

    }
}
