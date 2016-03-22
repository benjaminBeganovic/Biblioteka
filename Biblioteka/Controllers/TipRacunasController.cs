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
    public class TipRacunasController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/TipRacunas
        public IQueryable<TipRacuna> GetTipoviRacuna()
        {
            return db.TipRacunas;
        }

        // GET: api/TipRacunas/5
        [ResponseType(typeof(TipRacuna))]
        public IHttpActionResult GetTipRacuna(long id)
        {
            TipRacuna tipRacuna = db.TipRacunas.Find(id);
            if (tipRacuna == null)
            {
                return NotFound();
            }

            return Ok(tipRacuna);
        }

        // PUT: api/TipRacunas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipRacuna(long id, TipRacuna tipRacuna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipRacuna.ID)
            {
                return BadRequest();
            }

            db.Entry(tipRacuna).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipRacunaExists(id))
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

        // POST: api/TipRacunas
        [ResponseType(typeof(TipRacuna))]
        public IHttpActionResult PostTipRacuna(TipRacuna tipRacuna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipRacunas.Add(tipRacuna);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipRacuna.ID }, tipRacuna);
        }

        // DELETE: api/TipRacunas/5
        [ResponseType(typeof(TipRacuna))]
        public IHttpActionResult DeleteTipRacuna(long id)
        {
            TipRacuna tipRacuna = db.TipRacunas.Find(id);
            if (tipRacuna == null)
            {
                return NotFound();
            }

            db.TipRacunas.Remove(tipRacuna);
            db.SaveChanges();

            return Ok(tipRacuna);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipRacunaExists(long id)
        {
            return db.TipRacunas.Count(e => e.ID == id) > 0;
        }
    }
}