using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ENET_Care.BusinessLogic;
using System.Diagnostics;
using ENET_Care.Data;

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

        
        public ActionResult MyInformation()
        {
            AspNetUser currentUser = UserLogic.GetUserById(User.Identity.GetUserId());
            SetUpDistCentreDropDown();
            return View(currentUser);
        }
        //[HttpPost]
        //public ActionResult MyInformation(string firstName, string lastName, string email, string distCentre)
        //{
        //    Debug.WriteLine(firstName);
        //    dynamic mymodel = new System.Dynamic.ExpandoObject();
        //    mymodel.User = UserLogic.GetUserById(User.Identity.GetUserId());
        //    mymodel.DistCentres = DistCentreLogic.GetAllDistCentre();
        //    return View(mymodel);
        //}
        private void SetUpDistCentreDropDown()
        {
            using (var context = new Entities())
            {
                ViewBag.DistCentres = (from d in context.DistCentres
                                       select new SelectListItem { Value = d.CentreId.ToString(), Text = d.CentreName }).ToList();
            }
        }
        public ActionResult Distribute()
        {
            return View();
        }
    }
}