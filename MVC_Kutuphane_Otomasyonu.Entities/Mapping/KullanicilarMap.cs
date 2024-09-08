﻿using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class KullanicilarMap:EntityTypeConfiguration<Kullanicilar>
    {
        public KullanicilarMap()
        {
            this.ToTable("Kullanicilar");
            this.HasKey(x => x.Id);//Primary Key Olduğu
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);//Otomatik artan olduğu 
            this.Property(x => x.KullaniciAdi).IsRequired().HasMaxLength(30);
            this.Property(x => x.Sifre).IsRequired().HasMaxLength(15);
            this.Property(x => x.AdiSoyadi).IsRequired().HasMaxLength(100);
            this.Property(x => x.Telefon).IsRequired().HasMaxLength(150);
            this.Property(x => x.Adres).IsRequired().HasMaxLength(20);
            this.Property(x => x.EMail).IsRequired().HasMaxLength(500);

            
        }
    }
}
