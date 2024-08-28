using AutoMapper;
using Repositorio.Contexto;
using Repositorio.Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace CompusoftAtendimento.Models
{
    public class StatusModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }


        //Data Nottations:
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(30, ErrorMessage = "Descrição deve ter no máximo 30 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]

        [Display(Name = "Descrição")]
        public String descricao { get; set; }


        public StatusModel salvar(StatusModel model)
        {
            /*
            Estudio cat = new Estudio();
            cat.id = model.id;
            cat.descricao = model.descricao;
            */

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Status cat = mapper.Map<Status>(model);

            using (SoftContexto contexto = new SoftContexto())
            {
                StatusRepositorio repositorio =
                    new StatusRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
        public List<StatusModel> listar()
        {
            List<StatusModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                StatusRepositorio repositorio =
                    new StatusRepositorio(contexto);
                List<Status> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<StatusModel>>(lista);
            }
            return listamodel;
        }

        public StatusModel selecionar(int id)
        {
            StatusModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                StatusRepositorio repositorio =
                    new StatusRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from Estudio c 'onde' c.id = id
                Status cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<StatusModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {
            using (SoftContexto contexto = new SoftContexto())
            {
                StatusRepositorio repositorio =
                    new StatusRepositorio(contexto);
                Status cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
