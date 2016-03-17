namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clanstvoes", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Korisniks", "TipRacunaID", "dbo.TipRacunas");
            DropForeignKey("dbo.Zaduzenjas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Knjigas", "IzdavacID", "dbo.Izdavacs");
            DropForeignKey("dbo.Knjigas", "JezikID", "dbo.Jeziks");
            DropForeignKey("dbo.Knjigas", "TipKnjigeID", "dbo.TipKnjiges");
            DropForeignKey("dbo.Rezervacijas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Rezervacijas", "KorisnikID", "dbo.Korisniks");
            DropIndex("dbo.Zaduzenjas", new[] { "KorisnikID" });
            DropIndex("dbo.Zaduzenjas", new[] { "ZaposlenikID" });
            DropIndex("dbo.Zaduzenjas", new[] { "Korisnik_ID" });
            DropIndex("dbo.Zaduzenjas", new[] { "Korisnik_ID1" });
            DropColumn("dbo.Zaduzenjas", "KorisnikID");
            DropColumn("dbo.Zaduzenjas", "ZaposlenikID");
            RenameColumn(table: "dbo.Zaduzenjas", name: "Korisnik_ID", newName: "KorisnikID");
            RenameColumn(table: "dbo.Zaduzenjas", name: "Korisnik_ID1", newName: "ZaposlenikID");
            CreateTable(
                "dbo.KnjigaAutors",
                c => new
                    {
                        Knjiga_ID = c.Long(nullable: false),
                        Autor_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Knjiga_ID, t.Autor_ID })
                .ForeignKey("dbo.Knjigas", t => t.Knjiga_ID, cascadeDelete: true)
                .ForeignKey("dbo.Autors", t => t.Autor_ID, cascadeDelete: true)
                .Index(t => t.Knjiga_ID)
                .Index(t => t.Autor_ID);
            
            AlterColumn("dbo.Zaduzenjas", "KorisnikID", c => c.Long(nullable: false));
            AlterColumn("dbo.Zaduzenjas", "ZaposlenikID", c => c.Long(nullable: false));
            CreateIndex("dbo.Zaduzenjas", "KorisnikID");
            CreateIndex("dbo.Zaduzenjas", "ZaposlenikID");
            AddForeignKey("dbo.Clanstvoes", "KorisnikID", "dbo.Korisniks", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Korisniks", "TipRacunaID", "dbo.TipRacunas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Zaduzenjas", "KnjigaID", "dbo.Knjigas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Knjigas", "IzdavacID", "dbo.Izdavacs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Knjigas", "JezikID", "dbo.Jeziks", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Knjigas", "TipKnjigeID", "dbo.TipKnjiges", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Rezervacijas", "KnjigaID", "dbo.Knjigas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Rezervacijas", "KorisnikID", "dbo.Korisniks", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Zaduzenjas", "KorisnikID", "dbo.Korisniks", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Zaduzenjas", "ZaposlenikID", "dbo.Korisniks", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezervacijas", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Rezervacijas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Knjigas", "TipKnjigeID", "dbo.TipKnjiges");
            DropForeignKey("dbo.Knjigas", "JezikID", "dbo.Jeziks");
            DropForeignKey("dbo.Knjigas", "IzdavacID", "dbo.Izdavacs");
            DropForeignKey("dbo.Zaduzenjas", "KnjigaID", "dbo.Knjigas");
            DropForeignKey("dbo.Korisniks", "TipRacunaID", "dbo.TipRacunas");
            DropForeignKey("dbo.Clanstvoes", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.KnjigaAutors", "Autor_ID", "dbo.Autors");
            DropForeignKey("dbo.KnjigaAutors", "Knjiga_ID", "dbo.Knjigas");
            DropIndex("dbo.KnjigaAutors", new[] { "Autor_ID" });
            DropIndex("dbo.KnjigaAutors", new[] { "Knjiga_ID" });
            DropIndex("dbo.Zaduzenjas", new[] { "ZaposlenikID" });
            DropIndex("dbo.Zaduzenjas", new[] { "KorisnikID" });
            AlterColumn("dbo.Zaduzenjas", "ZaposlenikID", c => c.Long());
            AlterColumn("dbo.Zaduzenjas", "KorisnikID", c => c.Long());
            DropTable("dbo.KnjigaAutors");
            RenameColumn(table: "dbo.Zaduzenjas", name: "ZaposlenikID", newName: "Korisnik_ID1");
            RenameColumn(table: "dbo.Zaduzenjas", name: "KorisnikID", newName: "Korisnik_ID");
            AddColumn("dbo.Zaduzenjas", "ZaposlenikID", c => c.Long(nullable: false));
            AddColumn("dbo.Zaduzenjas", "KorisnikID", c => c.Long(nullable: false));
            CreateIndex("dbo.Zaduzenjas", "Korisnik_ID1");
            CreateIndex("dbo.Zaduzenjas", "Korisnik_ID");
            CreateIndex("dbo.Zaduzenjas", "ZaposlenikID");
            CreateIndex("dbo.Zaduzenjas", "KorisnikID");
            AddForeignKey("dbo.Rezervacijas", "KorisnikID", "dbo.Korisniks", "ID");
            AddForeignKey("dbo.Rezervacijas", "KnjigaID", "dbo.Knjigas", "ID");
            AddForeignKey("dbo.Knjigas", "TipKnjigeID", "dbo.TipKnjiges", "ID");
            AddForeignKey("dbo.Knjigas", "JezikID", "dbo.Jeziks", "ID");
            AddForeignKey("dbo.Knjigas", "IzdavacID", "dbo.Izdavacs", "ID");
            AddForeignKey("dbo.Zaduzenjas", "KnjigaID", "dbo.Knjigas", "ID");
            AddForeignKey("dbo.Korisniks", "TipRacunaID", "dbo.TipRacunas", "ID");
            AddForeignKey("dbo.Clanstvoes", "KorisnikID", "dbo.Korisniks", "ID");
        }
    }
}
