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
    public class PackageStatusController : Controller
    {
        private IEnumerable<PackageStandardType> medications;
        //private List<Package> packagesInStock;
        public PackageStatusController()
        {
            medications = MedicationLogic.GetAllMedications();
        }
        // GET: PackageStatus
        public ActionResult Index()
        {
            return View();
        }

        private void SetupMedicationList()
        {
            using (var context = new Entities())
            {
                ViewBag.Medications = (from d in medications
                                       select new SelectListItem { Value = d.Id.ToString(), Text = d.Description }).ToList();
            }
        }

        [HttpGet]
        public ActionResult RemoveLoss()
        {
            SetupMedicationList();
            return View();
        }


        [HttpPost]
        public ActionResult PackagesLost(List<Package> packageLost)
        {
            return View(packageLost);
        }

        [HttpPost]
        public ActionResult RemoveLoss(string barcode, int medications, string action)
        {

            if (action.Equals("Scan"))
            {
                SetupMedicationList();
                List<Package> packages = PackageStatusLogic.getPackagesInStockList();
                try
                {
                    int barcodeValue = int.Parse(barcode);
                    PackageStatusLogic.AddPackageInStockList(barcodeValue);
                }
                catch (FormatException) //If the barcode isn't a number
                {
                    ViewBag.Error = "Please enter a valid barcode";
                    return View(packages);
                }
                catch (OverflowException) //The barcode is an int32, cannot exceed 2^31 - 1
                {
                    ViewBag.Error = "Please enter a valid barcode";
                    return View(packages);
                }
                catch (InvalidOperationException) //If no package record exists
                {
                    ViewBag.Error = "The barcode you have entered doesn't exist/has not been registered";
                    return View(packages);
                }
                return View(packages);
            }
            else
            {
                List<Package> packageLost = PackageStatusLogic.UpdatePackageStatusLost(User.Identity.GetUserId(), medications);
                return View("PackagesLost", packageLost);
            }
        }
        public ActionResult Receive()
        {
            ViewBag.ReceiveUpdated = false;
            SetUpPackagesDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Receive(string packageIdBarcode, string packageIdDropdown)
        {
            string staffId = UserLogic.GetUserById(User.Identity.GetUserId()).Id;
            
            if(packageIdBarcode == "")
                PackageStatusLogic.ReceivePackage(Int32.Parse(packageIdDropdown), staffId);
            else
                PackageStatusLogic.ReceivePackage(Int32.Parse(packageIdBarcode), staffId);

            ViewBag.ReceiveUpdated = true;
            SetUpPackagesDropDown();
            return View();
        }
        public ActionResult Discard()
        {
            ViewBag.DiscardUpdated = false;
            SetUpPackagesDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Discard(string packageIdBarcode, string packageIdDropdown)
        {
            string staffId = UserLogic.GetUserById(User.Identity.GetUserId()).Id;

            if (packageIdBarcode == "")
                PackageStatusLogic.DiscardPackage(Int32.Parse(packageIdDropdown), staffId);
            else
                PackageStatusLogic.DiscardPackage(Int32.Parse(packageIdBarcode), staffId);

            ViewBag.DiscardUpdated = true;
            SetUpPackagesDropDown();
            return View();
        }
        public ActionResult Distribute()
        {
            ViewBag.DistributeUpdated = false;
            SetUpPackagesDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Distribute(string packageIdBarcode, string packageIdDropdown)
        {
            string staffId = UserLogic.GetUserById(User.Identity.GetUserId()).Id;

            if (packageIdBarcode == "")
                PackageStatusLogic.DistributePackage(Int32.Parse(packageIdDropdown), staffId);
            else
                PackageStatusLogic.DistributePackage(Int32.Parse(packageIdBarcode), staffId);
           
            ViewBag.DistributeUpdated = true;
            SetUpPackagesDropDown();
            return View();
        }
        private void SetUpPackagesDropDown()
        {
            using (var context = new Entities())
            {
                ViewBag.Packages = (from d in context.PackageStatus
                                       select new SelectListItem { Value = d.PackageID.ToString(), Text = d.PackageID.ToString()}).ToList();
            }
        }

    }
}