using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using CompusoftAtendimento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace CompusoftAtendimento.Models
{
    public class PlataformaModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }

        //Data Nottations:
        [Required(ErrorMessage="Campo obrigatório")]
        [MaxLength(30, ErrorMessage = "Descrição deve ter no máximo 30 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]
        [Display(Name ="Descrição")]

        public String descricao  { get; set; }
        [Display(Name = "Nome da Plataforma")]

        public PlataformaModel salvar(PlataformaModel model) 
        {
            /*
            Categoria cat = new Categoria();
            cat.id = model.id;
            cat.descricao = model.descricao;
            */

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Plataforma cat = mapper.Map<Plataforma>(model);

            using (SoftContexto contexto = new SoftContexto())
            {
                PlataformaRepositorio repositorio = 
                    new PlataformaRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
            public List<PlataformaModel> listar()
            {
            List<PlataformaModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                PlataformaRepositorio repositorio =
                    new PlataformaRepositorio(contexto);
                List<Plataforma> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<PlataformaModel>>(lista);
            }
            return listamodel;       
            }

            public PlataformaModel selecionar(int id)
            {
            PlataformaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                PlataformaRepositorio repositorio = 
                    new PlataformaRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from categoria c 'onde' c.id = id
                Plataforma cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<PlataformaModel>(cat);
            }
            return model;
            }   

            public void excluir(int id)
            {
                using (SoftContexto contexto = new SoftContexto())
                {
                PlataformaRepositorio repositorio =
                    new PlataformaRepositorio(contexto);
                Plataforma cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
                }
            }
    }
}
