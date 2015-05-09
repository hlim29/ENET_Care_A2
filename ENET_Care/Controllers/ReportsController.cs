using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENET_Care.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DistributionCentreStockReport()
        {
            return View();
        }

        public ActionResult DistributionCentreLossesReport()
        {
            return View();
        }

        public ActionResult DoctorActivityReport()
        {
            return View();
        }

        public ActionResult ValueInTransit()
        {
            return View();
        }

        public ActionResult GlobalStock()
        {
            return View();
        }

        public ActionResult MyInformation()
        {
            return View("Pages", MyInformation());
        }
    }
}