﻿using Biblioteka.Models;
using Biblioteka.Security;
using Biblioteka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace Biblioteka.Controllers
{
    public class AuthController : ApiController
    {
        private ProbaContext db = new ProbaContext();

        [ActionName("Login")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Login([FromBody] Login login)
        {
            string username = login.username;
            string password = login.password;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(ModelState);
            }
            Korisnik k;
            if (db.Korisniks.Any(a => a.username == username && a.password == password && a.odobren == true && a.izbrisan == false))
            {
                k = db.Korisniks.Where(a => a.username == username && a.password == password).First();
            }
            else
            {
                return NotFound();
            }
            SessionPersister.username = k.username;
            db.LoginLogs.Add(new LoginLog { username = k.username, vrijeme = DateTime.Now, dan = (int) DateTime.Now.DayOfWeek });
            db.SaveChanges();
            return Ok(k.TipRacuna.referenca);
        }
        
        [ActionName("Logout")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Logout()
        {
            /*if (string.IsNullOrEmpty(login.username) || string.IsNullOrEmpty(login.password))
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
            }*/
            SessionPersister.username = null;
            return Ok("dafad");
        }
    }
}
