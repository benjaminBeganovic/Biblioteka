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
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetPretragaKod(string kod)
        {
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

        private bool puna(string v)
        {
            if (v == "" || v == null)
            {
                return false;
            }
            return true;
        }

        [ActionName("Napredna")]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(List<Knjiga>))]
        public IHttpActionResult GetPretragaNapredna(string kljucne="", string naziv = "", string tip = "", string jezik ="", string autor = "", string izdavac = "", int? godina = 0)
        {
            prepareValue(ref naziv);
            prepareValue(ref autor);
            prepareValue(ref izdavac);
            prepareValue(ref tip);
            prepareValue(ref jezik);
            prepareValue(ref kljucne);
            List<Knjiga> knjige = null;
            IQueryable<Knjiga> knjiga = null;
            if (puna(autor) && puna(naziv))
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
            else if (puna(naziv))
            {
                knjiga = db.Knjigas.Where(a => a.naslov.ToLower().Contains(naziv))
                .Where(t => t.TipKnjige.referenca == tip)
                .Where(j => j.Jezik.referenca.ToLower() == jezik.ToLower());
            }
            else if (puna(autor))
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
            if (puna(izdavac))
            {
                knjiga = knjiga.Where(a => a.Izdavac.naziv.ToLower().Contains(izdavac));
            }
            if (godina != 0)
            {
                knjiga = knjiga.Where(a => a.godina_izdavanja.Value.Year == godina);
            }
            if (puna(kljucne))
            {
                String[] klj = kljucne.Split(',');
                knjiga = knjiga.Where(a => klj.Any(x => a.opis.ToLower().Contains(x.ToLower()) 
                || a.naslov.ToLower().Contains(x.ToLower())));
                
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
        
        [ActionName("Jednostavna")]
        [System.Web.Http.HttpGet]
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