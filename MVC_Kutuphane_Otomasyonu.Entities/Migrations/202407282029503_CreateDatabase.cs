namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Duyurulars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(),
                        Duyuru = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmanetKitaplars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UyeId = c.Int(nullable: false),
                        KitapId = c.Int(nullable: false),
                        KitapSayisi = c.Int(nullable: false),
                        KitapAldigiTarihi = c.DateTime(nullable: false),
                        KitapIadeTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hakkimizdas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icerik = c.String(),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Iletisims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdiSoyadi = c.String(),
                        Email = c.String(),
                        Baslik = c.String(),
                        Mesaj = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapHareketleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        KıtapId = c.Int(nullable: false),
                        YapilanIslem = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapKayitHareketleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        KitapId = c.Int(nullable: false),
                        YapilanIslem = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kitaplars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarkodNo = c.String(),
                        KitapTuruId = c.Int(nullable: false),
                        YazarAdi = c.String(),
                        YayinEvi = c.String(),
                        StokAdedi = c.Int(nullable: false),
                        SayfaSayisi = c.Int(nullable: false),
                        Aciklama = c.String(),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        GuncellenmeTarihi = c.DateTime(nullable: false),
                        SilindiMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitapTurleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KitapTuru = c.String(),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KullaniciHareketleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        Aciklama = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kullanicilars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(),
                        Sifre = c.String(),
                        AdiSoyadi = c.String(),
                        Telefon = c.String(),
                        Adres = c.String(),
                        EMail = c.String(),
                        KayitTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KullaniciRolleris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        RolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rollers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rol = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uyelers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdiSoyadi = c.String(),
                        Telefon = c.String(),
                        Adres = c.String(),
                        EMail = c.String(),
                        ResimYolu = c.String(),
                        OkuKitapSayisi = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Uyelers");
            DropTable("dbo.Rollers");
            DropTable("dbo.KullaniciRolleris");
            DropTable("dbo.Kullanicilars");
            DropTable("dbo.KullaniciHareketleris");
            DropTable("dbo.KitapTurleris");
            DropTable("dbo.Kitaplars");
            DropTable("dbo.KitapKayitHareketleris");
            DropTable("dbo.KitapHareketleris");
            DropTable("dbo.Iletisims");
            DropTable("dbo.Hakkimizdas");
            DropTable("dbo.EmanetKitaplars");
            DropTable("dbo.Duyurulars");
        }
    }
}
