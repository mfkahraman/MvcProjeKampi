﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator validationRules = new WriterValidator();
        public ActionResult Index()
        {
            var values = wm.GetList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer writer)
        {
            ValidationResult validationResult = validationRules.Validate(writer);
            if (validationResult != null)
            {
                wm.WriterAddBL(writer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writerValue = wm.GetByIdBL(id);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer writer)
        {
            ValidationResult validationResult = validationRules.Validate(writer);
            if (validationResult != null)
            {
                wm.WriterUpdateBL(writer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
    }
}