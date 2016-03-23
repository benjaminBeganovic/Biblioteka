using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Biblioteka.Models
{
    public class ProbaDatabaseInitialiser : DropCreateDatabaseIfModelChanges<ProbaContext>
    {
        /*
        public override void InitializeDatabase(ProbaContext context)
        {
            base.InitializeDatabase(context);
        }
        protected override void Seed(ProbaContext context)
        {
            context.Autors.Add(new Autor { ID = 1, naziv = "zEKO" });
            context.SaveChanges();
            //NapraviAutore().ForEach(c => context.Autors.Add(c));
            //context.SaveChanges();
        }

        private static List<Autor> NapraviAutore()
        {
            var autori = new List<Autor> {
                new Autor
                {
                    ID = 1,
                    naziv = "Zeko peko"
                }
            };

            return autori;
        }

        */
    }
         
}