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
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        Context context = new Context();

        public ActionResult MyContent()
        {
            string writerMail = (string)Session["WriterMail"];
            int writerId = context.Writers.Where(x => x.WriterMail == writerMail).Select(y => y.WriterId).FirstOrDefault();
            var contents = cm.GetListByWriter(writerId);
            return View(contents);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.HeadingId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content content)
        {
            string writerMail = (string)Session["WriterMail"];

            if (!string.IsNullOrEmpty(writerMail))
            {
                int writerId = context.Writers.Where(x => x.WriterMail == writerMail).Select(y => y.WriterId).FirstOrDefault();
                content.WriterId = writerId;
                content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                content.Status = true;
                cm.AddContent(content);
                return RedirectToAction("MyContent");
            }

            return View(AddContent(content.HeadingId));

        }


    }
}