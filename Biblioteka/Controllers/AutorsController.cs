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

namespace Biblioteka.Controllers
{
    [CustomAuthorize(Roles = "a,b")]
    public class AutorsController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Autors
        [ResponseType(typeof(List<Autor>))]
        public IHttpActionResult GetAutori()
        {
            return Ok(db.Autors.ToList());
        }

        [CustomAuthorize(Roles = "a")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<KeyValuePair<string,int>>))]
        [ActionName("PopularniAutori")]
        public IHttpActionResult GetKnjige(int size)
        {
            var autori = new Dictionary<string, int>();
            var a = db.Autors.ToList();
            foreach (var x in a)
            {
                try
                {
                    int knjiga = db.Knjigas.Where(i => i.Autori.Select(v => v.naziv).Contains(x.naziv)).Count();
                    autori.Add(x.naziv, knjiga);
                }
                catch (Exception)
                {
                    
                }
                
            }
            var rez = autori.OrderByDescending(z => z.Value).ToList();
            if (rez.Count < size)
            {
                return Ok(rez);
            }
            else
            {
                return Ok(rez.GetRange(0, size));
            }
        }

        [CustomAuthorize(Roles = "a")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<KeyValuePair<string, int>>))]
        [ActionName("GlavniAutori")]
        public IHttpActionResult GetKnjige1(int size)
        {
            var autori = new Dictionary<string, int>();
            var a = db.Autors.ToList();
            foreach (var x in a)
            {
                try
                {
                    int knjiga = db.Zaduzenjas.Where(i => i.Knjiga.Autori.Select(v => v.naziv).Contains(x.naziv)).Count();
                    autori.Add(x.naziv, knjiga);
                }
                catch (Exception)
                {

                }

            }
            var rez = autori.OrderByDescending(z => z.Value).ToList();
            if (rez.Count < size)
            {
                return Ok(rez);
            }
            else
            {
                return Ok(rez.GetRange(0, size));
            }
        }

        // GET: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult GetAutor(long id)
        {
            Autor autor = db.Autors.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        // PUT: api/Autors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutor(long id, Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autor.ID)
            {
                return BadRequest();
            }

            db.Entry(autor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
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

        // POST: api/Autors
        [ResponseType(typeof(Autor))]
        public IHttpActionResult PostAutor(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autors.Add(autor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autor.ID }, autor);
        }

        // DELETE: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult DeleteAutor(long id)
        {
            Autor autor = db.Autors.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            db.Autors.Remove(autor);
            db.SaveChanges();

            return Ok(autor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutorExists(long id)
        {
            return db.Autors.Count(e => e.ID == id) > 0;
        }
    }
}