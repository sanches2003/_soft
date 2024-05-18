using AutoMapper;
using Repositorio.Contexto;
using Repositorio.Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace CinemaxToledo.Models
{
        public class AtoresModel
        {
        [Display(Name = "Código")]
        public int id { get; set; }

        [Display(Name = "Atores")]
        public String nomeAtor { get; set; }
        

        //public int idAtoresFilme { get; set; }

        //public String nomeAtores { get; set; }

        public AtoresModel salvar(AtoresModel model)
            {

                //Categoria cat = new Categoria();
                //cat.id = model.id;
                //cat.descricao = model.descricao;
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                Atores cat = mapper.Map<Atores>(model);

                using (CinemaxContexto contexto = new CinemaxContexto())
                {
                    AtoresRepositorio repositorio =
                    new AtoresRepositorio(contexto);

                    if (model.id == 0)
                        repositorio.Inserir(cat);
                    else
                        repositorio.Alterar(cat);

                    contexto.SaveChanges();
                }
                model.id = cat.id;
                return model;


            }


            public List<AtoresModel> listar()
            {
                List<AtoresModel> listamodel = null;
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                using (CinemaxContexto contexto = new CinemaxContexto())
                {
                    AtoresRepositorio repositorio =
                        new AtoresRepositorio(contexto);
                    List<Atores> lista = repositorio.ListarTodos();
                    listamodel = mapper.Map<List<AtoresModel>>(lista);
                }

                return listamodel;
            }

            public AtoresModel selecionar(int id)
            {
                AtoresModel model = null;
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                using (CinemaxContexto contexto = new CinemaxContexto())
                {
                    AtoresRepositorio repositorio =
                        new AtoresRepositorio(contexto);
                    //select * from categoria c where c.id = id
                    Atores cat = repositorio.Recuperar(c => c.id == id);
                    model = mapper.Map<AtoresModel>(cat);
                }
                return model;
            }

            public void excluir(int id)
            {

                using (CinemaxContexto contexto = new CinemaxContexto())
                {
                    AtoresRepositorio repositorio =
                        new AtoresRepositorio(contexto);
                    Atores cat = repositorio.Recuperar(c => c.id == id);
                    repositorio.Excluir(cat);
                    contexto.SaveChanges();
                }
            }
        }
}
