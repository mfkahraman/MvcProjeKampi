using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{

    public class AdminCategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        CategoryValidator categoryValidator = new CategoryValidator();

        public ActionResult Index(int startPage = 1)
        {
            var values = cm.GetList().Where(x=>x.CategoryStatus == true).ToPagedList(startPage, 5); ;
            return View(values);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {   
            model.CategoryStatus = true;
            ValidationResult results = categoryValidator.Validate(model);
            if (results.IsValid)
            {
                cm.CategoryAddBL(model);
                return RedirectToAction("Index");
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

        public ActionResult DeleteCategory(int id)
        {
            var itemToDelete = cm.GetById(id);
            itemToDelete.CategoryStatus = false;
            cm.CategoryUpdate(itemToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var itemToUpdate = cm.GetById(id);
            return View(itemToUpdate);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category model)
        {
            cm.CategoryUpdate(model);
            return RedirectToAction("Index");
        }
    }
}