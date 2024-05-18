using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Filme
    {
        public Filme()
        {
            this.atores_filme = new HashSet<AtoresFilme>(); //ver com a prof!!!
            this.vendasingressos = new HashSet<VendaIngresso>();
            this.sessoes = new HashSet<Sessao>();
        }

        public int id { get; set; }

        public String titulo{ get; set; }

        public String descricao { get; set; }

        public DateTime inicioCartaz { get; set; }

        public DateTime finalCartaz { get; set; }

        public Decimal valorMeia { get; set; }

        public Decimal valorInteira { get; set; }

        public String duracao { get; set; }

        public bool estaDisponivel { get; set; }

        //Colocando uma imagem no nosso filme
        public String imagem { get; set; }

        //chave estrangeira:
        public int idCategoria { get; set; }
        public int idEstudio { get; set; }

        //Temporário
        public int idAtores { get; set; }



        //Instanciando a lista numa relação de UM para Muitos:
        public virtual Plataforma categoria { get; set; } // 1 filme tem 1 Categoria.
        public virtual Estudio estudio { get; set; } // 1 filme tem 1 Estudio.

        //temporário
        public virtual Atores? atores { get; set; } // 1 filme tem 1 Estudio.

        //Criar a lista de ***Atores*** respectiva ao FILMES (ver com a prof!!!)
        public virtual ICollection<AtoresFilme> atores_filme { get; set; }


        //Criar a lista de ***Sessões*** para FILMES:
        public virtual ICollection<Sessao> sessoes { get; set; }

        public virtual ICollection<VendaIngresso> vendasingressos { get; set; }

    }
}
