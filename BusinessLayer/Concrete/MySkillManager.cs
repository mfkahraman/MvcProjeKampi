using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MySkillManager : IMySkillService
    {
        private readonly IMySkillDal _mySkillDal;

        public MySkillManager(IMySkillDal mySkillDal)
        {
            _mySkillDal = mySkillDal;
        }

        public void AddSkill(MySkill skill)
        {
            _mySkillDal.Insert(skill);
        }

        public void DeleteSkill(MySkill skill)
        {
            _mySkillDal.Delete(skill);
        }

        public MySkill GetById(int id)
        {
            return _mySkillDal.Get(x => x.Id == id);
        }

        public List<MySkill> GetList()
        {
            return _mySkillDal.List();
        }

        public void UpdateSkill(MySkill skill)
        {
            _mySkillDal.Update(skill);
        }
    }
}
