using AutoMapper;
using Repositorio.Contexto;
using Repositorio.Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace CompusoftAtendimento.Models
{
        public class CategoriaProblemaModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }


        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(20, ErrorMessage = "Descrição deve ter no máximo 20 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]
        [Display(Name = "Descrição")]

        public String descricao { get; set; }
        

        //public int idAtoresFilme { get; set; }

        //public String nomeAtores { get; set; }

        public CategoriaProblemaModel salvar(CategoriaProblemaModel model)
            {

                //Categoria cat = new Categoria();
                //cat.id = model.id;
                //cat.descricao = model.descricao;
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                CategoriaProblema cat = mapper.Map<CategoriaProblema>(model);

                using (SoftContexto contexto = new SoftContexto())
                {
                CategoriaProblemaRepositorio repositorio =
                    new CategoriaProblemaRepositorio(contexto);

                    if (model.id == 0)
                        repositorio.Inserir(cat);
                    else
                        repositorio.Alterar(cat);

                    contexto.SaveChanges();
                }
                model.id = cat.id;
                return model;


            }


            public List<CategoriaProblemaModel> listar()
            {
                List<CategoriaProblemaModel> listamodel = null;
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                using (SoftContexto contexto = new SoftContexto())
                {
                CategoriaProblemaRepositorio repositorio =
                        new CategoriaProblemaRepositorio(contexto);
                    List<CategoriaProblema> lista = repositorio.ListarTodos();
                    listamodel = mapper.Map<List<CategoriaProblemaModel>>(lista);
                }

                return listamodel;
            }

            public CategoriaProblemaModel selecionar(int id)
            {
            CategoriaProblemaModel model = null;
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                using (SoftContexto contexto = new SoftContexto())
                {
                CategoriaProblemaRepositorio repositorio =
                        new CategoriaProblemaRepositorio(contexto);
                    //select * from categoria c where c.id = id
                    CategoriaProblema cat = repositorio.Recuperar(c => c.id == id);
                    model = mapper.Map<CategoriaProblemaModel>(cat);
                }
                return model;
            }

            public void excluir(int id)
            {

                using (SoftContexto contexto = new SoftContexto())
                {
                CategoriaProblemaRepositorio repositorio =
                        new CategoriaProblemaRepositorio(contexto);
                    CategoriaProblema cat = repositorio.Recuperar(c => c.id == id);
                    repositorio.Excluir(cat);
                    contexto.SaveChanges();
                }
            }
        }
}
