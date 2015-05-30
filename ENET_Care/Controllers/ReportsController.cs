using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENET_Care.BusinessLogic;
using ENET_Care.Data;
using Microsoft.AspNet.Identity;

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
            SetupCentreList();
            return View();
        }

        private void SetupCentreList()
        {
            ICollection<DistCentre> centreList = DistCentreLogic.GetAllDistCentre();
            List<SelectListItem> centres = new List<SelectListItem>();
            foreach (DistCentre centre in centreList)
            {
                centres.Add(new SelectListItem
                {
                    Text = centre.CentreName,
                    Value = centre.CentreId.ToString()
                }
            );
            }

            ViewBag.Centres = centres;
        }

        public ActionResult DistributionCentreLossesReport()
        {
            SetupCentreList();
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