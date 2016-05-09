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
    [CustomAuthorize(Roles = "a,b")]
    public class KnjigasController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Knjigas
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetKnjigas()
        {
            var knjige = db.Knjigas.ToList();
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
        [ActionName("Paging")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetKnjige(int page, int step)
        {
            List<Knjiga> knjiga = db.Knjigas.ToList();
            return Ok(knjiga.ToPagedList(page, step));

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
        [ResponseType(typeof(Knjiga))]
        public IHttpActionResult PostKnjiga(Knjiga knjiga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            db.Knjigas.Add(knjiga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = knjiga.ID }, knjiga);
        }

        // DELETE: api/Knjigas/5
        [ResponseType(typeof(Knjiga))]
        public IHttpActionResult DeleteKnjiga(long id)
        {
            Knjiga knjiga = db.Knjigas.Find(id);
            if (knjiga == null)
            {
                return NotFound();
            }

            db.Knjigas.Remove(knjiga);
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