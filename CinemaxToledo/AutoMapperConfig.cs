using AutoMapper;
using CinemaxToledo.Models;
using Repositorio.Entidades;

namespace CinemaxToledo
{
        public class AutoMapperConfig : Profile
        {
            public static MapperConfiguration RegisterMappings()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Plataforma, PlataformaModel>();
                    cfg.CreateMap<PlataformaModel, Plataforma>();

                    cfg.CreateMap<Filme, FilmeModel>();
                    cfg.CreateMap<FilmeModel, Filme>();

                    cfg.CreateMap<Cargo, CargoModel>();
                    cfg.CreateMap<CargoModel, Cargo>();

                    cfg.CreateMap<CategoriaProblema, CategoriaProblemaModel>();
                    cfg.CreateMap<CategoriaProblemaModel, CategoriaProblema>();

                    cfg.CreateMap<Login, LoginModel>();
                    cfg.CreateMap<LoginModel, Login>();

                    cfg.CreateMap<Empresa, EmpresaModel>();
                    cfg.CreateMap<EmpresaModel, Empresa>();

                    cfg.CreateMap<Venda, VendaModel>();
                    cfg.CreateMap<VendaModel, Venda>();

                    cfg.CreateMap<VendaIngresso, VendaIngressoModel>();
                    cfg.CreateMap<VendaIngressoModel, VendaIngresso>();



                    //se precisar mapear:
                    /*
                    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.codigo))
                    .ForMember(dest => dest.descricao, opt => opt.MapFrom(src => src.nome))
                    */
                });

                return config;
            }
        }

 }

