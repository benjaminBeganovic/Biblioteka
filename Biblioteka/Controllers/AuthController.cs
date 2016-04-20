using Biblioteka.Models;
using Biblioteka.Security;
using Biblioteka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Biblioteka.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        [ActionName("Login")]
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Login([FromBody] Login login)
        {
            string username = login.username;
            string password = login.password;
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
        public IHttpActionResult Logout([FromBody] Login login)
        {
            if (string.IsNullOrEmpty(login.username) || string.IsNullOrEmpty(login.password))
            {
                return BadRequest(ModelState);
            }
            Korisnik k;
            if (db.Korisniks.Any(a => a.username == login.username && a.password == login.password))
            {
                k = db.Korisniks.Where(a => a.username == login.username && a.password == login.password).First();
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
