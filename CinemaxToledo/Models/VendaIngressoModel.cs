using AutoMapper;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;

namespace CinemaxToledo.Models
{
    public class VendaIngressoModel
    {
        public int id { get; set; }
        public int qtde { get; set; }
        public Decimal valor { get; set; }
        public string? tipoIngresso { get; set; }

        public int idVenda { get; set; }
        public int idFilme { get; set; }


        public VendaIngressoModel salvar(VendaIngressoModel model)
        {

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            VendaIngresso cat = mapper.Map<VendaIngresso>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaIngressoRepositorio repositorio =
                new VendaIngressoRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }

        public virtual FilmeModel produto { get; set; }

        public List<VendaIngressoModel> listar(int idVendas)
        {
            List<VendaIngressoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaIngressoRepositorio repositorio =
                    new VendaIngressoRepositorio(contexto);
                List<VendaIngresso> lista = repositorio.Listar(c => c.idVenda == idVendas);
                listamodel = mapper.Map<List<VendaIngressoModel>>(lista);
                foreach (var item in listamodel)
                {
                    item.produto = new FilmeModel().selecionar(item.idFilme);
                }
            }

            return listamodel;
        }

        public void excluir(int id)
        {

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaIngressoRepositorio repositorio =
                    new VendaIngressoRepositorio(contexto);
                VendaIngresso cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }

        public VendaIngressoModel selecionar(int id)
        {
            VendaIngressoModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaIngressoRepositorio repositorio =
                    new VendaIngressoRepositorio(contexto);
                //select * from categoria c where c.id = id
                VendaIngresso cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<VendaIngressoModel>(cat);
            }
            return model;
        }
    }
}
