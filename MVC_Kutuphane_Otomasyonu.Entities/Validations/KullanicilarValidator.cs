using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class KullanicilarValidator:AbstractValidator<Kullanicilar>
    {
        public KullanicilarValidator()
        {
            RuleFor(x => x.EMail).NotEmpty().WithMessage("Email Alanı Boş Geçilemez");
            RuleFor(x => x.EMail).MaximumLength(150).WithMessage("Email Alanı en fazla 150 karakter olmadılır");
            RuleFor(x => x.EMail).EmailAddress().WithMessage("Lütfen bir mail adresi formatı girin");
            ////////

            RuleFor(x => x.AdiSoyadi).NotEmpty().WithMessage("Adı Soyadı Alanı Boş Geçilemez");
            RuleFor(x => x.AdiSoyadi).MaximumLength(100).WithMessage("Adı Soyadı Alanı  en fazla 100 karakter olabilir.");

            RuleFor(x => x.KullaniciAdi).NotEmpty().WithMessage("Kullanıcı Adı Alanı Boş Geçilemez");
            RuleFor(x => x.KullaniciAdi).MaximumLength(30).WithMessage("Kullanıcı Adı Alanı  en fazla 30 karakter olabilir.");

            RuleFor(x => x.Sifre).NotEmpty().WithMessage("Sifre Alanı Boş Geçilemez");
            RuleFor(x => x.Sifre).MaximumLength(15).WithMessage("Sifre Alanı  en fazla 15 karakter olabilir.");

            RuleFor(x => x.Telefon).NotEmpty().WithMessage("Telefon Alanı Boş Geçilemez");
            RuleFor(x => x.Telefon).MaximumLength(20).WithMessage("Telefon Alanı  en fazla 20 karakter olabilir.");

            RuleFor(x => x.Adres).NotEmpty().WithMessage("adres Alanı Boş Geçilemez");
            RuleFor(x => x.Adres).MaximumLength(500).WithMessage("adres Alanı  en fazla 500 karakter olabilir.");
        }
    }
}
