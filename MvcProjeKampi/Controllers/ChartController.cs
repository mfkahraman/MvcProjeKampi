using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ChartController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HeadingChart()
        {
            var headingList = headingManager.GetList();
            return Json(headingList, JsonRequestBehavior.AllowGet);
        }
    }
}