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
    public class WristbandsController : Controller
    {
        private Entities db = new Entities();

        // GET: Wristbands
        public ActionResult Index()
        {
            return View(db.Wristband.ToList());
        }

        // GET: Wristbands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }
            return View(wristband);
        }

        // GET: Wristbands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wristbands/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsActive,IsUsed")] Wristband wristband)
        {
            if (ModelState.IsValid)
            {
                db.Wristband.Add(wristband);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wristband);
        }

        // GET: Wristbands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }
            return View(wristband);
        }

        // POST: Wristbands/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,IsUsed")] Wristband wristband)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wristband).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wristband);
        }

        // GET: Wristbands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wristband wristband = db.Wristband.Find(id);
            if (wristband == null)
            {
                return HttpNotFound();
            }
            return View(wristband);
        }

        // POST: Wristbands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wristband wristband = db.Wristband.Find(id);
            db.Wristband.Remove(wristband);
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
