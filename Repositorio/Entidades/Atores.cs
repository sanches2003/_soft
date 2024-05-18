using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Atores
    {
        public Atores()
        {
            this.atores_filme = new HashSet<AtoresFilme>();
        }

        public int id { get; set; }

        public String nomeAtor { get; set; }

        //Criar a lista de filme respectiva ao atores:
        public virtual ICollection<AtoresFilme> atores_filme { get; set; }

        
    }
}
