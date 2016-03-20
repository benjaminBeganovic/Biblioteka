using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Izdavac
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }

         [Range(0, 45, ErrorMessage = "Naziv moze imati do 45 karaktera.")]
        public string naziv { get; set; }
         [Range(0, 45, ErrorMessage = "Adresa moze imati do 45 karaktera.")]
        public string adresa { get; set; }
    }
}