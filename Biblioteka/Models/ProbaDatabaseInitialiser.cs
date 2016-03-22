using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Biblioteka.Models
{
    public class ProbaDatabaseInitialiser : DropCreateDatabaseAlways<ProbaContext>
    {
        protected override void Seed(ProbaContext context)
        {
            
            NapraviAutore().ForEach(c => context.Autori.Add(c));
            //context.SaveChanges();
        }

        private static List<Autor> NapraviAutore()
        {
            var autori = new List<Autor> {
                new Autor
                {
                    ID = 1,
                    naziv = "Mesa Selimovic"
                },
                new Autor
                {
                    ID = 2,
                    naziv = "Perl Bak"
                }
            };

            return autori;
        }


    }
}