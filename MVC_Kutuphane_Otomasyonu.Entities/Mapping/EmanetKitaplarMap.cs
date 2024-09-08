using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class EmanetKitaplarMap:EntityTypeConfiguration<EmanetKitaplar>
    {
        public EmanetKitaplarMap()
        {

            this.ToTable("EmanetKitaplar");
            this.HasKey(x => x.Id);//Primary Key Olduğu
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);//Otomatik artan olduğu

            this.HasRequired(x=>x.Kitaplar).WithMany(x=>x.EmanetKitaplar).HasForeignKey(x=>x.KitapId);
            this.HasRequired(x => x.Uyeler).WithMany(x => x.EmanetKitaplar).HasForeignKey(x => x.UyeId);
        }
    }
}
