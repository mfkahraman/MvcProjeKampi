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
    [Authorize(Roles = "A")]
    public class AdminSkillsController : Controller
    {
        MySkillManager skillManager = new MySkillManager(new EfMySkillDal());

        public ActionResult Index()
        {
            var values = skillManager.GetList();
            return View(values);
        }

        public ActionResult AddSkill()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSkill(MySkill skill)
        {
            skillManager.AddSkill(skill);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateSkill(int id)
        {
            var skillValue = skillManager.GetById(id);
            return View(skillValue);
        }

        [HttpPost]
        public ActionResult UpdateSkill(MySkill skill)
        {
            skillManager.UpdateSkill(skill);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSkill(int id)
        {
            var skillValue = skillManager.GetById(id);
            skillManager.DeleteSkill(skillValue);
            return RedirectToAction("Index");
        }

    }
}