using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
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
            Context context = new Context();
            string writerMail = (string)Session["WriterMail"];
            int writerId = context.Writers.Where(x=>x.WriterMail == writerMail).Select(y=>y.WriterId).FirstOrDefault();
            var values = headingManager.GetListByWriter(writerId);
            return View(values);
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
            Context context = new Context();
            string writerMail = (string)Session["WriterMail"];
            int writerId = context.Writers.Where(x => x.WriterMail == writerMail).Select(y => y.WriterId).FirstOrDefault();
            heading.WriterId = writerId;
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.Status = true;
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
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var value = headingManager.GetById(id);
            value.Status = false;
            headingManager.DeleteHeadingBL(value);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading()
        {
            var values = headingManager.GetList();
            return View(values);
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