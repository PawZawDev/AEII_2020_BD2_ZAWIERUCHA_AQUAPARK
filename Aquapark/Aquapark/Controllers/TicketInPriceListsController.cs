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
    public class TicketInPriceListsController : Controller
    {
        private Entities db = new Entities();

        // GET: TicketInPriceLists
        public ActionResult Index()
        {
            if(System.Web.HttpContext.Current.User.IsInRole("Admin"))
            {
                return RedirectToAction("IndexHistory");
            }

            var ticketInPriceList = db.TicketInPriceList.Include(t => t.Attraction).Include(t => t.TicketType).Where(n => n.EndDate == null);
            return View(ticketInPriceList.ToList());
        }

        // GET: TicketInPriceLists/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketInPriceList ticketInPriceList = db.TicketInPriceList.Find(id);
            if (ticketInPriceList == null)
            {
                return HttpNotFound();
            }
            return View(ticketInPriceList);
        }

        // GET: TicketInPriceLists/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name");
            ViewBag.IdTicketType = new SelectList(db.TicketType, "Id", "Type");
            return View();
        }

        // POST: TicketInPriceLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,IdTicketType,Price,Entries,Duration,StartDate,EndDate,IdAttraction")] TicketInPriceList ticketInPriceList)
        {
            ticketInPriceList.StartDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.TicketInPriceList.Add(ticketInPriceList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", ticketInPriceList.IdAttraction);
            ViewBag.IdTicketType = new SelectList(db.TicketType, "Id", "Type", ticketInPriceList.IdTicketType);
            return View(ticketInPriceList);
        }

        // GET: TicketInPriceLists/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketInPriceList ticketInPriceList = db.TicketInPriceList.Find(id);
            if (ticketInPriceList == null)
            {
                return HttpNotFound();
            }
            ticketInPriceList.Name = ticketInPriceList.Name.TrimEnd();
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", ticketInPriceList.IdAttraction);
            ViewBag.IdTicketType = new SelectList(db.TicketType, "Id", "Type", ticketInPriceList.IdTicketType);
            return View(ticketInPriceList);
        }

        // POST: TicketInPriceLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperManager")]
        public ActionResult Edit([Bind(Include = "Id,Name,IdTicketType,Price,Entries,Duration,StartDate,EndDate,IdAttraction")] TicketInPriceList ticketInPriceList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketInPriceList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", ticketInPriceList.IdAttraction);
            ViewBag.IdTicketType = new SelectList(db.TicketType, "Id", "Type", ticketInPriceList.IdTicketType);
            return View(ticketInPriceList);
        }

        // GET: TicketInPriceLists/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketInPriceList ticketInPriceList = db.TicketInPriceList.Find(id);
            if (ticketInPriceList == null)
            {
                return HttpNotFound();
            }
            return View(ticketInPriceList);
        }

        // POST: TicketInPriceLists/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketInPriceList ticketInPriceList = db.TicketInPriceList.Find(id);
            db.TicketInPriceList.Remove(ticketInPriceList);
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



        // GET: TicketInPriceLists/TicketsForAttraction
        public ActionResult TicketsForAttraction(int? id)
        {
            var ticketInPriceList = db.TicketInPriceList.Include(t => t.Attraction).Include(t => t.TicketType).Where(tcketInPriceList => tcketInPriceList.IdAttraction == id);
            return View("Index",ticketInPriceList.ToList());
        }





        // GET: TicketInPriceLists/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketInPriceList ticketInPriceList = db.TicketInPriceList.Find(id);
            if (ticketInPriceList == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", ticketInPriceList.IdAttraction);
            ViewBag.IdTicketType = new SelectList(db.TicketType, "Id", "Type", ticketInPriceList.IdTicketType);
            return View(ticketInPriceList);
        }

        // POST: TicketInPriceLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([Bind(Include = "Id,Name,IdTicketType,Price,Entries,Duration,StartDate,EndDate,IdAttraction")] TicketInPriceList ticketInPriceList)
        {
            ticketInPriceList.StartDate = DateTime.Now;

            TicketInPriceList oldTicket = db.TicketInPriceList.Find(ticketInPriceList.Id);
            oldTicket.EndDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(oldTicket).State = EntityState.Modified;
                db.TicketInPriceList.Add(ticketInPriceList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAttraction = new SelectList(db.Attraction, "Id", "Name", ticketInPriceList.IdAttraction);
            ViewBag.IdTicketType = new SelectList(db.TicketType, "Id", "Type", ticketInPriceList.IdTicketType);
            return View(ticketInPriceList);
        }


        // GET: TicketInPriceLists
        public ActionResult IndexHistory(DateTime? searchDate)
        {
            ViewBag.searchDate = searchDate;

            if (searchDate != null)
            {
                var ticketInPriceList = db.TicketInPriceList.Include(t => t.Attraction).Include(t => t.TicketType).Where(n => n.EndDate >= searchDate).Where(n => n.StartDate <= searchDate);
                return View(ticketInPriceList.ToList());
            }
            else
            {
                var ticketInPriceList = db.TicketInPriceList.Include(t => t.Attraction).Include(t => t.TicketType).Where(n => n.EndDate == null);
                return View(ticketInPriceList.ToList());
            }
        }



    }
}
