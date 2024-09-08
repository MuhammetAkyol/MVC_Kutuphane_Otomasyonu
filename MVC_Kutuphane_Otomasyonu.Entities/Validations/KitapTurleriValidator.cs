using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class KitapTurleriValidator:AbstractValidator<KitapTurleri>
    {
        public KitapTurleriValidator()
        {
            RuleFor(x => x.KitapTuru).NotEmpty().WithMessage("Kitap Turu Alanı Boş Geçilemez.");
            RuleFor(x => x.KitapTuru).MinimumLength(5).WithMessage("Kitap Turu Alanı en az 5 karakter olmalıdır.");
            RuleFor(x => x.KitapTuru).MaximumLength(150).WithMessage("Kitap Türü Alanı en fazla 150 karakter olabilir.");
        }
    }
}
