using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class ProbaContext : DbContext
    {
        public ProbaContext() : base("ProbaContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
/*
            modelBuilder.Entity<Zaduzenja>()
                    .HasRequired(m => m.Korisnik)
                    .WithMany(t => t.Zaduzenja)
                    .HasForeignKey(m => m.KorisnikID)
                    .WillCascadeOnDelete(false);
            modelBuilder.Entity<Zaduzenja>()
                    .HasRequired(m => m.Zaposlenik)
                    .WithMany(t => t.ZaduzenjaZaposlenik)
                    .HasForeignKey(m => m.ZaposlenikID)
                    .WillCascadeOnDelete(false);*/
        }
        public DbSet<Autor> Autori { get; set; }
        public DbSet<Clanstvo> Clanstva { get; set; }
        public DbSet<Izdavac> Products { get; set; }
        public DbSet<Jezik> Jezici { get; set; }
        public DbSet<Knjiga> Knjige { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<TipKnjige> TipoviKnjiga { get; set; }
        public DbSet<TipRacuna> TipoviRacuna { get; set; }
        public DbSet<Zaduzenja> Zaduzenja { get; set; }

    }
}