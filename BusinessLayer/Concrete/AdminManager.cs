﻿using BusinessLayer.Abstract;
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
    public class AdminManager : IAdminService
    {
        private readonly IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AddAdmin(Admin admin)
        {
            admin.AdminPassword = PasswordHelper.HashPassword(admin.AdminPassword);
            _adminDal.Insert(admin);
        }

        public void DeleteAdmin(Admin admin)
        {
            _adminDal.Delete(admin);
        }

        public Admin GetById(int id)
        {
            return _adminDal.Get(x => x.AdminID == id);
        }

        public List<Admin> GetList()
        {
            return _adminDal.List();
        }

        public void UpdateAdmin(Admin admin)
        {
            admin.AdminPassword = PasswordHelper.HashPassword(admin.AdminPassword);
            _adminDal.Update(admin);
        }
    }
}
