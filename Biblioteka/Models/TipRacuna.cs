using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class TipRacuna
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }
        [Required(ErrorMessage = "Referenca je obavezna")]
        [StringLength(20)]
        public string referenca { get; set; }
        [Required(ErrorMessage = "Referenca je obavezna")]
        [StringLength(200)]
        public string opis { get; set; }
       
    }
}