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
using Biblioteka.Security;
using System.Web.SessionState;
using System.Net.Mail;
using System.Net.Http.Headers;
using Biblioteka.ViewModels;
using System.Data.Entity.SqlServer;

namespace Biblioteka.Controllers
{
    public class KorisniksController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Korisniks
        [CustomAuthorize(Roles = "a")]
        [ResponseType(typeof(List<Korisnik>))]
        public IHttpActionResult GetKorisnici()
        {
            var korisnici = db.Korisniks.ToList();
            return Ok(korisnici);
        }

        [ActionName("SviKorisnici")]
        [CustomAuthorize(Roles = "a")]
        [ResponseType(typeof(List<Korisnik>))]
        public IHttpActionResult GetKorisnici1()
        {
            var korisnici = db.Korisniks.ToList();
            return Ok(korisnici);
        }
        // GET: api/Korisniks/5
        [CustomAuthorize(Roles = "a")]
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult GetKorisnik(long id)
        {
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }

        [ActionName("Verifikuj")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Verifikuj(string kod, string username)
        {
            var response = new HttpResponseMessage();
            Korisnik korisnik = db.Korisniks.Where(a => a.username == username).First();
            if (korisnik == null)
            {
                response.Content = new StringContent("<html><body><h2>Greška</h2></body></html>");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return response;
            }
            if (korisnik.verifikacija == kod)
            {
                korisnik.odobren = true;
            }
            db.Entry(korisnik).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            response.Content = new StringContent("<html><body><h2>Uspjesno ste verifikovani!</h2></body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

		[ActionName("ResetPass")]
        [HttpPost]
        public IHttpActionResult ResetPass(string email)
        {
            Korisnik korisnik = db.Korisniks.Where(a => a.email == email).First();
            if (korisnik == null)
            {
                return NotFound();
            }

            string from = "bibliotekanwt@gmail.com";
            string pass = "bibliotekanwtpass1";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(email);

            mail.Subject = "Reset passworda";
            mail.IsBodyHtml = true;

            string noviPass = RandomString(6);
            korisnik.password = noviPass;
            db.Entry(korisnik).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            mail.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>vaš password je \"" + noviPass +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";


            SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com", 587);
            SMTPServer.Credentials = new System.Net.NetworkCredential(from, pass);
            SMTPServer.EnableSsl = true;

            try
            {
                SMTPServer.Send(mail);
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
            return Ok();
        }
		
        // PUT: api/Korisniks/5
        [CustomAuthorize(Roles = "c")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKorisnik(long id, Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.ID)
            {
                return BadRequest();
            }

            Korisnik k = db.Korisniks.Find(korisnik.ID);
            if(k == null)
            {
                return BadRequest("Nepostojeci Id");
            }

            if(korisnik.verifikacija != k.verifikacija)
            {
                return BadRequest("Pogresan kod!");
            }

            k.odobren = true;
            db.Entry(k).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
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

        [CustomAuthorize(Roles = "a")]
        [ActionName("BanKorisnik")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult BanKorisnik(long id)
        {
            Korisnik k = db.Korisniks.Where(a => a.ID == id).First();
            if (k == null)
            {
                return NotFound();
            }
            k.izbrisan = !k.izbrisan;
            db.Entry(k).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok(k);
        }

        [CustomAuthorize(Roles = "a")]
        [ActionName("ChangeRole")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ChangeRole(long id, string role)
        {
            Korisnik k = db.Korisniks.Where(a => a.ID == id).First();
            if (k == null || db.TipRacunas.Where(a => a.referenca == role).Count() == 0)
            {
                return NotFound();
            }
            k.TipRacuna = db.TipRacunas.Where(a => a.referenca == role).First();
            db.Entry(k).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok(k);
        }

        [CustomAuthorize(Roles = "a")]
        [ActionName("Aktivnost")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<KorisnikAktivnost>))]
        public IHttpActionResult GetAktivnost(int count)
        {
            List<KorisnikAktivnost> akt = new List<KorisnikAktivnost>();
            var korisnici = db.Korisniks.Where(a => a.Zaduzenja.Count() > 0 || a.Rezervacije.Count() > 0).ToList();
            foreach (var k in korisnici)
            {
                akt.Add(new KorisnikAktivnost() { username = k.username, brojRezervacija = k.Rezervacije.Count(), brojZaduzenja = k.Zaduzenja.Count() });
            }
            akt.OrderByDescending(a => a.brojRezervacija + a.brojZaduzenja);
            if (akt.Count() > count)
            {
                return Ok(akt.GetRange(0, count));
            }
            else
            {
                return Ok(akt);
            }
            
        }

        [CustomAuthorize(Roles = "a")]
        [ActionName("LoginLogs")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<int>))]
        public IHttpActionResult GetLoginLogs()
        {
            var numofday = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                numofday.Add(db.LoginLogs.Where(a => a.dan == i).Count());
            }
            return Ok(numofday);
        }
        // POST: api/Korisniks
        [CustomAuthorize(Roles = "a")]
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult PostKorisnik(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Korisnik> k = db.Korisniks.Where(c => c.username == korisnik.username).ToList();

            if(k.Count > 0)
            {
                return Ok("Izaberite drugi username!");
            }

            korisnik.TipRacunaID = 1;
            korisnik.izbrisan = false;
            korisnik.odobren = false;
            String kod = RandomString(6);
            korisnik.verifikacija = kod;

            List<string> listaKorisnika = new List<string>();
            listaKorisnika.Add(korisnik.email);
            kod = "http://nwtbiblioteka1.azurewebsites.net/api/Korisniks/Verifikuj?kod=" + kod + "&username=" + korisnik.username;
            sendEmailTimeIsUp(listaKorisnika, kod, "");

            db.Korisniks.Add(korisnik);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = korisnik.ID }, korisnik);
        }

        // DELETE: api/Korisniks/5
        [CustomAuthorize(Roles = "a")]
        [ResponseType(typeof(Korisnik))]
        public IHttpActionResult DeleteKorisnik(long id)
        {
            Korisnik korisnik = db.Korisniks.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            db.Korisniks.Remove(korisnik);
            db.SaveChanges();

            return Ok(korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisnikExists(long id)
        {
            return db.Korisniks.Count(e => e.ID == id) > 0;
        }

        [HttpPost]
        private void sendEmailTimeIsUp(List<string> mailsTo, string kod, string tip_maila)
        {
            string from = "bibliotekanwt@gmail.com";
            string pass = "bibliotekanwtpass1";

            MailMessage email = new MailMessage();
            email.From = new MailAddress(from);

            foreach (String m in mailsTo)
                email.To.Add(m);

            email.Subject = "Verifikacija";
            email.IsBodyHtml = true;

            email.Body = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td style='padding: 10px 10px 10px 10px;'>" +
                "Poštovanje, <br><br>kliknite na sljedeći link da potvrdite registraciju: \"" + kod +
                "<p>Lijep pozdrav.</p></td></tr><tr><td style='padding: 10px 10px 10px 10px;'>Vaša NWT Biblioteka!</td></tr></table>";


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
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}