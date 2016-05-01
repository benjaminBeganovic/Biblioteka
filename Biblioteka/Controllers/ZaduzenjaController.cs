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
using System.Net.Mail;
using Biblioteka.Security;

namespace Biblioteka.Controllers
{
    public class ZaduzenjaController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // Zaduzenje
        // POST api/Zaduzenja
        [CustomAuthorize(Roles = "b")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(Zaduzenja))]
        public IHttpActionResult PostZaduzenja(Zaduzenja zaduzenja)
        {
            zaduzenja.datum_vracanja = null;
            zaduzenja.datum_zaduzenja = System.DateTime.Now;

            CustomPrincipal cp = new CustomPrincipal(SessionPersister.username);
            Korisnik current_user = db.Korisniks.Where(c => c.username == cp.Identity.Name).First();
            zaduzenja.ZaposlenikID = current_user.ID;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //da li je vratio
            if (db.Zaduzenjas.Count(z => z.KnjigaID == zaduzenja.KnjigaID && z.KorisnikID == zaduzenja.KorisnikID && z.status == "nv") > 0)
            {
                zaduzenja.status = "vec_posjeduje_knjigu";
                return Ok(zaduzenja);
            }

            //da li je rezervisao
            List<Rezervacija> rez = new List<Rezervacija>();
            rez = db.Rezervacijas.Where(z => z.KnjigaID == zaduzenja.KnjigaID && z.KorisnikID == zaduzenja.KorisnikID && z.status == "co").ToList();

            if (rez.Count() < 1)
            {
                zaduzenja.status = "nije_zaduzio_knjigu";
                return Ok(zaduzenja);
            }

            rez[0].status = "re";
            zaduzenja.status = "nv";

            db.Entry(rez[0]).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }


            db.Zaduzenjas.Add(zaduzenja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = zaduzenja.ID }, zaduzenja);
        }

        // sve zaduzene knjige
        // GET api/Zaduzenja/
        [CustomAuthorize(Roles = "b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Zaduzenja>))]
        public IHttpActionResult GetZaduzenjas()
        {
            DateTime d = System.DateTime.Now;
            List<Zaduzenja> nisu_vratili = db.Zaduzenjas.Where(z => z.status == "nv" && (d > z.rok)).ToList();
            foreach (Zaduzenja z in nisu_vratili)
                z.status = "istekao_rok_razduzivanje";

            List<Zaduzenja> citaju = db.Zaduzenjas.Where(z => z.status == "nv" && (d <= z.rok)).ToList();
            foreach (Zaduzenja z in citaju)
            {
                z.status = "nije_istekao_rok_razduzivanje";
                nisu_vratili.Add(z);
            }

            List<Zaduzenja> vracene = db.Zaduzenjas.Where(z => z.status == "vr").ToList();
            foreach (Zaduzenja z in vracene)
            {
                z.status = "knjiga_razduzena";
                nisu_vratili.Add(z);
            }

            return Ok(nisu_vratili);
        }
        // Razduzenje
        // GET api/Zaduzenja/zid
        [CustomAuthorize(Roles = "b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(Zaduzenja))]
        public IHttpActionResult GetZaduzenja(long zid)
        {
            Zaduzenja zaduzenja = db.Zaduzenjas.Find(zid);
            if (zaduzenja == null)
            {
                return NotFound();
            }

            zaduzenja.status = "vr";
            zaduzenja.datum_vracanja = System.DateTime.Now;

            //sada saljemo mail onome koji je najvise cekao, i modifikujemo mu rezervaciju na "co"
            List<string> emails2 = new List<string>();
            List<Rezervacija> cekaju = db.Rezervacijas.Where(r => r.KnjigaID == zaduzenja.KnjigaID && r.status == "wa").ToList();
            cekaju.Sort(delegate(Rezervacija a, Rezervacija b)
            {
                return a.datum_rezervacije.CompareTo(b.datum_rezervacije);
            });

            if (cekaju.Count > 0)
            {
                emails2.Add(cekaju[0].Korisnik.email);
                cekaju[0].status = "co";
                cekaju[0].cekanje = 3;//sada dajemo 3 dana posto su cekali
                cekaju[0].datum_rezervacije = System.DateTime.Now;

                db.Entry(cekaju[0]).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException) { }

                Knjiga k = db.Knjigas.Find(zaduzenja.KnjigaID);
                sendEmailTimeIsUp(emails2, k.naslov, "co");
            }

            db.Entry(zaduzenja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }

            //preracunavanje dostupno kopija za knjigu
            long idKnjige = zaduzenja.KnjigaID;
            Knjiga kkk = db.Knjigas.Find(idKnjige);
            kkk.dostupno_kopija = kkk.dostupno_kopija + 1;

            db.Entry(kkk).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }

            zaduzenja.status = "uspjesno_ste_razduzili";
            return Ok(zaduzenja);
        }
        // GET api/Zaduzenja/username
        [CustomAuthorize(Roles = "b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Zaduzenja>))]
        public IHttpActionResult GetZaduzenja(string username)
        {
            List<Korisnik> korisnik = db.Korisniks.Where(k => k.username == username).ToList();

            if (korisnik.Count < 1)
                return BadRequest("pogr_username");

            long idKorisnika = korisnik[0].ID;
            List<Zaduzenja> zaduzenja = db.Zaduzenjas.Where(z => z.KorisnikID == idKorisnika && z.status == "nv").ToList();

            return Ok(zaduzenja);
        }
        // GET api/Zaduzenja/
        [CustomAuthorize(Roles = "c")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Zaduzenja>))]
        public IHttpActionResult GetZaduzenja(int cc)
        {
            CustomPrincipal cp = new CustomPrincipal(SessionPersister.username);
            Korisnik current_user = db.Korisniks.Where(c => c.username == cp.Identity.Name).First();
            long idKorisnika = current_user.ID;

            List<Zaduzenja> zaduzenja_nv = db.Zaduzenjas.Where(z => z.KorisnikID == idKorisnika && z.status == "nv").ToList();
            List<Zaduzenja> zaduzenja_vr = db.Zaduzenjas.Where(z => z.KorisnikID == idKorisnika && z.status == "vr").ToList();
            foreach (Zaduzenja z in zaduzenja_nv)
                z.status = "knjiga_nije_vracena";

            foreach (Zaduzenja z in zaduzenja_vr)
            {
                z.status = "knjiga_je_vracena";
                zaduzenja_nv.Add(z);
            }

            return Ok(zaduzenja_nv);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZaduzenjaExists(long id)
        {
            return db.Zaduzenjas.Count(e => e.ID == id) > 0;
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