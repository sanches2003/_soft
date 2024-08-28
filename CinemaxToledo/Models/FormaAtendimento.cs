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
    public class FormaAtendimentoModel
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

        public FormaAtendimentoModel salvar(FormaAtendimentoModel model) 
        {

            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            FormaAtendimento cat = mapper.Map<FormaAtendimento>(model);

            using (SoftContexto contexto = new SoftContexto())
            {
                FormaAtendimentoRepositorio repositorio = 
                    new FormaAtendimentoRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }
            public List<FormaAtendimentoModel> listar()
            {
            List<FormaAtendimentoModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                FormaAtendimentoRepositorio repositorio =
                    new FormaAtendimentoRepositorio(contexto);
                List<FormaAtendimento> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<FormaAtendimentoModel>>(lista);
            }
            return listamodel;       
            }

            public FormaAtendimentoModel selecionar(int id)
            {
            FormaAtendimentoModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (SoftContexto contexto = new SoftContexto())
            {
                FormaAtendimentoRepositorio repositorio = 
                    new FormaAtendimentoRepositorio(contexto);
                repositorio.Recuperar(c => c.id == id);//select * from categoria c 'onde' c.id = id
                FormaAtendimento cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<FormaAtendimentoModel>(cat);
            }
            return model;
            }   

            public void excluir(int id)
            {
                using (SoftContexto contexto = new SoftContexto())
                {
                FormaAtendimentoRepositorio repositorio =
                    new FormaAtendimentoRepositorio(contexto);
                FormaAtendimento cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
                }
            }
    }
}
