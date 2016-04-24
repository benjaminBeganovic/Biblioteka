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
    public class JeziksController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Jeziks
        [ResponseType(typeof(List<Jezik>))]
        public IHttpActionResult GetJezici()
        {
            return Ok(db.Jeziks.ToList());
        }

        // GET: api/Jeziks/5
        [CustomAuthorize(Roles = "a,b")]
        [ResponseType(typeof(Jezik))]
        public IHttpActionResult GetJezik(long id)
        {
            Jezik jezik = db.Jeziks.Find(id);
            if (jezik == null)
            {
                return NotFound();
            }

            return Ok(jezik);
        }

        // PUT: api/Jeziks/5
        [CustomAuthorize(Roles = "a,b")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJezik(long id, Jezik jezik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jezik.ID)
            {
                return BadRequest();
            }

            db.Entry(jezik).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JezikExists(id))
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

        // POST: api/Jeziks
        [CustomAuthorize(Roles = "a,b")]
        [ResponseType(typeof(Jezik))]
        public IHttpActionResult PostJezik(Jezik jezik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jeziks.Add(jezik);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jezik.ID }, jezik);
        }

        // DELETE: api/Jeziks/5
        [CustomAuthorize(Roles = "a,b")]
        [ResponseType(typeof(Jezik))]
        public IHttpActionResult DeleteJezik(long id)
        {
            Jezik jezik = db.Jeziks.Find(id);
            if (jezik == null)
            {
                return NotFound();
            }

            db.Jeziks.Remove(jezik);
            db.SaveChanges();

            return Ok(jezik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JezikExists(long id)
        {
            return db.Jeziks.Count(e => e.ID == id) > 0;
        }
    }
}