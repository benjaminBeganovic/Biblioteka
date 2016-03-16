using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Clanstvo
    {
        public long ID { get; set; }
        [ForeignKey("Korisnik")]
        public long KorisnikID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime datum_racuna { get; set; }
        public DateTime istek_racuna { get; set; }
        public int clanski_broj { get; set; }

        public virtual Korisnik Korisnik { get; set; }
    }
}