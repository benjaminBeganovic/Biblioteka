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
    public class TipKnjigesController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/TipKnjiges
        public IQueryable<TipKnjige> GetTipoviKnjiga()
        {
            return db.TipKnjiges;
        }

        // GET: api/TipKnjiges/5
        [ResponseType(typeof(TipKnjige))]
        public IHttpActionResult GetTipKnjige(long id)
        {
            TipKnjige tipKnjige = db.TipKnjiges.Find(id);
            if (tipKnjige == null)
            {
                return NotFound();
            }

            return Ok(tipKnjige);
        }

        // PUT: api/TipKnjiges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipKnjige(long id, TipKnjige tipKnjige)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipKnjige.ID)
            {
                return BadRequest();
            }

            db.Entry(tipKnjige).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipKnjigeExists(id))
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

        // POST: api/TipKnjiges
        [ResponseType(typeof(TipKnjige))]
        public IHttpActionResult PostTipKnjige(TipKnjige tipKnjige)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipKnjiges.Add(tipKnjige);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipKnjige.ID }, tipKnjige);
        }

        // DELETE: api/TipKnjiges/5
        [ResponseType(typeof(TipKnjige))]
        public IHttpActionResult DeleteTipKnjige(long id)
        {
            TipKnjige tipKnjige = db.TipKnjiges.Find(id);
            if (tipKnjige == null)
            {
                return NotFound();
            }

            db.TipKnjiges.Remove(tipKnjige);
            db.SaveChanges();

            return Ok(tipKnjige);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipKnjigeExists(long id)
        {
            return db.TipKnjiges.Count(e => e.ID == id) > 0;
        }
    }
}