using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}