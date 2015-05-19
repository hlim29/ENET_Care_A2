using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENET_Care.BusinessLogic;

namespace ENET_Care.Controllers
{
    public class PackageStatusController : Controller
    {
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
            PackageStatusLogic.UpdatePackageStatusLost();
            return View();
        }

        [HttpPost]
        public ActionResult AddPackageInStockList(int PackageId)
        {
            //User.Identity.GetUserId(); //staff id
            PackageStatusLogic.AddPackageInStockList(PackageId);
            return View();
        }
    }
}