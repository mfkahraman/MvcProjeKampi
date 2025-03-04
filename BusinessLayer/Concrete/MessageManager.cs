﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void AddMessageBL(Message message)
        {
            _messageDal.Insert(message);
        }

        public void DeleteMessageBL(Message message)
        {
            _messageDal.Delete(message);
        }

        public Message GetById(int id)
        {
            return _messageDal.Get(x => x.MessageId == id);
        }

        public List<Message> GetListInbox(string receiverMail)
        {
            return _messageDal.List(x=> x.ReceiverMail == receiverMail);
        }

        public List<Message> GetListSendBox(string senderMail)
        {
            return _messageDal.List(x => x.SenderMail == senderMail);
        }

        public void UpdateMessage(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
