using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Zaduzenja
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }

        [StringLength(20)]
        public string status { get; set; }

        [Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime datum_zaduzenja { get; set; }

        [JsonIgnore]
        [Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? datum_vracanja { get; set; }

        [Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime rok { get; set; }

        [ForeignKey("Korisnik"), Column(Order = 0)]
        public long KorisnikID { get; set; }

        [ForeignKey("Knjiga")]
        public long KnjigaID { get; set; }

        [ForeignKey("Zaposlenik"), Column(Order = 1)]
        public long ZaposlenikID { get; set; }

        public virtual Korisnik Korisnik { get; set; }
        public virtual Knjiga Knjiga { get; set; }
        [JsonIgnore]
        public virtual Korisnik Zaposlenik { get; set; }

    }
}