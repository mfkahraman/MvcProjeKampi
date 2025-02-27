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
            ViewBag.Categories = GetCategories();
            ViewBag.Writers = GetWriters();

            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading model)
        {
            model.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.AddHeadingBL(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateHeading(int id)
        {
            ViewBag.Categories = GetCategories();
            ViewBag.Writers = GetWriters();
            var value = hm.GetById(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateHeading(Heading model)
        {
            hm.UpdateHeading(model);
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetWriters()
        {

            //Writers Dropdown List
            return (from x in wm.GetList()
                    select new SelectListItem
                    {
                        Text = x.WriterName + " " + x.WriterSurname,
                        Value = x.WriterId.ToString()
                    }).ToList();
        }

        private List<SelectListItem> GetCategories()
        {
            return (from x in cm.GetList()
                    select new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryId.ToString()
                    }).ToList();
        }

        public ActionResult DeleteHeading(int id)
        {
            var value = hm.GetById(id);
            value.Status = false;
            hm.DeleteHeadingBL(value);
            return RedirectToAction("Index");
        }

        public ActionResult HeadingReport ()
        {
            var headingValues = hm.GetList();
            return View(headingValues);
        }
    }
}