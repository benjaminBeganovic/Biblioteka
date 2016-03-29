using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Biblioteka.Models
{
    public class Jezik
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [Required(ErrorMessage = "Referenca je obavezna")]
        [StringLength(45, ErrorMessage = "Referenca moze imati do 45 karaktera.")]
        public string referenca { get; set; }

        [StringLength(10000, ErrorMessage = "Naziv moze imati do 10000 karaktera.")]
        public string opis { get; set; }

        [JsonIgnore]
        public virtual ICollection<Knjiga> Knjige { get; set; }
    }
}