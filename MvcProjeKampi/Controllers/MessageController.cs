using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        Context context = new Context();

        public ActionResult Inbox()
        {
            string adminMail = (string)Session["AdminUserName"];
            var messageList = messageManager.GetListInbox(adminMail).Where(x => x.Status == true);
            return View(messageList);
        }

        public ActionResult Sendbox()
        {
            string adminMail = (string)Session["AdminUserName"];
            var messageList = messageManager.GetListSendBox(adminMail).Where(x => x.Status == true); ;
            return View(messageList);
        }

        public ActionResult GetMessageDetails(int id)
        {
            var value = messageManager.GetById(id);
            return View(value);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var value = messageManager.GetById(id);
            return View(value);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                message.SenderMail = "admin@gmail.com";
                messageManager.AddMessageBL(message);
                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
    }
}