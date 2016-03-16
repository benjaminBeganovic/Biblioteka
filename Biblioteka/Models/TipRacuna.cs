using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class TipRacuna
    {
        public long ID { get; set; }
        [Key]
        public string referenca { get; set; }
        public string opis { get; set; }
       
    }
}