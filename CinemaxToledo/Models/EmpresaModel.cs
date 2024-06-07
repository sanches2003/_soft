//using AspNetCore;
using AutoMapper;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace CinemaxToledo.Models
{
    public class EmpresaModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }


        [MaxLength(50, ErrorMessage = "Descrição deve ter no máximo 50 caracteres!")]
        [Display(Name = "Razão Social")] 
        public String razaosocial { get; set; }


        [Display(Name = "CNPJ")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "O CNPJ deve ter 14 dígitos.")]
        public string cnpj { get; set; }

        [Display(Name = "Telefone")]
        public String telefone { get; set; }

        [Display(Name = "Celular")]
        public String celular { get; set; }

        [Display(Name = "E-mail")]
        public String email { get; set; }
        
        [Display(Name = "CEP")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve ter 8 dígitos.")]
        public string cep { get; set; }


        [MaxLength(80, ErrorMessage = "Descrição deve ter no máximo 80 caracteres!")]
        [Display(Name = "Logradouro")]
        public String logradouro { get; set; }

        [Display(Name = "Número")]
        public String numero { get; set; }

        [Display(Name = "Bairro")]
        public String bairro { get; set; }

        [Display(Name = "Cidade")]
        public String cidade { get; set; }

        [Display(Name = "Estado")]
        public String estado { get; set; }

        [MaxLength(50, ErrorMessage = "Descrição deve ter no máximo 50 caracteres!")]
        [Display(Name = "Complemento")]
        public String complemento { get; set; }




        public EmpresaModel salvar(EmpresaModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Empresa cat = mapper.Map<Empresa>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EmpresaRepositorio repositorio =
                new EmpresaRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;


        }

        public List<EmpresaModel> listar()
        {
            List<EmpresaModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EmpresaRepositorio repositorio =
                    new EmpresaRepositorio(contexto);
                List<Empresa> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<EmpresaModel>>(lista);
            }

            return listamodel;
        }

        public EmpresaModel selecionar(int id)
        {
            EmpresaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EmpresaRepositorio repositorio =
                    new EmpresaRepositorio(contexto);
                //select * from categoria c where c.id = id
                Empresa cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<EmpresaModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EmpresaRepositorio repositorio =
                    new EmpresaRepositorio(contexto);
                Empresa cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
