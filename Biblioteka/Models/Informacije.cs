using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Informacije
    {
        [ScaffoldColumn(false)]
        [Display(Name ="ID")]
        public long ID { get; set; }

        [Required]
        [Display(Name = "Key")]
        public string key { get; set; }
    
        [Required]
        [Display(Name ="Value")]
        public string value { get; set; }

        [Required]
        [Display(Name = "Binary")]
        public Byte [] binary { get; set; }


    }
}