using FluentValidation.Attributes;
using MVC_Kutuphane_Otomasyonu.Entities.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Model
{
    [Validator(typeof(UyelerVaidator))]
    public class Uyeler
    {
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres {  get; set; }
        public string EMail { get; set; }
        public string Resim { get; set; }
        public int OkuKitapSayisi { get; set; }
        public DateTime KayitTarihi {  get; set; }

        public List<EmanetKitaplar> EmanetKitaplar { get; set; }

    }
}
