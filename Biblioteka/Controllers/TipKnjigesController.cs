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
    public class TipKnjigesController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/TipKnjiges
        [ResponseType(typeof(List<TipKnjige>))]
        public IHttpActionResult GetTipoviKnjiga()
        {
            return Ok(db.TipKnjiges.ToList());
        }

        // GET: api/TipKnjiges/5
        [CustomAuthorize(Roles = "a,b")]
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

        [CustomAuthorize(Roles = "a")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<KeyValuePair<string, int>>))]
        [ActionName("NajpopularnijiTipovi")]
        public IHttpActionResult Jezici(int size)
        {
            var tipovi = new Dictionary<string, int>();
            var a = db.TipKnjiges.ToList();
            foreach (var x in a)
            {
                try
                {
                    int broj = db.Knjigas.Where(i => i.TipKnjige.referenca == x.referenca).Count();
                    tipovi.Add(x.opis, broj);
                }
                catch (Exception)
                {

                }

            }
            var rez = tipovi.OrderByDescending(z => z.Value).ToList();
            if (rez.Count < size)
            {
                return Ok(rez);
            }
            else
            {
                return Ok(rez.GetRange(0, size));
            }
        }

        // PUT: api/TipKnjiges/5
        [CustomAuthorize(Roles = "a,b")]
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
        [CustomAuthorize(Roles = "a,b")]
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
        [CustomAuthorize(Roles = "a,b")]
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