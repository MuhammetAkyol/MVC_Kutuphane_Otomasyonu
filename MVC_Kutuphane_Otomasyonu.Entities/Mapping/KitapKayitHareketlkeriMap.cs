using MVC_Kutuphane_Otomasyonu.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Kutuphane_Otomasyonu.Entities.Mapping
{
    public class KitapKayitHareketlkeriMap:EntityTypeConfiguration<KitapKayitHareketleri>
    {
        public KitapKayitHareketlkeriMap()
        {
            this.ToTable("KitapKayitHareketleri");
            this.HasKey(x => x.Id);//Primary Key Olduğu
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);//Otomatik artan olduğu
            this.Property(x => x.YapilanIslem).IsRequired().HasMaxLength(150);
            this.Property(x => x.Aciklama).HasMaxLength(5000);
            
            this.HasRequired(x=>x.Kitaplar).WithMany(x=>x.KitapKayitHareketleri).HasForeignKey(x=>x.KitapId);
        }
    }
}
