using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Plataforma
    {
        /* 
               public Plataforma()
               {
                   this.filmes = new HashSet<Filme>();

               }
        */

        public int id { get; set; }
        public String descricao { get; set; }

        //public virtual ICollection<Filme> filmes { get; set; }
    }
}
