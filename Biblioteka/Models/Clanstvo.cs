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
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [ForeignKey("Korisnik")]
        public long KorisnikID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime datum_racuna { get; set; }

        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime istek_racuna { get; set; }

        [Range(0, 1000000,ErrorMessage = "Clanski broj mora biti od 0 do 1000000")]
        public int clanski_broj { get; set; }

        public virtual Korisnik Korisnik { get; set; }
    }
}