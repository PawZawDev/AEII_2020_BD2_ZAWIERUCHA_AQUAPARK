using Aquapark.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Aquapark.Controllers
{
    [Authorize(Roles = "Admin,Manager,SuperManager")]
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


        public ActionResult TicketsSoldByType()
        {

            var order = db.ClientTicket.Select(n => new
            {
                x = n.TicketInPriceList.Name,
            });

            var order2 = order.GroupBy(n => n.x).Select(n => new
            {
                x = n.Key,
                y = n.Count()
            });

            ViewBag.data = order.ToList();

            var chart = new Chart(600, 400).AddSeries(name: "Tickets", yValues: order2, yFields: "y", xValue: order2, xField: "x").AddTitle("Tickets sold by type");

            ViewBag.chart = chart;

            return View("Chart");
        }

        public ActionResult CreatePDF()
        {
            var report = new ViewAsPdf("FullReport");

            return report;
        }
    }
}