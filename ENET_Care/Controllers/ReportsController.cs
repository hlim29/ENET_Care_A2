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

        [HttpPost]
        public ActionResult DistributionCentreStockReport(int centres)
        {
            SetupCentreList();
            List<Report_MedicationInStock> medications = MedicationLogic.GetMedicationsInStockByDistCentre(centres);
            ViewBag.GrandTotal = MedicationLogic.GetTotalAmountMedication(medications);
            return View(medications);
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
            return View(DistCentreLogic.GetDistributionCentreLosses());
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
            List<Report_MedicationInStock> medications= MedicationLogic.GetMedicationsInStock();
            ViewBag.GrandTotal = MedicationLogic.GetTotalAmountMedication(medications);
            return View(medications);
        }

        public ActionResult MyInformation()
        {
            return View("Pages", MyInformation());
        }
    }
}