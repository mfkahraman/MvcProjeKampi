using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        ContentManager contentManager = new ContentManager(new EfContentDal());

        public PartialViewResult Index(int id = 1)
        {
            var contentList = contentManager.GetListByHeadingId(id);
            return PartialView(contentList);
        }

        public ActionResult Headings()
        {
            var headingList = headingManager.GetList().Where(x => x.Status == true);
            return View(headingList);
        }

        public ActionResult Calendar(int month = 0, int year = 0)
        {
            if (month == 0)
            {
                month = DateTime.Now.Month;
            }

            if (year == 0)
            {
                year = DateTime.Now.Year;
            }

            // Önceki ve sonraki aylar
            var prevMonth = new DateTime(year, month, 1).AddMonths(-1);
            var nextMonth = new DateTime(year, month, 1).AddMonths(1);

            // Takvimi güncel ay ve yıl ile al
            var headingList = headingManager.GetList().Where(x => x.Status == true && x.HeadingDate.Month == month && x.HeadingDate.Year == year);

            ViewBag.CurrentMonth = month;
            ViewBag.CurrentYear = year;
            ViewBag.PrevMonth = prevMonth;
            ViewBag.NextMonth = nextMonth;

            return View(headingList);
        }



    }
}