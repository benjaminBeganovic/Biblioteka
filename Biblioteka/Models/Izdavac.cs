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

        [StringLength(45, ErrorMessage = "Naziv moze imati do 45 karaktera.")]
        public string naziv { get; set; }

        [StringLength(100, ErrorMessage = "Naziv moze imati do 100 karaktera.")]
        public string adresa { get; set; }

        public virtual ICollection<Knjiga> Knjige { get; set; }
    }
}