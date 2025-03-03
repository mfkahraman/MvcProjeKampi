using BusinessLayer.Abstract;
using BusinessLayer.Utilities;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminLoginManager : IAdminLoginService
    {
        private readonly IAdminDal _adminDal;

        public AdminLoginManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public Admin GetAdmin(string adminUserName, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            return _adminDal.Get(x => x.AdminUserName == adminUserName && x.AdminPassword == hashedPassword);
        }
    }
}
