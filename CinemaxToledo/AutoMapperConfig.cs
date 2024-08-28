using AutoMapper;
using CompusoftAtendimento.Models;
using Repositorio.Entidades;

namespace CompusoftAtendimento
{
        public class AutoMapperConfig : Profile
        {
            public static MapperConfiguration RegisterMappings()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Plataforma, PlataformaModel>();
                    cfg.CreateMap<PlataformaModel, Plataforma>();

                    cfg.CreateMap<FormaAtendimento, FormaAtendimentoModel>();
                    cfg.CreateMap<FormaAtendimentoModel, FormaAtendimento>();

                    cfg.CreateMap<Filme, AtendimentoModel>();
                    cfg.CreateMap<AtendimentoModel, Filme>();

                    cfg.CreateMap<Cargo, CargoModel>();
                    cfg.CreateMap<CargoModel, Cargo>();

                    cfg.CreateMap<CategoriaProblema, CategoriaProblemaModel>();
                    cfg.CreateMap<CategoriaProblemaModel, CategoriaProblema>();

                    cfg.CreateMap<Login, LoginModel>();
                    cfg.CreateMap<LoginModel, Login>();

                    cfg.CreateMap<Empresa, EmpresaModel>();
                    cfg.CreateMap<EmpresaModel, Empresa>();

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

