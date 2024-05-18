//using AspNetCore;
using AutoMapper;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace CinemaxToledo.Models
{
    public class SessaoModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }

        [Display(Name = "Dia da Sessão")] 
        public DateTime diaSessao { get; set; }

        [Display(Name = "Hora da Sessão")]
        public DateTime horaSessao { get; set; }
        
        [Display(Name = "Sala")]
        public int sala { get; set; }

        [Display(Name = "Local")]
        public String local { get; set; }

        //Chave Estrangeira:

        [Display(Name = "Escolha o filme")]
        public int idFilme { get; set; }
        


        public SessaoModel salvar(SessaoModel model)
        {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Sessao cat = mapper.Map<Sessao>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                SessaoRepositorio repositorio =
                new SessaoRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;


        }

        /*
        public List<SessaoModel> listarSessao(int idFilme)
        {
            List<SessaoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                SessaoRepositorio repositorio =
                    new SessaoRepositorio(contexto);
                List<Sessao> lista = repositorio.Listar(c => c.idFilme == idFilme);
                listamodel = mapper.Map<List<SessaoModel>>(lista);
            }
        }
        */

        public List<SessaoModel> listar()
        {
            List<SessaoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                SessaoRepositorio repositorio =
                    new SessaoRepositorio(contexto);
                List<Sessao> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<SessaoModel>>(lista);
            }

            return listamodel;
        }

        public SessaoModel selecionar(int id)
        {
            SessaoModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                SessaoRepositorio repositorio =
                    new SessaoRepositorio(contexto);
                //select * from categoria c where c.id = id
                Sessao cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<SessaoModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                SessaoRepositorio repositorio =
                    new SessaoRepositorio(contexto);
                Sessao cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
