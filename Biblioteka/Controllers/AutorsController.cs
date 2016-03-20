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

namespace Biblioteka.Controllers
{
    public class AutorsController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Autors
        public IQueryable<Autor> GetAutori()
        {
            return db.Autori;
        }

        // GET: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult GetAutor(long id)
        {
            Autor autor = db.Autori.Find(id);
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

            db.Autori.Add(autor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autor.ID }, autor);
        }

        // DELETE: api/Autors/5
        [ResponseType(typeof(Autor))]
        public IHttpActionResult DeleteAutor(long id)
        {
            Autor autor = db.Autori.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            db.Autori.Remove(autor);
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
            return db.Autori.Count(e => e.ID == id) > 0;
        }
    }
}