using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class ProbaDatabaseInitialiser: DropCreateDatabaseIfModelChanges<ProbaContext>
    {
        protected override void Seed(ProbaContext context)
        {
            GetProbas().ForEach(p => context.Products.Add(p));
        }
        public static List<Proba> GetProbas()
        {
            var probas = new List<Proba> {
                new Proba
                {
                    ProbaId = 1
                }
            };
            return probas;
        }
    }
}