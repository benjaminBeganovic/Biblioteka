using Newtonsoft.Json;
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

        [StringLength(20, ErrorMessage = "Ime moze imati do 20 karaktera.")]
        public string ime { get; set; }

        [StringLength(20, ErrorMessage = "Prezime moze imati do 20 karaktera.")]
        public string prezime { get; set; }

        [StringLength(20, ErrorMessage = "Broj telefona moze imati do 20 karaktera.")]
        public string telefon { get; set; }

        [StringLength(50, ErrorMessage = "Adresa moze imati do 50 karaktera.")]
        public string adresa { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string email { get; set; }

        public bool izbrisan { get; set; }

        [StringLength(20, ErrorMessage = "Username moze imati do 20 karaktera.")]
        public string username { get; set; }

        [StringLength(20, ErrorMessage = "Password moze imati do 20 karaktera.")]
        public string password { get; set; }

        public string verifikacija { get; set; }

        public bool odobren { get; set; }


        public virtual TipRacuna TipRacuna { get; set; }

        [JsonIgnore]
        public virtual ICollection<Zaduzenja> Zaduzenja { get; set; }
        [JsonIgnore]
        public virtual ICollection<Zaduzenja> ZaduzenjaZaposlenik { get; set; }
        [JsonIgnore]
        public virtual ICollection<Clanstvo> Clanstva { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rezervacija> Rezervacije { get; set; }

    }
}