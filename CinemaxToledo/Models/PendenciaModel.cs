using AutoMapper;
using Repositorio.Contexto;
using Repositorio.Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace CompusoftAtendimento.Models
{
    public class PendenciaModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }


        //Data Nottations:
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(30, ErrorMessage = "Descrição deve ter no máximo 30 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]

        [Display(Name = "Descrição")]
        public String descricao { get; set; }


        public PendenciaModel salvar(PendenciaModel model)
        {

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Pendencia cat = mapper.Map<Pendencia>(model);

            using (SoftContexto contexto = new SoftContexto())
            {
                PendenciaRepositorio repositorio =
                    new PendenciaRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
        public List<PendenciaModel> listar()
        {
            List<PendenciaModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                PendenciaRepositorio repositorio =
                    new PendenciaRepositorio(contexto);
                List<Pendencia> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<PendenciaModel>>(lista);
            }
            return listamodel;
        }

        public PendenciaModel selecionar(int id)
        {
            PendenciaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                PendenciaRepositorio repositorio =
                    new PendenciaRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from Estudio c 'onde' c.id = id
                Pendencia cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<PendenciaModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {
            using (SoftContexto contexto = new SoftContexto())
            {
                PendenciaRepositorio repositorio =
                    new PendenciaRepositorio(contexto);
                Pendencia cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
