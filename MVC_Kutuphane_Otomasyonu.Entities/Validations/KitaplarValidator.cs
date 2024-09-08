using FluentValidation;
using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Validations
{
    public class KitaplarValidator:AbstractValidator<Kitaplar>
    {
        public KitaplarValidator()
        {
            RuleFor(x => x.BarkodNo).NotEmpty().WithMessage("BarkodNo AlanıBoş geçilemez.");
            RuleFor(x => x.BarkodNo).MaximumLength(30).WithMessage("BarkodNo Alanı en fazla 30 karakter olabilir."); 
            
            RuleFor(x => x.KitapAdi).NotEmpty().WithMessage("Kitap Adı AlanıBoş geçilemez.");
            RuleFor(x => x.KitapAdi).MaximumLength(100).WithMessage("Kitap Adı Alanı en fazla 100 karakter olabilir.");
            
            RuleFor(x => x.YazarAdi).NotEmpty().WithMessage("Yazar Adı AlanıBoş geçilemez.");
            RuleFor(x => x.YazarAdi).MaximumLength(100).WithMessage("Yazar Adı Alanı en fazla 100 karakter olabilir.");  

            RuleFor(x => x.YayinEvi).NotEmpty().WithMessage("Yayın evi AlanıBoş geçilemez.");
            RuleFor(x => x.YayinEvi).MaximumLength(150).WithMessage("yayın evi Alanı en fazla 150 karakter olabilir.");
        }
    }
}
