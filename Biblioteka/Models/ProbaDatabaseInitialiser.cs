using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Biblioteka.Models
{
    public class ProbaDatabaseInitialiser : DropCreateDatabaseAlways<ProbaContext>
    {
        protected override void Seed(ProbaContext context)
        {
            
            NapraviAutore().ForEach(c => context.Autors.Add(c));
            //context.SaveChanges();
        }

        private static List<Autor> NapraviAutore()
        {
            List<Autor> autori = new List<Autor> {
                new Autor
                {
                    ID = 3,
                    naziv = "Mesa Selimovic"
                },
                new Autor
                {
                    ID = 4,
                    naziv = "Perl Bak"
                }
            };

            return autori;
        }


    }
}