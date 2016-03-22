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
    public class ZaduzenjaController : ApiController
    {
        private ProbaContext db = new ProbaContext();

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
    }
}