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
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());
        // GET: About
        public ActionResult Index()
        {
            var values = aboutManager.GetList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            about.Status = true;
            aboutManager.AddAbout(about);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }

        public ActionResult Activate(int id)
        {
            var item = aboutManager.GetById(id);
            item.Status = true;
            aboutManager.UpdateAbout(item);
            return RedirectToAction("Index");
        }

        public ActionResult MakePassive(int id)
        {
            var item = aboutManager.GetById(id);
            item.Status = false;
            aboutManager.UpdateAbout(item);
            return RedirectToAction("Index");
        }
    }
}