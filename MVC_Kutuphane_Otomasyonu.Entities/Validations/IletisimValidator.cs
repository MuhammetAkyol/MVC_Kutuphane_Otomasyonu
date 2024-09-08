using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class IletisimValidator:AbstractValidator<Iletisim>
    {
        public IletisimValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Alanı Boş Geçilemez");
            RuleFor(x => x.Email).MaximumLength(150).WithMessage("Email Alanı en fazla 150 karakter olmadılır");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen bir mail adresi formatı girin");
            ////////

            RuleFor(x => x.AdiSoyadi).NotEmpty().WithMessage("Adı Soyadı Alanı Boş Geçilemez");
            RuleFor(x => x.AdiSoyadi).MaximumLength(100).WithMessage("Adı Soyadı Alanı  en fazla 100 karakter olabilir.");
            RuleFor(x => x.Baslik).NotEmpty().WithMessage("Başlık Alanı Boş Geçilemez");
            RuleFor(x => x.Baslik).MaximumLength(200).WithMessage("Başlık Alanı  en fazla 200 karakter olabilir.");
            RuleFor(x => x.Mesaj).NotEmpty().WithMessage("Mesaj Alanı Boş Geçilemez");
            RuleFor(x => x.Mesaj).MaximumLength(500).WithMessage("Mesaj Alanı  en fazla 500 karakter olabilir.");

        }
    }
}
