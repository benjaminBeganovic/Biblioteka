using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Rezervacija
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [StringLength(20, ErrorMessage = "Status moze imati do 20 karaktera.")]
        public string status { get; set; }

        [Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime datum_rezervacije { get; set; }

        [Range(0, 100, ErrorMessage = "Cekanje treba biti manje od 100 dana!")]
        public int cekanje { get; set; }

        [ForeignKey("Korisnik")]
        public long KorisnikID { get; set; }

        [ForeignKey("Knjiga")]
        public long KnjigaID { get; set; }

        [JsonIgnore]
        public virtual Korisnik Korisnik { get; set; }

        public virtual Knjiga Knjiga { get; set; }
    }
}