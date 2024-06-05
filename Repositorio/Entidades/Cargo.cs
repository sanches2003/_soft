using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Cargo
    {
        public Cargo()
        {
            this.usuarios = new HashSet<Login>();

        }

        public int id { get; set; }
        public String descricao { get; set; }

        public virtual ICollection<Login> usuarios { get; set; }

    }
}
