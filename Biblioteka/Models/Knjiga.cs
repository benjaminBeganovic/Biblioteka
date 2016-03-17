using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    public class Knjiga
    {
        public long ID { get; set; }
        
        [ForeignKey("Izdavac")]
        public long IzdavacID { get; set; }
        
        [ForeignKey("TipKnjige")]
        public long TipKnjigeID { get; set; }
        
        [ForeignKey("Jezik")]
        public long JezikID { get; set; }

        public string naslov { get; set; }
        public string isbn { get; set; }
        public int broj_strana { get; set; }
        public DateTime godina_izdavanja { get; set; }
        public int ukupno_kopija { get; set; }
        public int dostupno_kopija { get; set; }
        public int izdanje { get; set; }
        public string opis { get; set; }
        public bool izbrisano { get; set; }

        public virtual Izdavac Izdavac { get; set; }
        public virtual TipKnjige TipKnjige { get; set; }
        public virtual Jezik Jezik { get; set; }
        public virtual ICollection<Autor> Autori { get; set; }
    }
}