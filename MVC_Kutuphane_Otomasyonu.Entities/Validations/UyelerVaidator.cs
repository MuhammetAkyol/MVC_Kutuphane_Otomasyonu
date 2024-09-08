using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class UyelerVaidator:AbstractValidator<Uyeler>
    {
        public UyelerVaidator()
        {
            RuleFor(x => x.AdiSoyadi).NotEmpty().WithMessage("ad soyad Alanı Boş Geçilemez");
            RuleFor(x => x.AdiSoyadi).MaximumLength(100).WithMessage("ad soyad Alanı  en fazla 100 karakter olabilir.");

            RuleFor(x => x.Telefon).NotEmpty().WithMessage("telefon Alanı Boş Geçilemez");
            RuleFor(x => x.Telefon).MaximumLength(20).WithMessage("telefon Alanı  en fazla 20 karakter olabilir.");

            RuleFor(x => x.EMail).NotEmpty().WithMessage("email Alanı Boş Geçilemez");
            RuleFor(x => x.EMail).MaximumLength(150).WithMessage("email Alanı  en fazla 150 karakter olabilir.");
            RuleFor(x => x.EMail).EmailAddress().WithMessage("lütfen bir mail formatu Şeklinde Giriniz.");

            RuleFor(x => x.Adres).NotEmpty().WithMessage("adres Alanı Boş Geçilemez");
            RuleFor(x => x.Adres).MaximumLength(500).WithMessage("adres Alanı  en fazla 500 karakter olabilir.");
        }
    }
}
