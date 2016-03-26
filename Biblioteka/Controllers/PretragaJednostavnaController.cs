using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Biblioteka.Controllers
{
    public class PretragaJednostavnaController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        // GET: api/Knjigas/PretragaJednostavna
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetPretragaJednostavna(string tipknjige, string jezik, string naziv, string autor)
        {
            List<Knjiga> knjiga = null;
            if (naziv != "")
            {
                knjiga = db.Knjigas.Where(t => t.TipKnjige.referenca == tipknjige)
                    .Where(j => j.Jezik.referenca == jezik)
                    .Where(n => n.naslov.ToLower().Contains(naziv.ToLower())).ToList();
            }
            
            if (knjiga == null)
            {
                return NotFound();
            }

            return Ok(knjiga);
        }
    }
}