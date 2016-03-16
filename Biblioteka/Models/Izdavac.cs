using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Izdavac
    {
        public long ID { get; set; }
        
        public string naziv { get; set; }
        public string adresa { get; set; }
    }
}