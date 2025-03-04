﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetList(string p = null);
        List<Content> GetListByHeadingId(int id);
        List<Content> GetListByWriter(int id);
        void AddContent(Content content);
        Content GetById(int id);
        void DeleteContent(Content content);
        void UpdateContent(Content content);
    }
}
