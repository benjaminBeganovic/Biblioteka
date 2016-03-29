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
        /*
        // GET api/Rezervacija
        [ResponseType(typeof(List<Rezervacija>))]
        public IHttpActionResult GetRezervacijas()
        {
            var rezervacije = db.Rezervacijas.ToList();
            return Ok(rezervacije);
        }

        // GET api/Rezervacija/5
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult GetRezervacija(long id)
        {
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return Ok(rezervacija);
        }

        
        // PUT api/Rezervacija/5
        public IHttpActionResult PutRezervacija(long id, Rezervacija rezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rezervacija.ID)
            {
                return BadRequest();
            }

            db.Entry(rezervacija).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervacijaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        */
        /*defaultna
        // POST api/Rezervacija
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult PostRezervacija(Rezervacija rezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rezervacijas.Add(rezervacija);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rezervacija.ID }, rezervacija);
        }
        */

        // POST api/Rezervacija
        [CustomAuthorize(Roles = "c")]
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult PostRezervacija(Rezervacija rezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            rezervacija.cekanje = 2;
            rezervacija.datum_rezervacije = System.DateTime.Now;

            //provjeravamo da li je vec rezervisano
            List<Rezervacija> rezzz = db.Rezervacijas.Where(r => r.KnjigaID == rezervacija.KnjigaID && r.KorisnikID == rezervacija.KorisnikID && (r.status == "co" || r.status == "wa")).ToList();
            List<Zaduzenja> zaddd = db.Zaduzenjas.Where(z => z.KnjigaID == rezervacija.KnjigaID && z.KorisnikID == rezervacija.KorisnikID && z.status == "nv").ToList();
            if(rezzz.Count() > 0 || zaddd.Count > 0)
            {
                return BadRequest("Vec ste rezervisali ovu knjigu!");
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

        // GET api/Rezervacija
        [CustomAuthorize(Roles = "b")]
        [ResponseType(typeof(String))]
        public string GetRezervacijas()
        {
            String response = "";

            List<Knjiga> kriticne = db.Knjigas.Where(k => (db.Zaduzenjas.Where(z => z.status == "nv" && z.KnjigaID == k.ID).Count() + 
                db.Rezervacijas.Where(r => r.status == "co" && r.KnjigaID == k.ID).Count()) - k.ukupno_kopija == 0).ToList();

            foreach (Knjiga k in kriticne)
            {
                response += "Knjiga: " + k.naslov + ", ukupno kopija: " + k.ukupno_kopija + "\n";
                List<Autor> autori = k.Autori.ToList();
                string autori_s = "";
                foreach(Autor a in autori)
                    autori_s += a.naziv + ", ";

                autori_s = autori_s.Substring(0, autori_s.Length - 2);

                response += autori_s + ".\n\n";
            }

            if (kriticne.Count() < 1)
            {
                return "Nema kriticnih knjiga!";
            }

            return response;
        }
        /*
        // DELETE api/Rezervacija/5
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult DeleteRezervacija(long id)
        {
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            db.Rezervacijas.Remove(rezervacija);
            db.SaveChanges();

            return Ok(rezervacija);
        }
        */
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
            string pass = "";

            MailMessage email = new MailMessage();
            email.From = new MailAddress(from);

            foreach(String m in mailsTo)
                email.To.Add(m);

            email.Subject = "Obavještenje";
            email.IsBodyHtml = true;

            if(tip_maila == "no")
            {
                email.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>Obavještavamo Vas da je Vaša rezervacija knjige \"" + naziv_knjige + "\" istekla. Ukoliko želite zadužiti ovu knjigu molimo Vas da je ponovo rezervišete!" +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";

            }else if(tip_maila == "vr")
            {
                email.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>Obavještavamo Vas da je istekao rok za vračanje knjige \"" + naziv_knjige + "\" koju ste zadužili, pa Vas molimo da, što je prije moguće vratite istu, kako bi i drugi čitaoci mogli doći do knjige." +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";

            }else if (tip_maila == "co")
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
