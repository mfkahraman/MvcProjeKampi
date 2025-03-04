using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMySkillService
    {
        List<MySkill> GetList();
        void AddSkill(MySkill skill);
        void DeleteSkill(MySkill skill);
        void UpdateSkill(MySkill skill);
        MySkill GetById(int id);
    }
}
