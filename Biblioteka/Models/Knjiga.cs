using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    public class Knjiga
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [ForeignKey("Izdavac")]
        public long IzdavacID { get; set; }

        [ForeignKey("TipKnjige")]
        public long TipKnjigeID { get; set; }

        [ForeignKey("Jezik")]
        public long JezikID { get; set; }

        [StringLength(45, ErrorMessage = "Naslov moze imati do 45 karaktera.")]
        public string naslov { get; set; }

        [StringLength(45, ErrorMessage = "ISBN moze imati do 45 karaktera.")]
        public string isbn { get; set; }

        public string autor { get; set; }

        [StringLength(45, ErrorMessage = "Idbroj moze imati do 45 karaktera.")]
        public string idbroj { get; set; }

        [Range(0, 10000, ErrorMessage = "Knjiga ne moze imati vise od 10000 strana!")]
        public int broj_strana { get; set; }

        [Range(0, 1000000, ErrorMessage = "Godina ne moze biti negativan ili veci od 1000000!")]
        public int godina_izdavanja { get; set; }

        [Range(0, 1000000, ErrorMessage = "Broj kopija ne moze biti negativan ili veci od 1000000!")]
        public int ukupno_kopija { get; set; }

        [Range(0, 1000000, ErrorMessage = "Broj kopija ne moze biti negativan ili veci od 1000000!")]
        public int dostupno_kopija { get; set; }

        [Range(0, 1000000, ErrorMessage = "Izdanje ne moze biti negativan broj ili veci od 1000000!")]
        public int izdanje { get; set; }

        [StringLength(10000, ErrorMessage = "Opis knjige ne moze imati vise od 10000 karaktera.")]
        public string opis { get; set; }

        public bool izbrisano { get; set; }


        public virtual Izdavac Izdavac { get; set; }
        public virtual TipKnjige TipKnjige { get; set; }
        public virtual Jezik Jezik { get; set; }

        [JsonIgnore]
        public virtual ICollection<Zaduzenja> Zaduzenja { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
        public virtual ICollection<Autor> Autori { get; set; }
    }
}