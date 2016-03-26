using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Biblioteka.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private ProbaContext db = new ProbaContext();
        private Korisnik korisnik;

        public CustomPrincipal(string username)
        {
            korisnik = db.Korisniks.Where(a => a.username == username).First();
            this.Identity = new GenericIdentity(username);
        }
         
        public IIdentity Identity
        {
            get; set;
        }

        public bool IsInRole(string role)
        {
            string[] roles = role.Split(',');
            return roles.Any(r => korisnik.TipRacuna.referenca.ToLower() == r.ToLower());
        }
    }
}