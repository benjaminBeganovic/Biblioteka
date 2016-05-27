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
using PagedList;

namespace Biblioteka.Controllers
{
    public class KnjigasController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Knjigas
        [CustomAuthorize(Roles = "b")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetKnjigas()
        {
            var knjige = db.Knjigas.Where(a => a.izbrisano == false).ToList();
            return Ok(knjige);
        }
        //kriticne knjige - knjige koje su sve zauzete
        // GET: api/Knjigas
        [CustomAuthorize(Roles = "b")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetKnjigas(string kk)
        {
            List<Knjiga> kriticne = db.Knjigas.Where(k => (db.Zaduzenjas.Where(z => z.status == "nv" && z.KnjigaID == k.ID).Count() +
                db.Rezervacijas.Where(r => r.status == "co" && r.KnjigaID == k.ID).Count()) - k.ukupno_kopija == 0).ToList();

            return Ok(kriticne);
        }
        // GET: api/Knjigas/parametar
        
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetKnjige(int page, int step)
        {
            List<Knjiga> knjiga = db.Knjigas.ToList();
            return Ok(knjiga.ToPagedList(page, step));

        }

        [CustomAuthorize(Roles = "a,b")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<int>))]
        public IHttpActionResult GetKnjige(string numofcat)
        {
            List<int> cat_num = new List<int>();
            List<TipKnjige> tipovi_knjiga = db.TipKnjiges.ToList();
            foreach (TipKnjige t in tipovi_knjiga)
            {
                cat_num.Add(db.Knjigas.Where(k => k.TipKnjigeID == t.ID).Count());
            }
            return Ok(cat_num);
        }

        // GET: api/Knjigas/5
        [ResponseType(typeof(Knjiga))]
        public IHttpActionResult GetKnjiga(long id)
        {
            Knjiga knjiga = db.Knjigas.Find(id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return Ok(knjiga);
        }

        // PUT: api/Knjigas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKnjiga(long id, Knjiga knjiga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != knjiga.ID)
            {
                return BadRequest();
            }

            db.Entry(knjiga).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KnjigaExists(id))
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

        // POST: api/Knjigas
        [CustomAuthorize(Roles = "a,b")]
        [ResponseType(typeof(Knjiga))]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostKnjiga(Knjiga knjiga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autori = new List<Autor>();
            foreach(string a in knjiga.autor.Split(','))
            {
                var aut = new Autor();
                aut.naziv = a;
                db.Autors.Add(aut);
                autori.Add(aut);
            }
            knjiga.Autori = autori;
            knjiga.izbrisano = false;
            db.Knjigas.Add(knjiga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = knjiga.ID }, knjiga);
        }

        [ResponseType(typeof(Knjiga))]
        [System.Web.Http.HttpPost]
        [ActionName("DeleteKnjiga")]
        public IHttpActionResult DeleteKnjiga(long id)
        {
            Knjiga knjiga = db.Knjigas.Find(id);
            if (knjiga == null)
            {
                return Ok("Nema");
            }

            knjiga.izbrisano = true;
            db.Entry(knjiga).State = EntityState.Modified;

            db.SaveChanges();

            return Ok(knjiga);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KnjigaExists(long id)
        {
            return db.Knjigas.Count(e => e.ID == id) > 0;
        }
    }
}