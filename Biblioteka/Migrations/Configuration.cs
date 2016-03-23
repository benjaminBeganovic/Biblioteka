namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Biblioteka.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Biblioteka.Models.ProbaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Biblioteka.Models.ProbaContext context)
        {
            context.Autors.AddOrUpdate(new Autor { naziv = "Mesa Selimovic" });
            context.Autors.AddOrUpdate(new Autor { naziv = "Enver Colakovic" });
            context.Autors.AddOrUpdate(new Autor { naziv = "Aleksa Santic" });

            //i za ostale modele treba dodati


            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
