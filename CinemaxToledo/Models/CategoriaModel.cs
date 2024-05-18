using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using CinemaxToledo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace CinemaxToledo.Models
{
    public class CategoriaModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }

        //Data Nottations:
        [Required(ErrorMessage="Campo obrigatório")]
        [MaxLength(150, ErrorMessage = "Descrição deve ter no máximo 150 caracteres!")]
        [MinLength(3, ErrorMessage = "Descrição deve ter no mínimo 3 caracteres!")]
        [Display(Name ="Descrição")]

        public String descricao  { get; set; }
        [Display(Name = "Nome da Categoria")]

        public CategoriaModel salvar(CategoriaModel model) 
        {
            /*
            Categoria cat = new Categoria();
            cat.id = model.id;
            cat.descricao = model.descricao;
            */

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Categoria cat = mapper.Map<Categoria>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CategoriaRepositorio repositorio = 
                    new CategoriaRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
            public List<CategoriaModel> listar()
            {
            List<CategoriaModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CategoriaRepositorio repositorio =
                    new CategoriaRepositorio(contexto);
                List<Categoria> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<CategoriaModel>>(lista);
            }
            return listamodel;       
            }

            public CategoriaModel selecionar(int id)
            {
            CategoriaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                CategoriaRepositorio repositorio = 
                    new CategoriaRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from categoria c 'onde' c.id = id
                Categoria cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<CategoriaModel>(cat);
            }
            return model;
            }   

            public void excluir(int id)
            {
                using (CinemaxContexto contexto = new CinemaxContexto())
                {
                CategoriaRepositorio repositorio =
                    new CategoriaRepositorio(contexto);
                Categoria cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
                }
            }
    }
}
