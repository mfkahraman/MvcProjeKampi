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
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        Context context = new Context();

        public ActionResult Inbox()
        {
            string writerMail = (string)Session["WriterMail"];
            var messageList = messageManager.GetListInbox(writerMail);
            return View(messageList);
        }

        public ActionResult Sendbox()
        {
            string writerMail = (string)Session["WriterMail"];
            var messageList = messageManager.GetListSendBox(writerMail);
            return View(messageList);
        }

        public ActionResult GetMessageDetails(int id)
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
            string writerMail = (string)Session["WriterMail"];
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.SenderMail = writerMail;
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
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


        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
    }
}