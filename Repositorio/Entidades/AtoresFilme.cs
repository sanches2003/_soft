using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class AtoresFilme //ver com a prof!!!
    {
        //"Chaves Primárias":
        public int id { get; set; }
        public int idAtores { get; set; }
        public int idFilme { get; set; }


    
        public virtual Atores ator { get; set; } //Mostra os atores
        public virtual Filme Filme { get; set; } //Mostra os filmes


    }
}
