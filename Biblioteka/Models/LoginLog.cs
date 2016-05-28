using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class LoginLog
    {
        [ScaffoldColumn(false)]
        public long ID { get; set; }
        public string username { get; set; }
        public DateTime vrijeme { get; set; }
        public int dan { get; set; }
    }
}