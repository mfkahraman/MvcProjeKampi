using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator validationRules = new WriterValidator();

        public ActionResult Index()
        {
            var values = wm.GetList().Where(x => x.Status == true);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer model, HttpPostedFileBase imageFile)
        {
            ValidationResult validationResult = validationRules.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                TempData["Error"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return View(model);
            }

            if (imageFile != null)
            {
                string imagePath = SaveImage(imageFile, "writers/");
                if (!string.IsNullOrEmpty(imagePath))
                {
                    model.WriterImage = imagePath;
                }
            }

            model.Status = true; // Yeni yazar varsayılan olarak aktif
            wm.WriterAddBL(model);
            TempData["Success"] = "Yazar başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writer = wm.GetByIdBL(id);
            if (writer == null)
            {
                TempData["Error"] = "Güncellenmek istenen yazar bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(writer);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer model, HttpPostedFileBase imageFile)
        {
            ValidationResult validationResult = validationRules.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                TempData["Error"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return View(model);
            }

            if (imageFile != null)
            {
                DeleteOldImage(model.WriterImage);
                string imagePath = SaveImage(imageFile, "writers/");
                if (!string.IsNullOrEmpty(imagePath))
                {
                    model.WriterImage = imagePath;
                }
            }

            model.Status = true; // Yazar güncellendiğinde varsayılan olarak aktif
            wm.WriterUpdateBL(model);
            TempData["Success"] = "Yazar bilgileri başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        public ActionResult DeactivateWriter(int id)
        {
            var writer = wm.GetByIdBL(id);
            if (writer == null)
            {
                TempData["Error"] = "Silinmek istenen yazar bulunamadı.";
                return RedirectToAction("Index");
            }

            writer.Status = false;
            wm.WriterUpdateBL(writer);
            TempData["Success"] = "Yazar başarıyla pasif hale getirildi.";
            return RedirectToAction("Index");
        }


        private string SaveImage(HttpPostedFileBase file, string folderPath)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".gif")
            {
                return null;
            }

            string fileName = Guid.NewGuid() + fileExtension;
            string rootPath = "/wwwroot/images/";
            string fullPath = rootPath + folderPath;
            string serverFolderPath = Server.MapPath(fullPath);
            if (!Directory.Exists(serverFolderPath))
            {
                Directory.CreateDirectory(serverFolderPath);
            }
            string path = Path.Combine(serverFolderPath, fileName);
            file.SaveAs(path);

            return fullPath + fileName;
        }

        private bool DeleteOldImage(string imageUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var oldImagePath = Server.MapPath(imageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }


    }
}