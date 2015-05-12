using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENET_Care.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Receive()
        {
            return View();
        }

        public ActionResult Send()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Stocktaking()
        {
            return View();
        }

        public ActionResult Discard()
        {
            return View();
        }

        public ActionResult RemoveLoss()
        {
            return View();
        }

        public ActionResult MyInformation()
        {
            return View();
        }

        public ActionResult Distribute()
        {
            return View();
        }
    }
}