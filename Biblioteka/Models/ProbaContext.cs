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
  //          modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Zaduzenja>()
                    .HasRequired(m => m.Korisnik)
                    .WithMany(t => t.Zaduzenja)
                    .HasForeignKey(m => m.KorisnikID)
                    .WillCascadeOnDelete(false);
            modelBuilder.Entity<Zaduzenja>()
                    .HasRequired(m => m.Zaposlenik)
                    .WithMany(t => t.ZaduzenjaZaposlenik)
                    .HasForeignKey(m => m.ZaposlenikID)
                    .WillCascadeOnDelete(false);
        }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Clanstvo> Clanstvoes { get; set; }
        public DbSet<Jezik> Jeziks { get; set; }
        public DbSet<Knjiga> Knjigas { get; set; }
        public DbSet<Izdavac> Izdavacs { get; set; }
        public DbSet<Korisnik> Korisniks { get; set; }
        public DbSet<Rezervacija> Rezervacijas { get; set; }
        public DbSet<TipKnjige> TipKnjiges { get; set; }
        public DbSet<TipRacuna> TipRacunas { get; set; }
        public DbSet<Zaduzenja> Zaduzenjas { get; set; }
        public DbSet<Informacije> Informacijes { get; set; }
    }
}