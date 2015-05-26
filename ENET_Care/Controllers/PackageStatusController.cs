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
        //private List<Package> packagesInStock;

        // GET: PackageStatus
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RemoveLoss()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdatePackageStatusLost()
        {
            List<Package> packageLost = PackageStatusLogic.UpdatePackageStatusLost(User.Identity.GetUserId());
            return View();
        }

        [HttpPost]
        public ActionResult RemoveLoss(string barcode)
        {
            try
            {
                int barcodeValue = int.Parse(barcode);
                PackageStatusLogic.AddPackageInStockList(barcodeValue);
            }
            catch (FormatException) //If the barcode isn't a number
            {
                ViewBag.Error = "Please enter a valid barcode";
                return View(PackageStatusLogic.packagesInStock);
            }
            catch (OverflowException) //The barcode is an int32, cannot exceed 2^31 - 1
            {
                ViewBag.Error = "Please enter a valid barcode";
                return View(PackageStatusLogic.packagesInStock);
            }
            catch (InvalidOperationException) //If no package record exists
            {
                ViewBag.Error = "The barcode you have entered doesn't exist/has not been registered";
                return View(PackageStatusLogic.packagesInStock);
            }
            return View(PackageStatusLogic.packagesInStock);
        }
        public ActionResult Receive()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Receive(string packageId)
        {
            string staffId = UserLogic.GetUserById(User.Identity.GetUserId()).Id;
            PackageStatusLogic.ReceivePackage(Int32.Parse(packageId), staffId);
            return View();
        }
        public ActionResult Discard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Discard(string packageId)
        {
            string staffId = UserLogic.GetUserById(User.Identity.GetUserId()).Id;
            PackageStatusLogic.DiscardPackage(Int32.Parse(packageId), staffId);
            return View();
        }
        public ActionResult Distribute()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Distribute(string packageId)
        {
            string staffId = UserLogic.GetUserById(User.Identity.GetUserId()).Id;
            PackageStatusLogic.DistributePackage(Int32.Parse(packageId), staffId);
            return View();
        }

    }
}