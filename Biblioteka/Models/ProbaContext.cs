using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class ProbaContext : DbContext
    {
        public ProbaContext() : base("ProbaContext")
        {
        }
        public DbSet<Autor> Autori { get; set; }
        public DbSet<Clanstvo> Clanstva { get; set; }
        public DbSet<Izdavac> Products { get; set; }
        public DbSet<Jezik> Jezici { get; set; }
        public DbSet<Knjiga> Knjige { get; set; }
        public DbSet<KnjigaAutor> KnjigaAutor { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<TipKnjige> TipoviKnjiga { get; set; }
        public DbSet<TipRacuna> TipoviRacuna { get; set; }
        public DbSet<Zaduzenja> Zaduzenja { get; set; }

    }
}