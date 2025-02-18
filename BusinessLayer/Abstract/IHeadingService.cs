using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IHeadingService
    {
        List<Heading> GetList();
        void AddHeadingBL(Heading heading);
        Heading GetById(int id);
        void DeleteHeadingBL(Heading heading);
        void UpdateHeading(Heading heading);
    }
}
