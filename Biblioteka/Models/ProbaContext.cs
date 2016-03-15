using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class ProbaContext : DbContext
    {
        public ProbaContext() : base("ProbaContext")
        {
        }
        public DbSet<Proba> Products { get; set; }
    }
}