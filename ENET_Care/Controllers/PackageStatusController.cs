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
        private List<Package> packagesInStock = new List<Package>();

        // GET: PackageStatus
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RemoveLoss()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePackageStatusLost()
        {
            PackageStatusLogic.UpdatePackageStatusLost(User.Identity.GetUserId(),packagesInStock);
            return View();
        }

        [HttpPost]
        public ActionResult AddPackageInStockList(int PackageId)
        {
            PackageStatusLogic.AddPackageInStockList(PackageId,packagesInStock);
            return View();
        }


    }
}