using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Estudio
    {
        public Estudio()
        {
            this.filmes = new HashSet<Filme>();
        }

        public int id { get; set; }
        public String nomeEstudio { get; set; }

        public virtual ICollection<Filme> filmes { get; set; }
    }
}
