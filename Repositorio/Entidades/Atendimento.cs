using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Atendimento
    {
        public int id { get; set; }
        public String Contato { get; set; }
        public DateTime DataHora { get; set; }
        public String Descricao { get; set; }
        public String Anexo { get; set; }
        public String DescricaoSolucao { get; set; }
        public String Telefone { get; set; }
        public String FormaAtendimento { get; set; }
        public String Pendencia { get; set; }

        //FK:
        public int idCategoriaProblema { get; set; }
        public int idEmpresa { get; set; }
        public int idPlataforma { get; set; }
        public int idStatus { get; set; }
        public int idUsuario { get; set; }
        public int idPendencia { get; set; }


        //public int idTempo { get; set; }  -- Relógio

        //relacionamento
        public virtual CategoriaProblema categoriaproblema { get; set; }
        public virtual Empresa empresa { get; set; }
        public virtual Plataforma plataforma { get; set; }
        public virtual Status status { get; set; }
        public virtual Login login { get; set; }
        public virtual Pendencia pendencia { get; set; }

    }
}
