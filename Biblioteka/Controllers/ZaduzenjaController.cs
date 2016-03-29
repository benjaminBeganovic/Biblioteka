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

        /*
        // GET api/Zaduzenja
        [ResponseType(typeof(List<Zaduzenja>))]
        public IHttpActionResult GetZaduzenjas()
        {
            var zaduzenja = db.Zaduzenjas.ToList();
            return Ok(zaduzenja);
        }

        // GET api/Zaduzenja/5
        [ResponseType(typeof(Zaduzenja))]
        public IHttpActionResult GetZaduzenja(long id)
        {
            Zaduzenja zaduzenja = db.Zaduzenjas.Find(id);
            if (zaduzenja == null)
            {
                return NotFound();
            }

            return Ok(zaduzenja);
        }
        
        // PUT api/Zaduzenja/5
        public IHttpActionResult PutZaduzenja(long id, Zaduzenja zaduzenja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zaduzenja.ID)
            {
                return BadRequest();
            }

            db.Entry(zaduzenja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaduzenjaExists(id))
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

        // POST api/Zaduzenja
        [ResponseType(typeof(Zaduzenja))]
        public IHttpActionResult PostZaduzenja(Zaduzenja zaduzenja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Zaduzenjas.Add(zaduzenja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = zaduzenja.ID }, zaduzenja);
        }
        */


        //servis za Razduzenje
        // PUT api/Zaduzenja/5
        [CustomAuthorize(Roles = "b")]
        public IHttpActionResult PutZaduzenja(long id, Zaduzenja zaduzenja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zaduzenja.ID)
            {
                return BadRequest();
            }

            //da li je zaduzio
            if (db.Zaduzenjas.Count(z => z.KnjigaID == zaduzenja.KnjigaID && z.KorisnikID == zaduzenja.KorisnikID && z.status == "nv") < 1)
            {
                return BadRequest("Korisnik nije ni zaduzio knjigu!");
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
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaduzenjaExists(id))
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

        // servis za Zaduzenje
        // POST api/Zaduzenja
        [CustomAuthorize(Roles = "b")]
        [ResponseType(typeof(Zaduzenja))]
        public IHttpActionResult PostZaduzenja(Zaduzenja zaduzenja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //da li je vratio
            if (db.Zaduzenjas.Count(z => z.KnjigaID == zaduzenja.KnjigaID && z.KorisnikID == zaduzenja.KorisnikID && z.status == "nv") > 0)
            {
                return BadRequest("Korisnik vec posjeduje ovu knjigu!");
            }

            //da li je rezervisao
            List<Rezervacija> rez = new List<Rezervacija>();
            rez = db.Rezervacijas.Where(z => z.KnjigaID == zaduzenja.KnjigaID && z.KorisnikID == zaduzenja.KorisnikID && z.status == "co").ToList();

            if (rez.Count() < 1)
            {
                return BadRequest("Korisnik nije zaduzio knjigu!");
            }

            rez[0].status = "re";

            db.Entry(rez[0]).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }

            zaduzenja.status = "nv";
            zaduzenja.datum_vracanja = null;
            zaduzenja.datum_zaduzenja = System.DateTime.Now;

            db.Zaduzenjas.Add(zaduzenja);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = zaduzenja.ID }, zaduzenja);
        }

        // Knjige kojma je istekao rok za vracanje
        // GET api/Zaduzenja/
        [CustomAuthorize(Roles = "b")]
        [ResponseType(typeof(String))]
        public string GetZaduzenja()
        {
            String response = "";

            DateTime d = System.DateTime.Now;
            List<Zaduzenja> nisu_vratili = db.Zaduzenjas.Where(z => z.status == "nv" && (d > z.rok)).ToList();

            foreach (Zaduzenja z in nisu_vratili)
            {
                Knjiga k = db.Knjigas.Find(z.KnjigaID);
                Korisnik ko = db.Korisniks.Find(z.KorisnikID);
                response += ko.ime + " " + ko.prezime + ", " + ko.telefon + ", " + ko.email + "\n";
                response += "Knjiga:" + " " + k.naslov + "\n";
                response += "Zaduzena:" + " " + z.datum_zaduzenja.ToString("d") + " " + "Rok: " + z.rok.ToString("d") + " Kašnjenje: " + Convert.ToInt32((System.DateTime.Now - z.rok).TotalDays) + " dana.\n\n";
                sendEmailTimeIsUp(new List<string>() { z.Korisnik.email }, k.naslov, "vr");
            }

            if (nisu_vratili.Count() < 1)
            {
                return "Sve knjige su vračene u roku!";
            }

            return response;
        }

        /*
        // DELETE api/Zaduzenja/5
        [ResponseType(typeof(Zaduzenja))]
        public IHttpActionResult DeleteZaduzenja(long id)
        {
            Zaduzenja zaduzenja = db.Zaduzenjas.Find(id);
            if (zaduzenja == null)
            {
                return NotFound();
            }

            db.Zaduzenjas.Remove(zaduzenja);
            db.SaveChanges();

            return Ok(zaduzenja);
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

        private bool ZaduzenjaExists(long id)
        {
            return db.Zaduzenjas.Count(e => e.ID == id) > 0;
        }

        [HttpPost]
        private void sendEmailTimeIsUp(List<string> mailsTo, string naziv_knjige, string tip_maila)
        {
            string from = "bibliotekanwt@gmail.com";
            string pass = "";

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