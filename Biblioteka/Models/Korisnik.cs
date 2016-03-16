﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Korisnik
    {
        public long ID { get; set; }
        [Key]
        [ForeignKey("TipRacuna")]
        public long TipRacunaID { get; set; }
       

        public string ime { get; set; }
        public string prezime { get; set; }
        public string telefon { get; set; }
        public string adresa { get; set; }
        public string email{ get; set; }
        public bool  izbrisan { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool odobren { get; set; }

        public virtual TipRacuna TipRacuna { get; set; }
        
    }
}