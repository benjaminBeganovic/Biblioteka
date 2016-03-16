using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class KnjigaAutor
    {
        [Key]
        [ForeignKey("Knjiga")]
        public long KnjigaID { get; set; }
        [Key]
        [ForeignKey("Autor")]
        public long AutorID { get; set; }

        public virtual Knjiga Knjiga { get; set; }
        public virtual Autor Autor { get; set; }
    }
}