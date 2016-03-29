namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autors",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        naziv = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Clanstvoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        KorisnikID = c.Long(nullable: false),
                        datum_racuna = c.DateTime(nullable: false),
                        istek_racuna = c.DateTime(nullable: false),
                        clanski_broj = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikID, cascadeDelete: true)
                .Index(t => t.KorisnikID);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TipRacunaID = c.Long(nullable: false),
                        ime = c.String(maxLength: 20),
                        prezime = c.String(maxLength: 20),
                        telefon = c.String(maxLength: 20),
                        adresa = c.String(maxLength: 50),
                        email = c.String(),
                        izbrisan = c.Boolean(nullable: false),
                        username = c.String(maxLength: 20),
                        password = c.String(maxLength: 20),
                        odobren = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TipRacunas", t => t.TipRacunaID, cascadeDelete: true)
                .Index(t => t.TipRacunaID);
            
            CreateTable(
                "dbo.TipRacunas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        referenca = c.String(nullable: false, maxLength: 20),
                        opis = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Zaduzenjas",
                c => new
                    {
                        KorisnikID = c.Long(nullable: false),
                        ZaposlenikID = c.Long(nullable: false),
                        ID = c.Long(nullable: false, identity: true),
                        status = c.String(maxLength: 20),
                        datum_zaduzenja = c.DateTime(nullable: false),
                        datum_vracanja = c.DateTime(nullable: false),
                        rok = c.DateTime(nullable: false),
                        KnjigaID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Knjigas", t => t.KnjigaID, cascadeDelete: true)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikID)
                .ForeignKey("dbo.Korisniks", t => t.ZaposlenikID)
                .Index(t => t.KorisnikID)
                .Index(t => t.ZaposlenikID)
                .Index(t => t.KnjigaID);
            
            CreateTable(
                "dbo.Knjigas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IzdavacID = c.Long(nullable: false),
                        TipKnjigeID = c.Long(nullable: false),
                        JezikID = c.Long(nullable: false),
                        naslov = c.String(maxLength: 45),
                        isbn = c.String(maxLength: 45),
                        broj_strana = c.Int(nullable: false),
                        godina_izdavanja = c.DateTime(nullable: false),
                        ukupno_kopija = c.Int(nullable: false),
                        dostupno_kopija = c.Int(nullable: false),
                        izdanje = c.Int(nullable: false),
                        opis = c.String(),
                        izbrisano = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Izdavacs", t => t.IzdavacID, cascadeDelete: true)
                .ForeignKey("dbo.Jeziks", t => t.JezikID, cascadeDelete: true)
                .ForeignKey("dbo.TipKnjiges", t => t.TipKnjigeID, cascadeDelete: true)
                .Index(t => t.IzdavacID)
                .Index(t => t.TipKnjigeID)
                .Index(t => t.JezikID);
            
            CreateTable(
                "dbo.Izdavacs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        naziv = c.String(maxLength: 45),
                        adresa = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Jeziks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        referenca = c.String(nullable: false, maxLength: 45),
                        opis = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipKnjiges",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        referenca = c.String(nullable: false, maxLength: 45),
                        opis = c.String(maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rezervacijas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        status = c.String(maxLength: 20),
                        datum_rezervacije = c.DateTime(nullable: false),
                        cekanje = c.Int(nullable: false),
                        KorisnikID = c.Long(nullable: false),
                        KnjigaID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Knjigas", t => t.KnjigaID, cascadeDelete: true)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikID, cascadeDelete: true)
                .Index(t => t.KorisnikID)
                .Index(t => t.KnjigaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezervacijas", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Rezervacijas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Clanstvoes", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Zaduzenjas", "ZaposlenikID", "dbo.Korisniks");
            DropForeignKey("dbo.Zaduzenjas", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Zaduzenjas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Knjigas", "TipKnjigeID", "dbo.TipKnjiges");
            DropForeignKey("dbo.Knjigas", "JezikID", "dbo.Jeziks");
            DropForeignKey("dbo.Knjigas", "IzdavacID", "dbo.Izdavacs");
            DropForeignKey("dbo.Korisniks", "TipRacunaID", "dbo.TipRacunas");
            DropIndex("dbo.Rezervacijas", new[] { "KnjigaID" });
            DropIndex("dbo.Rezervacijas", new[] { "KorisnikID" });
            DropIndex("dbo.Knjigas", new[] { "JezikID" });
            DropIndex("dbo.Knjigas", new[] { "TipKnjigeID" });
            DropIndex("dbo.Knjigas", new[] { "IzdavacID" });
            DropIndex("dbo.Zaduzenjas", new[] { "KnjigaID" });
            DropIndex("dbo.Zaduzenjas", new[] { "ZaposlenikID" });
            DropIndex("dbo.Zaduzenjas", new[] { "KorisnikID" });
            DropIndex("dbo.Korisniks", new[] { "TipRacunaID" });
            DropIndex("dbo.Clanstvoes", new[] { "KorisnikID" });
            DropTable("dbo.Rezervacijas");
            DropTable("dbo.TipKnjiges");
            DropTable("dbo.Jeziks");
            DropTable("dbo.Izdavacs");
            DropTable("dbo.Knjigas");
            DropTable("dbo.Zaduzenjas");
            DropTable("dbo.TipRacunas");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Clanstvoes");
            DropTable("dbo.Autors");
        }
    }
}
