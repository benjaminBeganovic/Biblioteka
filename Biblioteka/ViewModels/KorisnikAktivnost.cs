using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.ViewModels
{
    public class KorisnikAktivnost
    {
        public string username { get; set; }
        public int brojRezervacija { get; set; }
        public int brojZaduzenja { get; set; }
    }
}