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
    public class IzdavacsController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Izdavacs
        public IQueryable<Izdavac> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Izdavacs/5
        [ResponseType(typeof(Izdavac))]
        public IHttpActionResult GetIzdavac(long id)
        {
            Izdavac izdavac = db.Products.Find(id);
            if (izdavac == null)
            {
                return NotFound();
            }

            return Ok(izdavac);
        }

        // PUT: api/Izdavacs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIzdavac(long id, Izdavac izdavac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != izdavac.ID)
            {
                return BadRequest();
            }

            db.Entry(izdavac).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IzdavacExists(id))
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

        // POST: api/Izdavacs
        [ResponseType(typeof(Izdavac))]
        public IHttpActionResult PostIzdavac(Izdavac izdavac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(izdavac);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = izdavac.ID }, izdavac);
        }

        // DELETE: api/Izdavacs/5
        [ResponseType(typeof(Izdavac))]
        public IHttpActionResult DeleteIzdavac(long id)
        {
            Izdavac izdavac = db.Products.Find(id);
            if (izdavac == null)
            {
                return NotFound();
            }

            db.Products.Remove(izdavac);
            db.SaveChanges();

            return Ok(izdavac);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IzdavacExists(long id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}