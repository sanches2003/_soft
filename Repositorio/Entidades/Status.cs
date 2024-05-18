using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Status
    {
        public Status()
        {
            this.vendas = new HashSet<Venda>();
        }
        public int id { get; set; }
        public String descricao { get; set; }

        public virtual ICollection<Venda> vendas { get; set; }
    }
}
