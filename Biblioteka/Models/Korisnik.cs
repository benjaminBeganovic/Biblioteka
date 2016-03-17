using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Korisnik
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [ForeignKey("TipRacuna")]
        public long TipRacunaID { get; set; }

        [StringLength(20)]
        public string ime { get; set; }

        [StringLength(20)]
        public string prezime { get; set; }

        [StringLength(20)]
        public string telefon { get; set; }

        [StringLength(50)]
        public string adresa { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string email { get; set; }

        public bool izbrisan { get; set; }

        [StringLength(20)]
        public string username { get; set; }

        [StringLength(20)]
        public string password { get; set; }

        public bool odobren { get; set; }

        public virtual TipRacuna TipRacuna { get; set; }
        public virtual ICollection<Zaduzenja> Zaduzenja {get; set;}
        public virtual ICollection<Zaduzenja> ZaduzenjaZaposlenik { get; set; }

    }
}