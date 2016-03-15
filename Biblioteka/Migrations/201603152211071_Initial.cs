namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Probas",
                c => new
                    {
                        ProbaId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ProbaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Probas");
        }
    }
}
