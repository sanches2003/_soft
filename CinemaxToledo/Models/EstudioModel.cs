using AutoMapper;
using Repositorio.Contexto;
using Repositorio.Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace CinemaxToledo.Models
{
    public class EstudioModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        

        //Data Nottations:
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(150, ErrorMessage = "Descrição deve ter no máximo 150 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]

        [Display(Name = "Nome do Estúdio")]
        public String nomeEstudio { get; set; }
        

        public EstudioModel salvar(EstudioModel model)
        {
            /*
            Estudio cat = new Estudio();
            cat.id = model.id;
            cat.descricao = model.descricao;
            */

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Estudio cat = mapper.Map<Estudio>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EstudioRepositorio repositorio =
                    new EstudioRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
        public List<EstudioModel> listar()
        {
            List<EstudioModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EstudioRepositorio repositorio =
                    new EstudioRepositorio(contexto);
                List<Estudio> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<EstudioModel>>(lista);
            }
            return listamodel;
        }

        public EstudioModel selecionar(int id)
        {
            EstudioModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EstudioRepositorio repositorio =
                    new EstudioRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from Estudio c 'onde' c.id = id
                Estudio cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<EstudioModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                EstudioRepositorio repositorio =
                    new EstudioRepositorio(contexto);
                Estudio cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
