using Aquapark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Aquapark.Controllers
{
    public class ReportsController : Controller
    {
        private Entities db = new Entities();


        // GET: Reports
        public ActionResult Index(string selectedChart)
        {
            ViewBag.selectedChart = selectedChart;
            return View();
        }

        public ActionResult TicketsTypes()
        {

            var order = db.TicketInPriceList.Select(n => new
            {
                y = n.Id,
                x = n.TicketType.Type
            });

            ViewBag.data = order.ToList();

            var chart = new Chart(600, 400).AddSeries(name: "Tickets", yValues: order, yFields: "y", xValue: order, xField: "x").AddTitle("Tickets types");

            ViewBag.chart = chart;

            return View("Chart");
        }

        public ActionResult UsedWristbands()
        {

            var order = db.Wristband.Select(n => new
            {
                y = n.Id,
                x = n.IsUsed
            });

            ViewBag.data = order.ToList();

            var chart = new Chart(600, 400).AddSeries(name: "Tickets", yValues: order, yFields: "y", xValue: order, xField: "x").AddTitle("Used wristband");

            ViewBag.chart = chart;

            return View("Chart");
        }
    }
}