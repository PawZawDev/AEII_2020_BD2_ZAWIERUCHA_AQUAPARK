﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Aquapark.Models;

namespace Aquapark.Controllers
{
    public class AttractionsController : Controller
    {
        private Entities db = new Entities();

        // GET: Attractions
        [Authorize(Roles = "Admin,Employee,Manager,SuperManager")]
        public ActionResult Index(int? isOpenSearch)
        {
            Dictionary<int, string> isOpenList = new Dictionary<int, string>();
            isOpenList.Add(1, "Open");
            isOpenList.Add(2, "Close");

            var isOpenSearchList = isOpenList.Select(n => new
            {
                Id = n.Key,
                Value = n.Value
            });
            var attractions = db.Attraction.Select(n => n);

            if(isOpenSearch == 1)
            {
                attractions = attractions.Where(n => n.IsOpen == true);
            }
            if (isOpenSearch == 2)
            {
                attractions = attractions.Where(n => n.IsOpen == false);
            }

            attractions.OrderBy(n => n.Name);


            ViewBag.IsOpenSearch = new SelectList(isOpenSearchList, "Id", "Value");

            if (Request.IsAjaxRequest())
                return PartialView("IndexSearch", attractions.ToList());

            return View(attractions.ToList());
        }

        // GET: Attractions/Details/5
        [Authorize(Roles = "Admin,Manager,SuperManager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction attraction = db.Attraction.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }


        // GET: Attractions/Create
        [Authorize(Roles = "Admin,SuperManager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attractions/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsOpen")] Attraction attraction)
        {
            if (ModelState.IsValid)
            {
                db.Attraction.Add(attraction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attraction);
        }

        // GET: Attractions/Edit/5
        [Authorize(Roles = "Admin,SuperManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction attraction = db.Attraction.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }

            attraction.Name = attraction.Name.TrimEnd();

            return View(attraction);
        }

        // POST: Attractions/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsOpen")] Attraction attraction)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(attraction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attraction);
        }

        // GET: Attractions/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction attraction = db.Attraction.Find(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }
            return View(attraction);
        }

        // POST: Attractions/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attraction attraction = db.Attraction.Find(id);
            db.Attraction.Remove(attraction);
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
