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
            var headingList = headingManager.GetList();
            return View(headingList);
        }
    }
}