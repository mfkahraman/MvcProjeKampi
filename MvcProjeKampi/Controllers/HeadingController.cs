using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var values = hm.GetList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            //Categories Dropdown List
            List<SelectListItem> categories = (from x in cm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryId.ToString()
                                               }).ToList();
            ViewBag.Categories = categories;

            //Writers Dropdown List
            List<SelectListItem> writers = (from x in wm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.WriterName + " " + x.WriterSurname,
                                                   Value = x.WriterId.ToString()
                                               }).ToList();
            ViewBag.Writers = writers;

            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading model)
        {
            model.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.AddHeadingBL(model);
            return RedirectToAction("Index");
        }
    }
}