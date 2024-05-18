using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class VendaIngresso
    {
        public int id { get; set; }
        public int qtde { get; set; }
        public Decimal valor { get; set; }
        public string? tipoIngresso { get; set; }

        public int idVenda { get; set; }
        public int idFilme { get; set; }


        public virtual Venda vendas { get; set; }
        public virtual Filme filmes { get; set; }



    }
}
