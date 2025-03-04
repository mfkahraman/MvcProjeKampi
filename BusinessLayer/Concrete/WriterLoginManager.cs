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
    public class WriterLoginManager : IWriterLoginService
    {
        IWriterDal _writerDal;

        public WriterLoginManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetWriter(string mailAdress, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            return _writerDal.Get(x => x.WriterMail == mailAdress && x.WriterPassword == hashedPassword);
        }
    }
}
