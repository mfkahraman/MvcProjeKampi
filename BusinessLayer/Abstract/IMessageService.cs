using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetListInbox(string receiverMail);
        List<Message> GetListSendBox(string senderMail);
        void AddMessageBL(Message message);
        Message GetById(int id);
        void DeleteMessageBL(Message message);
        void UpdateMessage(Message message);
    }
}
