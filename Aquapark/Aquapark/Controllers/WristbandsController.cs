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
        public ActionResult Index(int? isActiveSearch, int? isUsedSearch)
        {
            Dictionary<int, string> isActiveList = new Dictionary<int, string>();
            isActiveList.Add(1, "Close");
            isActiveList.Add(2, "All");

            var isActiveSearchList = isActiveList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            ViewBag.isActiveSearchList = new SelectList(isActiveSearchList, "Id", "Value");

            Dictionary<int, string> IsUsedList = new Dictionary<int, string>();
            IsUsedList.Add(1, "Used");
            IsUsedList.Add(2, "Not used");

            var isUsedSearchList = IsUsedList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            ViewBag.isUsedSearchList = new SelectList(isUsedSearchList, "Id", "Value");


            var wristbands = db.Wristband.Select(n => n);


            if (isActiveSearch == null) 
            {
                wristbands = wristbands.Where(n => n.IsActive == true);
            }
            else if (isActiveSearch == 1)
            {
                wristbands = wristbands.Where(n => n.IsActive == false);
            }
            if (isUsedSearch == 1)
            {
                wristbands = wristbands.Where(n => n.IsUsed == true);
            }
            else if (isUsedSearch == 2)
            {
                wristbands = wristbands.Where(n => n.IsUsed == false);
            }


            if (Request.IsAjaxRequest())
                return PartialView("IndexSearch", wristbands.ToList()); 


            return View(wristbands.ToList());
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
