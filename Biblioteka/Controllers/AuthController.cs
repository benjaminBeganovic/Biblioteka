using Biblioteka.Models;
using Biblioteka.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Biblioteka.Controllers
{
    public class AuthController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        [ActionName("Login")]
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(ModelState);
            }
            Korisnik k;
            if (db.Korisniks.Any(a => a.username == username && a.password == password))
            {
                k = db.Korisniks.Where(a => a.username == username && a.password == password).First();
            }
            else
            {
                return NotFound();
            }
            SessionPersister.username = k.username;
            return Ok();
        }

        [ActionName("Logout")]
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Logout(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(ModelState);
            }
            Korisnik k;
            if (db.Korisniks.Any(a => a.username == username && a.password == password))
            {
                k = db.Korisniks.Where(a => a.username == username && a.password == password).First();
            }
            else
            {
                return NotFound();
            }
            SessionPersister.username = null;
            return Ok();
        }
    }
}
