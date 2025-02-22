using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı Adresi Boş Geçilemez");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu Boş Geçilemez");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesaj Boş Geçilemez");
            RuleFor(x=> x.ReceiverMail).EmailAddress().WithMessage("Geçerli Bir Mail Adresi Giriniz");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen 100 karakterden fazla değer girişi yapmayınız");
            RuleFor(x => x.MessageContent).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız");
            RuleFor(x => x.MessageContent).MaximumLength(500).WithMessage("Lütfen 500 karakterden fazla değer girişi yapmayınız");
        }
    }
}
