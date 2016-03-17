namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Zaduzenjas", "KorisnikID", "dbo.Korisniks", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Zaduzenjas", "ZaposlenikID", "dbo.Korisniks", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zaduzenjas", "KorisnikID", "dbo.Korisniks");
            DropForeignKey("dbo.Zaduzenjas", "ZaposlenikID", "dbo.Korisniks");
        }
    }
}
