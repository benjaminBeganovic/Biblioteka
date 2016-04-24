using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}