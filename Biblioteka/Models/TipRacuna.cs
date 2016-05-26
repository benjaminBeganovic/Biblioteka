using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class TipRacuna
    {
        [JsonIgnore]
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [Required(ErrorMessage = "Referenca je obavezna")]
        [StringLength(20, ErrorMessage = "Referenca moze imati do 20 karaktera.")]
        public string referenca { get; set; }

        [Required(ErrorMessage = "Opis je obavezna")]
        [StringLength(200, ErrorMessage = "Opis moze imati do 200 karaktera.")]
        public string opis { get; set; }

        [JsonIgnore]
        public virtual ICollection<Korisnik> Korisnici { get; set; }

    }
}