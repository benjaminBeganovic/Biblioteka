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
    public class RezervacijaController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET api/Rezervacija
        public IQueryable<Rezervacija> GetRezervacije()
        {
            return db.Rezervacije;
        }

        // GET api/Rezervacija/5
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult GetRezervacija(long id)
        {
            Rezervacija rezervacija = db.Rezervacije.Find(id);
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

        // POST api/Rezervacija
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult PostRezervacija(Rezervacija rezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rezervacije.Add(rezervacija);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rezervacija.ID }, rezervacija);
        }

        // DELETE api/Rezervacija/5
        [ResponseType(typeof(Rezervacija))]
        public IHttpActionResult DeleteRezervacija(long id)
        {
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            db.Rezervacije.Remove(rezervacija);
            db.SaveChanges();

            return Ok(rezervacija);
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
            return db.Rezervacije.Count(e => e.ID == id) > 0;
        }
    }
}