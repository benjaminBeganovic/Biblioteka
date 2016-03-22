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
    public class ClanstvoesController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Clanstvoes
        [ResponseType(typeof(List<Clanstvo>))]
        public IHttpActionResult GetClanstvoes()
        {
            var clanstva = db.Clanstvoes.ToList();
            return Ok(clanstva);
        }

        // GET: api/Clanstvoes/5
        [ResponseType(typeof(Clanstvo))]
        public IHttpActionResult GetClanstvo(long id)
        {
            Clanstvo clanstvo = db.Clanstvoes.Find(id);
            if (clanstvo == null)
            {
                return NotFound();
            }

            return Ok(clanstvo);
        }

        // PUT: api/Clanstvoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClanstvo(long id, Clanstvo clanstvo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clanstvo.ID)
            {
                return BadRequest();
            }

            db.Entry(clanstvo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClanstvoExists(id))
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

        // POST: api/Clanstvoes
        [ResponseType(typeof(Clanstvo))]
        public IHttpActionResult PostClanstvo(Clanstvo clanstvo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clanstvoes.Add(clanstvo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clanstvo.ID }, clanstvo);
        }

        // DELETE: api/Clanstvoes/5
        [ResponseType(typeof(Clanstvo))]
        public IHttpActionResult DeleteClanstvo(long id)
        {
            Clanstvo clanstvo = db.Clanstvoes.Find(id);
            if (clanstvo == null)
            {
                return NotFound();
            }

            db.Clanstvoes.Remove(clanstvo);
            db.SaveChanges();

            return Ok(clanstvo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClanstvoExists(long id)
        {
            return db.Clanstvoes.Count(e => e.ID == id) > 0;
        }
    }
}