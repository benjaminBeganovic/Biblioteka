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

        void prepareValue(ref string a)
        {
            if (a != null)
            {
                a = a.ToLower().Trim();
            }
        }

        [ActionName("Kod")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetPretragaKod(string kod)
        {
            return BadRequest();
            prepareValue(ref kod);
            if (kod != null)
            {
                var knjige = db.Knjigas.Where(a => a.idbroj.ToLower().Contains(kod)).ToList();
                if (knjige.Count() > 0)
                {
                    return Ok(knjige);
                }
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        [ActionName("Napredna")]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetPretragaNapredna(string naziv, string tip, string jezik, string autor, string izdavac, int godina, List<string> kljucne)
        {
            prepareValue(ref naziv);
            prepareValue(ref autor);
            prepareValue(ref izdavac);
            prepareValue(ref tip);
            prepareValue(ref jezik);
            List<Knjiga> knjige = null;
            IQueryable<Knjiga> knjiga = null;
            if (autor != null && naziv != null)
            {
                knjiga = db.Knjigas.Where(a => a.naslov.ToLower().Contains(naziv))
                .Where(t => t.TipKnjige.referenca == tip)
                .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower());
                var rez = new List<Knjiga>();
                foreach (var k in knjiga.ToList())
                {
                    if (k.Autori.Any(a => a.naziv.ToLower().Contains(autor)))
                    {
                        rez.Add(k);
                    }
                }
                knjiga = rez.AsQueryable();
            }
            else if (naziv != null)
            {
                knjiga = db.Knjigas.Where(a => a.naslov.ToLower().Contains(naziv))
                .Where(t => t.TipKnjige.referenca == tip)
                .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower());
            }
            else if (autor != null)
            {
                knjiga = db.Knjigas.Where(t => t.TipKnjige.referenca == tip)
                .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower());
                var rez = new List<Knjiga>();
                foreach (var k in knjiga.ToList())
                {
                    if (k.Autori.Any(a => a.naziv.ToLower().Contains(autor)))
                    {
                        rez.Add(k);
                    }
                }
                knjiga = rez.AsQueryable();
            }
            if (izdavac != null)
            {
                knjiga = knjiga.Where(a => a.Izdavac.naziv.ToLower().Contains(izdavac));
            }
            if (godina != 0)
            {
                knjiga = knjiga.Where(a => a.godina_izdavanja.Value.Year == godina);
            }
            if (kljucne != null)
            {
                foreach (var k in kljucne)
                {
                    if (knjiga.Any(a => a.opis.ToLower().Contains(k)))
                    {
                        knjiga = knjiga.Where(a => a.opis.ToLower().Contains(k));
                    }
                }
            }
            if (knjiga.ToList().Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(knjiga.ToList());
            }
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