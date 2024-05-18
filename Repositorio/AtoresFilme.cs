using Repositorio.Contexto;
using Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class AtoresFilmeRepositorio : BaseRepositorio<AtoresFilme> // criar entidade!!!
    {
        public AtoresFilmeRepositorio(CinemaxContexto contexto)
            : base(contexto) { }
    }
}
