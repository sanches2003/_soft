using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{

   /*Um filme será vinculado a várias Sessoes. Cada Sessão será vinculada a só
   1 filme.*/

    public class Sessao
    {
        public int id { get; set; }

        public DateTime diaSessao { get; set; }

        public DateTime horaSessao { get; set; }

        public int sala { get; set; }

        public String local { get; set; }

        //Chave Estrangeira:

        public int idFilme { get; set; }

        //instanciando a lista
        public virtual Filme filmes { get; set; } // 1 sessão tem 1 filme.

    }
}
