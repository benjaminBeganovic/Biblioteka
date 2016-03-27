using Biblioteka.Models;
using Biblioteka.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Biblioteka.Controllers
{
    public class PretragaController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        [ActionName("Autori")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetAutori(string naziv, string autor)
        {
            List<Knjiga> knjiga = null;
            knjiga = db.Knjigas.Where(a => a.naslov.ToLower().Contains(naziv.ToLower())).ToList();
            List<Knjiga> rez = new List<Knjiga>();
            foreach (var k in knjiga)
            {
                if (k.Autori.Any(a => a.naziv.ToLower().Contains(autor)))
                {
                    rez.Add(k);
                }
            }
            return Ok(rez);
        }

        [AllowAnonymous]
        [ActionName("Jednostavna")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetPretragaJednostavna(string tipknjige, string jezik, string naziv, string autor)
        {
            List<Knjiga> knjiga = null;
            if (naziv != null)
            {
                naziv = naziv.ToLower().Trim();
            }
            if (autor != null)
            {
                autor = autor.ToLower().Trim();
            }
            if (naziv != null && autor != null)
            {
                knjiga = db.Knjigas.Where(a => a.naslov.ToLower().Contains(naziv.ToLower()))
                    .Where(t => t.TipKnjige.referenca == tipknjige)
                    .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower()).ToList();
                List<Knjiga> rez = new List<Knjiga>();
                foreach (var k in knjiga)
                {
                    if (k.Autori.Any(a => a.naziv.ToLower().Contains(autor)))
                    {
                        rez.Add(k);
                    }
                }
                knjiga = rez;
            }
            else if (naziv != null)
            {
                knjiga = db.Knjigas.Where(t => t.TipKnjige.referenca == tipknjige)
                    .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower())
                    .Where(n => n.naslov.ToLower().Contains(naziv)).ToList();
            }
            else if (autor != null)
            {
                knjiga = db.Knjigas.Where(t => t.TipKnjige.referenca == tipknjige)
                    .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower()).ToList();
                List<Knjiga> rez = new List<Knjiga>();
                foreach (var k in knjiga)
                {
                    if (k.Autori.Any(a => a.naziv.ToLower().Contains(autor)))
                    {
                        rez.Add(k);
                    }
                }
                knjiga = rez;
            }
            
            if (knjiga == null)
            {
                return NotFound();
            }

            return Ok(knjiga);
        }
    }
}