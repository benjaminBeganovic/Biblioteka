using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class TipClanstvo
    {
        public long ID { get; set; }
        [Key]
        [ForeignKey("Korisnik")]
        public long KorisnikID { get; set; }
        public DateTime datum_racuna { get; set; }
        public DateTime istek_racuna { get; set; }
        public int clanski_broj{ get; set; }
    }
}