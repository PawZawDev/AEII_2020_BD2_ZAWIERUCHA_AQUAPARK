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
    public class EntryGatesController : Controller
    {
        private Entities db = new Entities();

        // GET: EntryGates
        public ActionResult Index()
        {
            var entryGate = db.EntryGate.Include(e => e.Attraction);
            return View(entryGate.ToList());
        }

        // GET: EntryGates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryGate entryGate = db.EntryGate.Find(id);
            if (entryGate == null)
            {
                return HttpNotFound();
            }
            return View(entryGate);
        }

        // GET: EntryGates/Create
        public ActionResult Create()
        {
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name");
            return View();
        }

        // POST: EntryGates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsActive,IdAttraction")] EntryGate entryGate)
        {
            if (ModelState.IsValid)
            {
                db.EntryGate.Add(entryGate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", entryGate.IdAttraction);
            return View(entryGate);
        }

        // GET: EntryGates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryGate entryGate = db.EntryGate.Find(id);
            if (entryGate == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", entryGate.IdAttraction);
            return View(entryGate);
        }

        // POST: EntryGates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,IdAttraction")] EntryGate entryGate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entryGate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", entryGate.IdAttraction);
            return View(entryGate);
        }

        // GET: EntryGates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryGate entryGate = db.EntryGate.Find(id);
            if (entryGate == null)
            {
                return HttpNotFound();
            }
            return View(entryGate);
        }

        // POST: EntryGates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryGate entryGate = db.EntryGate.Find(id);
            db.EntryGate.Remove(entryGate);
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
