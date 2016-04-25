namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Biblioteka.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Biblioteka.Models.ProbaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Biblioteka.Models.ProbaContext context)
        {

            //tipovi knjiga
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 1, referenca = "lit", opis = "Književnost" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 2, referenca = "pro", opis = "Stručna literatura" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 3, referenca = "phi", opis = "Filozofija" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 4, referenca = "sci", opis = "Nauka" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 5, referenca = "law", opis = "Zakon" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 6, referenca = "rel", opis = "Religija" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 7, referenca = "let", opis = "Pismo" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 8, referenca = "ess", opis = "Esej" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 9, referenca = "dia", opis = "Dnevnici i Časopisi" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 10, referenca = "mem", opis = "Autobiografija" });
            context.TipKnjiges.AddOrUpdate(new TipKnjige { ID = 11, referenca = "bio", opis = "Biografija" });
            context.SaveChanges();

            //jezici, oznake po: ISO 639-2
            context.Jeziks.AddOrUpdate(new Jezik { ID = 1, referenca = "ara", opis = "Arapski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 2, referenca = "bos", opis = "Bosanski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 3, referenca = "ger", opis = "Njemacki" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 4, referenca = "gre", opis = "Grčki" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 5, referenca = "eng", opis = "Engleski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 6, referenca = "fre", opis = "Francuski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 7, referenca = "hrv", opis = "Hrvatski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 8, referenca = "ita", opis = "Italijanski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 9, referenca = "mac", opis = "Makedonski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 10, referenca = "rus", opis = "Ruski" });
            context.Jeziks.AddOrUpdate(new Jezik { ID = 11, referenca = "tur", opis = "Turski" });
            context.SaveChanges();

            //izdavaci
            context.Izdavacs.AddOrUpdate(new Izdavac { ID = 1, naziv = "Connectum", adresa = "8. mart br. 56, 71000 Sarajevo, Bosna i Hercegovina" });
            context.Izdavacs.AddOrUpdate(new Izdavac { ID = 2, naziv = "Pearson", adresa = "Nije poznata" });
            context.SaveChanges();

            //knjige

            Knjiga k1 = new Knjiga
            {
                ID = 1,
                IzdavacID = 1,
                TipKnjigeID = 1,
                JezikID = 2,
                naslov = "Zlocin i kazna",
                isbn = "86-7674-029-1",
                idbroj = "1000",
                broj_strana = 524,
                godina_izdavanja = new DateTime(1866, 1, 1),
                ukupno_kopija = 1,
                dostupno_kopija = 1,
                izdanje = 1,
                opis = "Zločin i kazna je roman ruskog pisca Fjodora Mihajloviča Dostojevskog izdan 1866." +
                "godine u časopisu Ruski vjesnik. Smatra se jednim od najvećih djela ruske književnosti.",
                izbrisano = false
            };

            Knjiga k2 = new Knjiga
            {
                ID = 2,
                IzdavacID = 1,
                TipKnjigeID = 1,
                JezikID = 2,
                idbroj = "1001",
                naslov = "Hiljadu i jedna noć",
                isbn = "3-932068-56-4",
                broj_strana = 887,
                godina_izdavanja = new DateTime(1704, 7, 7),
                ukupno_kopija = 1,
                dostupno_kopija = 1,
                izdanje = 1,
                opis = "Hiljadu i jedna noć je zbirka dužih i kraćih priča koje su nastale u periodu od" +
                "9. do 14. vijeka. Priče imaju korijene iz starih civilizacija Arabije, Perzije, Indije, Egipta i Mezopotamije." +
                "Osnova cijele ove zbirke je priča o Šeherzadi.",
                izbrisano = false
            };

            Knjiga k3 = new Knjiga
            {
                ID = 3,
                IzdavacID = 1,
                TipKnjigeID = 1,
                JezikID = 2,
                naslov = "Hamlet",
                isbn = "0-486272-78-8",
                broj_strana = 192,
                godina_izdavanja = new DateTime(1603, 1, 1),
                ukupno_kopija = 1,
                dostupno_kopija = 1,
                izdanje = 1,
                opis = "Hamlet je tragedija Williama Shakespearea, jedna od njegovih najpoznatijih i " +
                "najizvođenijih tragedija na pozornicama širom svijeta. Napisana je u periodu između 1600. i ljeta 1602. " +
                "godine, u drugom razdoblju Shakespearova stvaralaštva. Legenda o Hamletu spominje se već u 9. stoljeću i " +
                "potječe iz drevnih skandinavskih saga, a zapisao ju danski pjesnik i povjesničar Saxo Gramaticus u 13. stoljeću.",
                izbrisano = false
            };

            Knjiga k4 = new Knjiga
            {
                ID = 4,
                IzdavacID = 1,
                TipKnjigeID = 1,
                JezikID = 2,
                naslov = "Derviš i smrt",
                isbn = "8-675721-54-4",
                broj_strana = 473,
                godina_izdavanja = new DateTime(1966, 1, 1),
                ukupno_kopija = 1,
                idbroj = "1002",
                dostupno_kopija = 1,
                izdanje = 1,
                opis = "Derviš i smrt je najuspješniji roman bosanskohercegovačkog pisca Meše Selimovića." +
                "Roman je pisan u razdoblju od četiri godine (1962-1966) u piščevom poodmaklom dobu. Objavljen je 1966. godine " +
                "od strane izdavačke kuće Svjetlost iz Sarajeva i doživio nevjerovatan uspjeh u okvirima čitalačke javnosti " +
                "širom tadašnje Jugoslavije. Roman je doživio brojna reizdanja. Derviš i smrt donio je Selimoviću mnogobrojne " +
                "najviše jugoslavenske nagrade, između ostalih Njegoševu, Goranovu i NIN-ovu nagradu.",
                izbrisano = false
            };

            Knjiga k5 = new Knjiga
            {
                ID = 5,
                IzdavacID = 1,
                TipKnjigeID = 1,
                JezikID = 2,
                naslov = "Tvrđava",
                isbn = "8-661070-19-8",
                broj_strana = 383,
                godina_izdavanja = new DateTime(1970, 1, 1),
                ukupno_kopija = 1,
                dostupno_kopija = 1,
                izdanje = 1,
                opis = "Tvrđava je jedan od poznatijih romana bosanskohercegovačkog pisca Meše Selimovića. " +
                "Ovaj roman koji je prvi put objavljen 1970. godine, predstavlja svojevrsnog blizanca Selimovićevog prethodnog " +
                "romana Derviš i smrt (1966.) U ovom romanu pisac nastavlja uzvišenu metaforu iz romana Derviš i smrt " +
                "o čovjeku kome prijete smrt i vlast. Unutrašnji prostor ovog romana jasnije je određen duhom nego što " +
                "je spolja određen realnim historijskim vremenom.",
                izbrisano = false
            };

            Knjiga k6 = new Knjiga
            {
                ID = 6,
                IzdavacID = 2,
                TipKnjigeID = 2,
                JezikID = 5,
                naslov = "Computer networking",
                isbn = "0-13-285620-4",
                broj_strana = 889,
                godina_izdavanja = new DateTime(2010, 1, 1),
                ukupno_kopija = 2,
                dostupno_kopija = 2,
                izdanje = 6,
                opis = "",
                izbrisano = false
            };

            //autori
            Autor a1 = new Autor { ID = 1, naziv = "Autor nepoznat" };
            a1.Knjige = new List<Knjiga>();
            a1.Knjige.Add(k2);

            Autor a2 = new Autor { ID = 2, naziv = "Mesa Selimovic" };
            a2.Knjige = new List<Knjiga>();
            a2.Knjige.Add(k4);
            a2.Knjige.Add(k5);

            Autor a3 = new Autor { ID = 3, naziv = "William Shakespeare" };
            a3.Knjige = new List<Knjiga>();
            a3.Knjige.Add(k3);

            Autor a4 = new Autor { ID = 4, naziv = "Fjodor Dostojevski" };
            a4.Knjige = new List<Knjiga>();
            a4.Knjige.Add(k1);

            Autor a5 = new Autor { ID = 5, naziv = "James F. Kurose" };
            a5.Knjige = new List<Knjiga>();
            a5.Knjige.Add(k6);

            Autor a6 = new Autor { ID = 6, naziv = "Keith W. Ross" };
            a6.Knjige = new List<Knjiga>();
            a6.Knjige.Add(k6);

            context.Autors.AddOrUpdate(a1);
            context.Autors.AddOrUpdate(a2);
            context.Autors.AddOrUpdate(a3);
            context.Autors.AddOrUpdate(a4);
            context.Autors.AddOrUpdate(a5);
            context.Autors.AddOrUpdate(a6);
            context.SaveChanges();


            //tipovi racuna
            context.TipRacunas.AddOrUpdate(new TipRacuna { ID = 1, referenca = "c", opis = "Član" });
            context.TipRacunas.AddOrUpdate(new TipRacuna { ID = 2, referenca = "b", opis = "Bibliotekar" });
            context.TipRacunas.AddOrUpdate(new TipRacuna { ID = 3, referenca = "a", opis = "Administrator" });
            context.SaveChanges();

            //korisnici
            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 1,
                TipRacunaID = 3,
                ime = "Administrator1",
                prezime = "Prezime",
                telefon = "062-111-111",
                adresa = "Ulica bb, Sarajevo",
                email = "administrator1@hotmail.com",
                izbrisan = false,
                username = "admin1",
                password = "admin1pass",
                odobren = true
            });

            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 2,
                TipRacunaID = 3,
                ime = "Administrator2",
                prezime = "Prezime",
                telefon = "062-222-222",
                adresa = "Ulica bb, Sarajevo",
                email = "administrator2@hotmail.com",
                izbrisan = false,
                username = "admin2",
                password = "admin2pass",
                odobren = true
            });

            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 3,
                TipRacunaID = 2,
                ime = "Bibliotekar1",
                prezime = "Prezime",
                telefon = "062-333-333",
                adresa = "Ulica bb, Sarajevo",
                email = "bibliotekar1@hotmail.com",
                izbrisan = false,
                username = "bib1",
                password = "bib1pass",
                odobren = true
            });

            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 4,
                TipRacunaID = 2,
                ime = "Bibliotekar2",
                prezime = "Prezime",
                telefon = "062-444-444",
                adresa = "Ulica bb, Sarajevo",
                email = "bibliotekar2@hotmail.com",
                izbrisan = false,
                username = "bib2",
                password = "bib2pass",
                odobren = true
            });

            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 5,
                TipRacunaID = 1,
                ime = "Clan1",
                prezime = "Prezime",
                telefon = "062-555-555",
                adresa = "Ulica bb, Sarajevo",
                email = "clan1@hotmail.com",
                izbrisan = false,
                username = "clan1",
                password = "clan1pass",
                odobren = true
            });

            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 6,
                TipRacunaID = 1,
                ime = "Clan2",
                prezime = "Prezime",
                telefon = "062-666-666",
                adresa = "Ulica bb, Sarajevo",
                email = "clan2@hotmail.com",
                izbrisan = false,
                username = "clan2",
                password = "clan2pass",
                odobren = true
            });

            context.Korisniks.AddOrUpdate(new Korisnik
            {
                ID = 7,
                TipRacunaID = 1,
                ime = "Clan3",
                prezime = "Prezime",
                telefon = "062-777-777",
                adresa = "Ulica bb, Sarajevo",
                email = "clan3@hotmail.com",
                izbrisan = false,
                username = "clan3",
                password = "clan3pass",
                odobren = false
            });
            context.SaveChanges();

            //clanstva
            context.Clanstvoes.AddOrUpdate(new Clanstvo
            {
                ID = 1,
                KorisnikID = 5,
                datum_racuna = new DateTime(2016, 1, 1),
                istek_racuna = new DateTime(2017, 1, 1),
                clanski_broj = 1000000
            });

            context.Clanstvoes.AddOrUpdate(new Clanstvo
            {
                ID = 2,
                KorisnikID = 6,
                datum_racuna = new DateTime(2016, 1, 2),
                istek_racuna = new DateTime(2017, 1, 2),
                clanski_broj = 1000001
            });
            context.SaveChanges();

            //rezervacije
            // ceka se da dodje po knjigu - co (coming)
            // nije dosao po knjigu - no (not)
            // ceka da se oslobodi neka knjiga - wa (waiting)
            // zaduzio knjigu - re (reserved)
            context.Rezervacijas.AddOrUpdate(new Rezervacija
            {
                ID = 1,
                status = "co",
                datum_rezervacije = new DateTime(2016, 3, 25),
                cekanje = 2,
                KorisnikID = 5,
                KnjigaID = 6
            });
            context.Rezervacijas.AddOrUpdate(new Rezervacija
            {
                ID = 2,
                status = "co",
                datum_rezervacije = new DateTime(2016, 4, 25),
                cekanje = 2,
                KorisnikID = 5,
                KnjigaID = 5
            });
            context.Rezervacijas.AddOrUpdate(new Rezervacija
            {
                ID = 3,
                status = "re",
                datum_rezervacije = new DateTime(2016, 2, 20),
                cekanje = 2,
                KorisnikID = 6,
                KnjigaID = 6
            });
            context.SaveChanges();

            //zaduzenja
            // vraceno - vr
            // nije vraceno - nv

            context.SaveChanges();

            context.Zaduzenjas.AddOrUpdate(new Zaduzenja
            {
                ID = 1,
                status = "nv",
                datum_zaduzenja = new DateTime(2016, 3, 21),
                datum_vracanja = null,
                rok = new DateTime(2016, 4, 25),
                KorisnikID = 6,
                KnjigaID = 6,
                ZaposlenikID = 3
            });
            context.SaveChanges();




            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person {ID = 1,FullName = "Andrew Peters" },
            //      new Person {ID = 1,FullName = "Brice Lambson" },
            //      new Person {ID = 1,FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}