using ENET_Care.BusinessLogic;
using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ENET_Care.Controllers
{
    [Authorize]
    public class PackageController : Controller
    {
        private PackageLogic packages;
        private IEnumerable<PackageStandardType> medications;
        public Func<string> GetUserId;
        // GET: Package

        public PackageController()
        {
            medications = MedicationLogic.GetAllMedications();
            GetUserId = () => User.Identity.GetUserId();
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
            try
            {
                string currentUserId = GetUserId();
                int currentUserCentreId = UserLogic.GetDistCentreByUserId(currentUserId).CentreId;
                int barcode = PackageLogic.RegisterPackage(DateTime.Parse(expiry), medications);
                if (barcode == -1)
                    throw new ArgumentOutOfRangeException();
                PackageStatusLogic.RegisterArrival(barcode, currentUserCentreId, currentUserId);
                ViewBag.Barcode = barcode.ToString();
            }
            catch (ArgumentOutOfRangeException) //If the expiry date is in the past
            {
                ViewBag.Error = "The package is expired";
                return Register();
            }
            catch (FormatException) //If the date field is invalid
            {
                ViewBag.Error = "Please enter a valid date";
                return Register();
            }
            return View("RegSuccess");
        }

        [HttpGet]
        public ActionResult Send()
        {
            SetupCentreList();
            return View();
        }

        [HttpPost]
        public ActionResult Send(string barcode, int centres)
        {
            string currentUserId = GetUserId();
            int currentUserCentreId = UserLogic.GetDistCentreByUserId(currentUserId).CentreId;
            PackageStatus currentPackageStatus;
            try
            {
                int barcodeValue = int.Parse(barcode);
                currentPackageStatus = PackageStatusLogic.GetPackageStatusById(barcodeValue);
                PackageStatusLogic.SendPackage(currentUserCentreId,centres,currentUserId,barcodeValue);
            }
            catch (FormatException) //If the barcode isn't a number
            {
                ViewBag.Error = "Please enter a valid barcode";
                return Send();
            } 
            catch (OverflowException) //The barcode is an int32, cannot exceed 2^31 - 1
            {
                ViewBag.Error = "Please enter a valid barcode";
                return Send();
            }
            catch (InvalidOperationException) //If no package record exists
            {
                ViewBag.Error = "The barcode you have entered doesn't exist/has not been registered";
                return Send();
            }
            return View("SendSuccess", PackageStatusLogic.GetPackageStatusEager(currentPackageStatus));
        }

        private void SetupCentreList()
        {
            ICollection<DistCentre> centreList = DistCentreLogic.GetAllDistCentre();
            List<SelectListItem> centres = new List<SelectListItem>();
            foreach (DistCentre centre in centreList){
                centres.Add(new SelectListItem
                {
                    Text = centre.CentreName,
                    Value = centre.CentreId.ToString()
                }
            );
            }

            ViewBag.Centres = centres;

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