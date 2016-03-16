namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autors",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        naziv = c.String(),
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
                .ForeignKey("dbo.Korisniks", t => t.KorisnikID)
                .Index(t => t.KorisnikID);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TipRacunaID = c.Long(nullable: false),
                        ime = c.String(),
                        prezime = c.String(),
                        telefon = c.String(),
                        adresa = c.String(),
                        email = c.String(),
                        izbrisan = c.Boolean(nullable: false),
                        username = c.String(),
                        password = c.String(),
                        odobren = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TipRacunas", t => t.TipRacunaID)
                .Index(t => t.TipRacunaID);
            
            CreateTable(
                "dbo.TipRacunas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        referenca = c.String(),
                        opis = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Zaduzenjas",
                c => new
                    {
                        KorisnikID = c.Long(nullable: false),
                        ZaposlenikID = c.Long(nullable: false),
                        ID = c.Long(nullable: false, identity: true),
                        status = c.String(),
                        datum_zaduzenja = c.DateTime(nullable: false),
                        datum_vracanja = c.DateTime(nullable: false),
                        rok = c.DateTime(nullable: false),
                        cekanje = c.Int(nullable: false),
                        KnjigaID = c.Long(nullable: false),
                        Korisnik_ID = c.Long(),
                        Korisnik_ID1 = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Knjigas", t => t.KnjigaID)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikID)
                .ForeignKey("dbo.Korisniks", t => t.ZaposlenikID)
                .ForeignKey("dbo.Korisniks", t => t.Korisnik_ID)
                .ForeignKey("dbo.Korisniks", t => t.Korisnik_ID1)
                .Index(t => t.KorisnikID)
                .Index(t => t.ZaposlenikID)
                .Index(t => t.KnjigaID)
                .Index(t => t.Korisnik_ID)
                .Index(t => t.Korisnik_ID1);
            
            CreateTable(
                "dbo.Knjigas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IzdavacID = c.Long(nullable: false),
                        TipKnjigeID = c.Long(nullable: false),
                        JezikID = c.Long(nullable: false),
                        naslov = c.String(),
                        isbn = c.String(),
                        broj_strana = c.Int(nullable: false),
                        godina_izdavanja = c.DateTime(nullable: false),
                        ukupno_kopija = c.Int(nullable: false),
                        dostupno_kopija = c.Int(nullable: false),
                        izdanje = c.Int(nullable: false),
                        opis = c.String(),
                        izbrisano = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Izdavacs", t => t.IzdavacID)
                .ForeignKey("dbo.Jeziks", t => t.JezikID)
                .ForeignKey("dbo.TipKnjiges", t => t.TipKnjigeID)
                .Index(t => t.IzdavacID)
                .Index(t => t.TipKnjigeID)
                .Index(t => t.JezikID);
            
            CreateTable(
                "dbo.Izdavacs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        naziv = c.String(),
                        adresa = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Jeziks",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        referenca = c.String(),
                        opis = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipKnjiges",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        referenca = c.String(),
                        opis = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rezervacijas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        status = c.String(),
                        datum_rezervacije = c.DateTime(nullable: false),
                        cekanje = c.Int(nullable: false),
                        KorisnikID = c.Long(nullable: false),
                        KnjigaID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Knjigas", t => t.KnjigaID)
                .ForeignKey("dbo.Korisniks", t => t.KorisnikID)
                .Index(t => t.KorisnikID)
                .Index(t => t.KnjigaID);
            
            DropTable("dbo.Probas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Probas",
                c => new
                    {
                        ProbaId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ProbaId);
            
            DropForeignKey("dbo.Rezervacijas", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Rezervacijas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Clanstvoes", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Zaduzenjas", "Korisnik_ID1", "dbo.Korisniks");
            DropForeignKey("dbo.Zaduzenjas", "Korisnik_ID", "dbo.Korisniks");
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
            DropIndex("dbo.Zaduzenjas", new[] { "Korisnik_ID1" });
            DropIndex("dbo.Zaduzenjas", new[] { "Korisnik_ID" });
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
