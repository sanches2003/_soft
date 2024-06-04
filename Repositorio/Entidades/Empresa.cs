using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{

    public class Empresa
    {

        public int id { get; set; }

        public String razaosocial { get; set; }

        public String cnpj { get; set; }

        public String telefone { get; set; }

        public String celular { get; set; }

        public String email { get; set; }

        public String cep { get; set; }

        public String logradouro { get; set; }

        public String numero { get; set; }

        public String bairro { get; set; }

        public String cidade { get; set; }

        public String estado { get; set; }

        public String complemento { get; set; }



    }
}
