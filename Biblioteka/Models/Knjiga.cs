using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteka.Models
{
    public class Knjiga
    {
        public long ID { get; set; }
        [Key]
        [ForeignKey("Izdavac")]
        public long IzdavacID { get; set; }
        [Key]
        [ForeignKey("TipKnjige")]
        public long TipKnjigeID { get; set; }
        [Key]
        [ForeignKey("Jezik")]
        public long JezikID { get; set; }
    }
}