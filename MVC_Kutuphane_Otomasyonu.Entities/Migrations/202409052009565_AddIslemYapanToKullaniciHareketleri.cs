namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIslemYapanToKullaniciHareketleri : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Duyurular", name: "Başlık", newName: "Baslik");
            RenameColumn(table: "dbo.Duyurular", name: "Açıklama", newName: "Aciklama");
            AddColumn("dbo.KullaniciHareketleri", "islemYapan", c => c.Int(nullable: false));
            AlterColumn("dbo.Duyurular", "Baslik", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Duyurular", "Tarih", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Duyurular", "Tarih", c => c.String());
            AlterColumn("dbo.Duyurular", "Baslik", c => c.String(nullable: false, maxLength: 150, unicode: false));
            DropColumn("dbo.KullaniciHareketleri", "islemYapan");
            RenameColumn(table: "dbo.Duyurular", name: "Aciklama", newName: "Açıklama");
            RenameColumn(table: "dbo.Duyurular", name: "Baslik", newName: "Başlık");
        }
    }
}
