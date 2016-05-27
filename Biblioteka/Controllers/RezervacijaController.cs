using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Biblioteka.Models;
using System.Data.Entity.Core.Objects;
using System.Net.Mail;
using Biblioteka.Security;

namespace Biblioteka.Controllers
{
    public class RezervacijaController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // POST api/Rezervacija
        [CustomAuthorize(Roles = "c")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult PostRezervacija(long idKnjige)
        {
            Rezervacija rezervacija = new Rezervacija();
            rezervacija.KnjigaID = idKnjige;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Onemogucavanje da korisnik napravi rezervaciju drugom korisnuku
            CustomPrincipal cp = new CustomPrincipal(SessionPersister.username);
            Korisnik current_user = db.Korisniks.Where(c => c.username == cp.Identity.Name).First();
            rezervacija.KorisnikID = current_user.ID;

            //Provjera da li je isteklo clanstvo
            DateTime d = System.DateTime.Now;
            List<Clanstvo> clanstvaa = db.Clanstvoes.Where(c => c.KorisnikID == current_user.ID && c.istek_racuna > d).ToList();
            if (clanstvaa.Count() < 1)
            {
                rezervacija.status = "clanstvo_produzi";
                return Ok(rezervacija);
            }

            rezervacija.cekanje = 2;
            rezervacija.datum_rezervacije = System.DateTime.Now;

            //provjeravamo da li je vec rezervisano
            List<Rezervacija> rezzz = db.Rezervacijas.Where(r => r.KnjigaID == rezervacija.KnjigaID && r.KorisnikID == rezervacija.KorisnikID && (r.status == "co" || r.status == "wa")).ToList();
            List<Zaduzenja> zaddd = db.Zaduzenjas.Where(z => z.KnjigaID == rezervacija.KnjigaID && z.KorisnikID == rezervacija.KorisnikID && z.status == "nv").ToList();
            if (rezzz.Count() > 0 || zaddd.Count > 0)
            {
                rezervacija.status = "vec_rezervisano";
                return Ok(rezervacija);
            }

            Knjiga k = db.Knjigas.Find(rezervacija.KnjigaID);
            int dostupno_knjiga = k.ukupno_kopija;

            //oduzimamo broj onih koji nisu vratili
            List<Zaduzenja> zad = db.Zaduzenjas.Where(z => z.KnjigaID == rezervacija.KnjigaID && z.status == "nv").ToList();
            dostupno_knjiga -= zad.Count();

            //oduzimamo broj onih na koje se ceka da dodju po knjigu
            List<Rezervacija> rez = db.Rezervacijas.Where(r => r.KnjigaID == rezervacija.KnjigaID && r.status == "co").ToList();
            dostupno_knjiga -= rez.Count();

            if (dostupno_knjiga > 0)
            {
                rezervacija.status = "co";
            }
            else
            {
                dostupno_knjiga = Azuriraj(rezervacija.KnjigaID);

                if (dostupno_knjiga > 0)
                    rezervacija.status = "co";
                else
                    rezervacija.status = "wa";
            }

            db.Rezervacijas.Add(rezervacija);
            db.SaveChanges();

            //preracunavanje dostupno kopija za knjigu
            Knjiga kkk = db.Knjigas.Find(idKnjige);
            kkk.dostupno_kopija = kkk.ukupno_kopija - (db.Zaduzenjas.Where(z => z.KnjigaID == idKnjige && z.status == "nv").Count() +
                                db.Rezervacijas.Where(r => r.KnjigaID == idKnjige && (r.status == "co" || r.status == "wa")).Count());

            db.Entry(kkk).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }

            return CreatedAtRoute("DefaultApi", new { id = rezervacija.ID }, rezervacija);
        }

        private int Azuriraj(long id_knjige)
        {

            //saljemo mail svima onima kojima je isteklo vrijeme cekanja, i ponistavamo njihove rezervacije
            List<Rezervacija> nisu_dosli = db.Rezervacijas.Where(r => r.KnjigaID == id_knjige && r.status == "co")
                                                          .ToList()
                                                          .Where(r => System.DateTime.Now > r.datum_rezervacije.AddDays(r.cekanje)).ToList();

            List<string> emails = new List<string>();
            foreach (Rezervacija r in nisu_dosli)
            {
                emails.Add(r.Korisnik.email);
                r.status = "no";

                db.Entry(r).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException) { }
            }

            Knjiga k = db.Knjigas.Find(id_knjige);
            sendEmailTimeIsUp(emails, k.naslov, "no");

            //sada se salje mail onima koji nisu vratili, a isteklo im je vrijeme
            DateTime d = System.DateTime.Now;
            List<Zaduzenja> nisu_vratili = db.Zaduzenjas.Where(z => z.KnjigaID == id_knjige && z.status == "nv" &&
                    (d > z.rok)).ToList();

            List<string> emails_nv = new List<string>();
            foreach (Zaduzenja z in nisu_vratili)
                emails_nv.Add(z.Korisnik.email);
            sendEmailTimeIsUp(emails_nv, k.naslov, "vr");

            //sada ponovo racunamo koliko je dostupno knjiga
            int dostupno_knjiga = db.Knjigas.Find(id_knjige).ukupno_kopija;

            List<Zaduzenja> zad = db.Zaduzenjas.Where(z => z.KnjigaID == id_knjige && z.status == "nv").ToList();
            dostupno_knjiga -= zad.Count();

            List<Rezervacija> rez = db.Rezervacijas.Where(r => r.KnjigaID == id_knjige && r.status == "co").ToList();
            dostupno_knjiga -= rez.Count();

            //sada saljemo mail onima koji cekaju, i modifikujemo im rezervaciju na "co"
            List<string> emails2 = new List<string>();
            List<Rezervacija> cekaju = db.Rezervacijas.Where(r => r.KnjigaID == id_knjige && r.status == "wa").ToList();
            cekaju.Sort(delegate(Rezervacija a, Rezervacija b)
            {
                return a.datum_rezervacije.CompareTo(b.datum_rezervacije);
            });
            int podjela = (dostupno_knjiga > cekaju.Count()) ? cekaju.Count() : dostupno_knjiga;
            while (podjela > 0)
            {
                emails2.Add(cekaju[podjela - 1].Korisnik.email);
                cekaju[podjela - 1].status = "co";
                cekaju[podjela - 1].cekanje = 3;//sada dajemo 3 dana posto su cekali
                cekaju[podjela - 1].datum_rezervacije = System.DateTime.Now;

                db.Entry(cekaju[podjela - 1]).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException) { }

                podjela--;
            }

            sendEmailTimeIsUp(emails2, k.naslov, "co");

            if (dostupno_knjiga > cekaju.Count())
                return dostupno_knjiga - cekaju.Count();

            return 0;
        }

        // GET api/Rezervacija/username
        [CustomAuthorize(Roles = "b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Rezervacija>))]
        public IHttpActionResult GetRezervacija(string username)
        {
            List<Korisnik> korisnik = db.Korisniks.Where(k => k.username == username).ToList();

            if (korisnik.Count < 1)
                return BadRequest("pogr_username");

            long idKorisnika = korisnik[0].ID;
            List<Rezervacija> rezervacije = db.Rezervacijas.Where(r => r.KorisnikID == idKorisnika && r.status == "co").ToList();

            return Ok(rezervacije);
        }
        // GET api/Rezervacija
        [CustomAuthorize(Roles = "c")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Rezervacija>))]
        public IHttpActionResult GetRezervacijas()
        {
            CustomPrincipal cp = new CustomPrincipal(SessionPersister.username);
            Korisnik current_user = db.Korisniks.Where(c => c.username == cp.Identity.Name).First();

            long idKorisnika = current_user.ID;
            //pravi se lista tako da idu redom isti tipovi rezervacija
            List<Rezervacija> rezervacije_co = db.Rezervacijas.Where(r => r.KorisnikID == idKorisnika && r.status == "co").ToList();
            List<Rezervacija> rezervacije_wa = db.Rezervacijas.Where(r => r.KorisnikID == idKorisnika && r.status == "wa").ToList();
            List<Rezervacija> rezervacije_no = db.Rezervacijas.Where(r => r.KorisnikID == idKorisnika && r.status == "no").ToList();
            List<Rezervacija> rezervacije_re = db.Rezervacijas.Where(r => r.KorisnikID == idKorisnika && r.status == "re").ToList();

            foreach (Rezervacija r in rezervacije_co)
                r.status = "doci_po_knjigu";

            foreach (Rezervacija r in rezervacije_wa)
            {
                r.status = "obavjest_raspolozivost";
                rezervacije_co.Add(r);
            }
            foreach (Rezervacija r in rezervacije_no)
            {
                r.status = "rok_istekao";
                rezervacije_co.Add(r);
            }
            foreach (Rezervacija r in rezervacije_re)
            {
                r.status = "knjiga_zaduzena";
                rezervacije_co.Add(r);
            }

            return Ok(rezervacije_co);
        }

        [CustomAuthorize(Roles = "a,b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<int>))]
        public IHttpActionResult GetRezervacija(string ly, string cy, string comp)
        {
            List<int> rez_per_mon_l = new List<int>();
            List<int> rez_per_mon_c = new List<int>();
            int tmp_l = 0;
            int tmp_c = 0;

            for (int m = 1; m <= 12; m++)
            {

                tmp_l = (m > 1) ? rez_per_mon_l[m - 2] : 0;
                rez_per_mon_l.Add(tmp_l + db.Rezervacijas.Where(r => r.datum_rezervacije.Month == m && r.datum_rezervacije.Year == DateTime.Now.Year - 1).Count());
                if (m <= DateTime.Now.Month)
                {
                    tmp_c = (m > 1) ? rez_per_mon_c[m - 2] : 0;
                    rez_per_mon_c.Add(tmp_c + db.Rezervacijas.Where(r => r.datum_rezervacije.Month == m && r.datum_rezervacije.Year == DateTime.Now.Year).Count());
                }
            }

            rez_per_mon_l.AddRange(rez_per_mon_c);
            return Ok(rez_per_mon_l);
        }

        // samo tokom TESTIRANJA treba
        // DELETE ALL
        // GET api/Rezervacija/del
        [CustomAuthorize(Roles = "b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(void))]
        public IHttpActionResult GetRezervacija(string del, string all)
        {
            List<Rezervacija> rezervacije = db.Rezervacijas.Where(i => i.ID >= 1).ToList();
            List<Zaduzenja> zaduzenja = db.Zaduzenjas.Where(i => i.ID >= 1).ToList();
            List<Knjiga> knjige = db.Knjigas.Where(i => i.ID >= 1).ToList();

            foreach (Knjiga k in knjige)
            {
                k.dostupno_kopija = 10;
                k.ukupno_kopija = 10;
                if (k.ID == 1)
                {
                    //"Zlocin i kazna"
                    k.dostupno_kopija = 1;
                    k.ukupno_kopija = 1;
                }
                db.Entry(k).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException) { }
            }

            foreach (Rezervacija r in rezervacije)
                if (r != null)
                {
                    db.Rezervacijas.Remove(r);
                    db.SaveChanges();
                }

            foreach (Zaduzenja z in zaduzenja)
                if (z != null)
                {
                    db.Zaduzenjas.Remove(z);
                    db.SaveChanges();
                }


            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RezervacijaExists(long id)
        {
            return db.Rezervacijas.Count(e => e.ID == id) > 0;
        }

        [HttpPost]
        private void sendEmailTimeIsUp(List<string> mailsTo, string naziv_knjige, string tip_maila)
        {
            string from = "bibliotekanwt@gmail.com";
            string pass = "bibliotekanwtpass1";

            MailMessage email = new MailMessage();
            email.From = new MailAddress(from);

            foreach (String m in mailsTo)
                email.To.Add(m);

            email.Subject = "Obavještenje";
            email.IsBodyHtml = true;

            if (tip_maila == "no")
            {
                email.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>Obavještavamo Vas da je Vaša rezervacija knjige \"" + naziv_knjige + "\" istekla. Ukoliko želite zadužiti ovu knjigu molimo Vas da je ponovo rezervišete!" +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";

            }
            else if (tip_maila == "vr")
            {
                email.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>Obavještavamo Vas da je istekao rok za vračanje knjige \"" + naziv_knjige + "\" koju ste zadužili, pa Vas molimo da, što je prije moguće vratite istu, kako bi i drugi čitaoci mogli doći do knjige." +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";

            }
            else if (tip_maila == "co")
            {
                email.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>Obavještavamo Vas da je dostupna knjiga \"" + naziv_knjige + "\" na čiju rezervaciju ste čekali. Mi smo automatski napravili rezervaciju za Vas. Po knjigu mozete doći u sljedeća tri dana, kako bi istu zadužili." +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";
            }


            SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com", 587);
            SMTPServer.Credentials = new System.Net.NetworkCredential(from, pass);
            SMTPServer.EnableSsl = true;

            try
            {
                SMTPServer.Send(email);
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }

        }

    }
}