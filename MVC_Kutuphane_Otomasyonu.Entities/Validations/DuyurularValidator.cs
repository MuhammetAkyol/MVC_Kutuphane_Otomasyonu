using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class DuyurularValidator:AbstractValidator<Duyurular>
    {
        public DuyurularValidator()
        {
            RuleFor(x => x.Baslik).NotEmpty().WithMessage("Başlıık Alanı Boş Geçilemez.");
            RuleFor(x => x.Duyuru).NotEmpty().WithMessage("Duyuru Alanı Boş Geçilemez."); 
            RuleFor(x => x.Tarih).NotEmpty().WithMessage("Tarih Alanı Boş Geçilemez."); 
            RuleFor(x => x.Baslik).Length(5, 150).WithMessage("Başlık Alanı 5 ile 150 arası karakter olmalıdır.");
            RuleFor(x => x.Duyuru).MaximumLength(500).WithMessage("Duyuru Alanı en fazla 500 karakter olmalıdır.");       
        }
    }
}
