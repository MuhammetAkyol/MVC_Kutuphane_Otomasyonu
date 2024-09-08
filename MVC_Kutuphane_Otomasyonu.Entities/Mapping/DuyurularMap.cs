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
    public class DuyurularMap:EntityTypeConfiguration<Duyurular>
    {
        private DatabaseGeneratedOption? databaseGeneratedOption;
             
             
        public DuyurularMap()
        {
            this.ToTable("Duyurular");
            this.HasKey(x => x.Id);//Primary Key Olduğu
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);//Otomatik artan olduğu
            this.Property(x => x.Baslik).IsRequired().HasMaxLength(150);
            this.Property(x => x.Duyuru).IsRequired().HasMaxLength(500);
            this.Property(x => x.Aciklama).HasMaxLength(5000);

        }

    }
}
