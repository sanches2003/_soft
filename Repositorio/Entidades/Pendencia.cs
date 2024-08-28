using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Pendencia
    {
        public Pendencia()
        {
            this.atendimentos = new HashSet<Atendimento>();
        }


        public int id { get; set; }

        public String descricao { get; set; }

        public virtual ICollection<Atendimento> atendimentos { get; set; }

    }
}
