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
        public ActionResult Index()
        {
            var clientEntry = db.ClientEntry.Include(c => c.EntryGate).Include(c => c.Wristband);
            return View(clientEntry.ToList());
        }

        // GET: ClientEntries/Details/5
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
        public ActionResult Create()
        {
            ViewBag.IdEntryGate = new SelectList(db.EntryGate, "Id", "Id");
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id");
            return View();
        }

        // POST: ClientEntries/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,IsGoesInside,IdEntryGate,IdWristband")] ClientEntry clientEntry)
        {
            if (ModelState.IsValid)
            {
                db.ClientEntry.Add(clientEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEntryGate = new SelectList(db.EntryGate, "Id", "Id", clientEntry.IdEntryGate);
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientEntry.IdWristband);
            return View(clientEntry);
        }

        // GET: ClientEntries/Create
        public ActionResult Symulation()
        {
            ViewBag.IdEntryGate = new SelectList(db.EntryGate, "Id", "Id");
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Symulation([Bind(Include = "Id,ReturnDate,IsGoesInside,IdEntryGate,IdWristband")] ClientEntry clientEntry)
        {

            if (ModelState.IsValid)
            {
                db.ClientEntry.Add(clientEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            clientEntry.ReturnDate = DateTime.Now;
            ViewBag.IdEntryGate = new SelectList(db.EntryGate, "Id", "Id", clientEntry.IdEntryGate);
            ViewBag.IdWristband = new SelectList(db.Wristband, "Id", "Id", clientEntry.IdWristband);
            return View(clientEntry);
        }

        // GET: ClientEntries/Edit/5
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
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
}
