using ENET_Care.BusinessLogic;
using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENET_Care.Controllers
{
    public class PackageController : Controller
    {
        private PackageLogic packages;
        private IEnumerable<PackageStandardType> medications;
        // GET: Package

        public PackageController()
        {
            medications = MedicationLogic.GetAllMedications();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            SetupMedicationList();
            return View();
        }

        [HttpPost]
        public ActionResult Register(int medications, string expiry)
        {
            int barcode = PackageLogic.RegisterPackage(DateTime.Parse(expiry),medications);
            ViewBag.Barcode = barcode.ToString();
            
            return View("RegSuccess"); 
        }

        private void SetupMedicationList()
        {
            using (var context = new Entities())
            {
                ViewBag.Medications = (from d in medications
                                       select new SelectListItem { Value = d.Id.ToString(), Text = d.Description }).ToList();
            }
        }
    }
}