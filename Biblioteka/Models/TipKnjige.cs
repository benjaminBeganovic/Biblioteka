﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    public class TipKnjige
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }
        [Required(ErrorMessage = "Referenca je obavezna")]
        [StringLength(45)]
        public string referenca { get; set; }

        [Range(0, 45, ErrorMessage = "Opis moze imati do 45 karaktera.")]
        public string opis { get; set; }
    }
}