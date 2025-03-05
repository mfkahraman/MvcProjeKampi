using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [Authorize(Roles = "A")]
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator validationRules = new ContactValidator();
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        public ActionResult Index()
        {
            var values = cm.GetList().Where(x=> x.Status == true);
            return View(values);
        }

        public ActionResult GetContactDetails(int id)
        {
            var value = cm.GetById(id);
            return View(value);
        }

        public PartialViewResult MessageListMenu()
        {
            string adminMail = (string)Session["AdminUserName"];
            ViewBag.ReadContactCount = cm.GetList().Where(x => x.Status == true && x.IsRead == true).Count();
            ViewBag.UneadContactCount = cm.GetList().Where(x => x.Status == true && x.IsRead == false).Count();
            ViewBag.InboxReadCount = messageManager.GetListInbox(adminMail).Where(x => x.Status == true && x.IsRead == true).Count();
            ViewBag.InboxUnreadCount = messageManager.GetListInbox(adminMail).Where(x => x.Status == true && x.IsRead == false).Count();
            ViewBag.SendboxReadCount = messageManager.GetListSendBox(adminMail).Where(x => x.Status == true && x.IsRead == true).Count();
            ViewBag.SendboxUnreadCount = messageManager.GetListSendBox(adminMail).Where(x => x.Status == true && x.IsRead == false).Count();
            return PartialView();
        }
    }
}