using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();
        void AddAdmin(Admin admin);
        Admin GetById(int id);
        void DeleteAdmin(Admin admin);
        void UpdateAdmin(Admin admin);

    }
}
