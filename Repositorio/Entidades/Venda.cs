using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Venda
    {
        public Venda()
        {
            this.vendasingressos = new HashSet<VendaIngresso>();
        }

        public int id { get; set; }
        public DateTime dataVenda { get; set; }
        public Decimal valor { get; set; }
        public string? tipoIngresso { get; set; }
        public int idStatus { get; set; }

        public String? idPreferencia { get; set; }
        public String? url { get; set; }


        public virtual Status status { get; set; }

        public virtual ICollection<VendaIngresso> vendasingressos { get; set; }

    }
}
