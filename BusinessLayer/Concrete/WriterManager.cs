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
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetByIdBL(int id)
        {
           return _writerDal.Get(x => x.WriterId == id);
        }

        public List<Writer> GetList()
        {
            return _writerDal.List();
        }

        public void WriterAddBL(Writer writer)
        {
            writer.WriterPassword = PasswordHelper.HashPassword(writer.WriterPassword);
            _writerDal.Insert(writer);
        }

        public void WriterDeleteBL(Writer writer)
        {
            _writerDal.Delete(writer);
        }

        public void WriterUpdateBL(Writer writer)
        {
            writer.WriterPassword = PasswordHelper.HashPassword(writer.WriterPassword);
            _writerDal.Update(writer);
        }
    }
}
