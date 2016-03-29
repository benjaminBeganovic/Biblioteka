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
    public class InformacijesController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Informacijes
        public IQueryable<Informacije> GetInformacijes()
        {
            return db.Informacijes;
        }

        // GET: api/Informacijes/5
        [ResponseType(typeof(Informacije))]
        public IHttpActionResult GetInformacije(long id)
        {
            Informacije informacije = db.Informacijes.Find(id);
            if (informacije == null)
            {
                return NotFound();
            }

            return Ok(informacije);
        }

        // PUT: api/Informacijes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInformacije(long id, Informacije informacije)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != informacije.ID)
            {
                return BadRequest();
            }

            db.Entry(informacije).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformacijeExists(id))
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

        // POST: api/Informacijes
        [ResponseType(typeof(Informacije))]
        public IHttpActionResult PostInformacije(Informacije informacije)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Informacijes.Add(informacije);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = informacije.ID }, informacije);
        }

        // DELETE: api/Informacijes/5
        [ResponseType(typeof(Informacije))]
        public IHttpActionResult DeleteInformacije(long id)
        {
            Informacije informacije = db.Informacijes.Find(id);
            if (informacije == null)
            {
                return NotFound();
            }

            db.Informacijes.Remove(informacije);
            db.SaveChanges();

            return Ok(informacije);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InformacijeExists(long id)
        {
            return db.Informacijes.Count(e => e.ID == id) > 0;
        }
    }
}