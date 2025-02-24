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
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        // GET: WriterPanel
        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading()
        {
            var values = headingManager.GetListByWriter(4);
            return View();
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.Status = true;
            heading.WriterId = 4;
            headingManager.AddHeadingBL(heading);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult UpdateHeading(int id)
        {
            ViewBag.Categories = GetCategories();
            var value = headingManager.GetById(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateHeading(Heading model)
        {
            headingManager.UpdateHeading(model);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var value = headingManager.GetById(id);
            value.Status = false;
            headingManager.DeleteHeadingBL(value);
            return RedirectToAction("MyHead'ng");
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
    }
}