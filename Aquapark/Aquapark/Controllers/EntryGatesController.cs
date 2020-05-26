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
        public ActionResult Index(string attractionNameSearch,int? isActiveSearch)
        {
            Dictionary<int, string> isActiveList = new Dictionary<int, string>();
            isActiveList.Add(1, "Active");
            isActiveList.Add(2, "Close");

            var isActiveSearchList = isActiveList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            ViewBag.IsActiveSearch = new SelectList(isActiveSearchList, "Id", "Value");


            var entryGate = db.EntryGate.Include(e => e.Attraction);

            if (attractionNameSearch != null)
            {
                entryGate = entryGate.Where(n => n.Attraction.Name.Contains(attractionNameSearch));
            }
            if (isActiveSearch == 1)
            {
                entryGate = entryGate.Where(n => n.IsActive == true);
            }
            if (isActiveSearch == 2)
            {
                entryGate = entryGate.Where(n => n.IsActive == false);
            }

            if (Request.IsAjaxRequest())
                return PartialView("IndexSearch", entryGate.ToList());

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
