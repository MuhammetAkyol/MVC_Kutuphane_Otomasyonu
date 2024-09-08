using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class RollerValidator:AbstractValidator<Roller>
    {
        public RollerValidator()
        {
            RuleFor(x => x.Rol).NotEmpty().WithMessage("rol Alanı Boş Geçilemez");
            RuleFor(x => x.Rol).MaximumLength(50).WithMessage("rol Alanı  en fazla 50 karakter olabilir.");
        }
    }
}
