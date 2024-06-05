using AutoMapper;
using Repositorio.Contexto;
using Repositorio.Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace CinemaxToledo.Models
{
    public class CargoModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }
        

        //Data Nottations:
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(30, ErrorMessage = "Descrição deve ter no máximo 30 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]

        [Display(Name = "Descrição")]
        public String descricao { get; set; }
        

        public CargoModel salvar(CargoModel model)
        {
            /*
            Estudio cat = new Estudio();
            cat.id = model.id;
            cat.descricao = model.descricao;
            */

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Cargo cat = mapper.Map<Cargo>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CargoRepositorio repositorio =
                    new CargoRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
        public List<CargoModel> listar()
        {
            List<CargoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CargoRepositorio repositorio =
                    new CargoRepositorio(contexto);
                List<Cargo> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<CargoModel>>(lista);
            }
            return listamodel;
        }

        public CargoModel selecionar(int id)
        {
            CargoModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CargoRepositorio repositorio =
                    new CargoRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from Estudio c 'onde' c.id = id
                Cargo cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<CargoModel>(cat);
            }
            return model;
        }

        public void excluir(int id)
        {
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CargoRepositorio repositorio =
                    new CargoRepositorio(contexto);
                Cargo cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }
    }
}
