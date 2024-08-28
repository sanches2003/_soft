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

                    cfg.CreateMap<Cargo, CargoModel>();
                    cfg.CreateMap<CargoModel, Cargo>();

                    cfg.CreateMap<CategoriaProblema, CategoriaProblemaModel>();
                    cfg.CreateMap<CategoriaProblemaModel, CategoriaProblema>();

                    cfg.CreateMap<Login, LoginModel>();
                    cfg.CreateMap<LoginModel, Login>();

                    cfg.CreateMap<Empresa, EmpresaModel>();
                    cfg.CreateMap<EmpresaModel, Empresa>();

                    cfg.CreateMap<Atendimento, AtendimentoModel>();
                    cfg.CreateMap<AtendimentoModel, Atendimento>();

                    cfg.CreateMap<Status, StatusModel>();
                    cfg.CreateMap<StatusModel, Status>();

                    cfg.CreateMap<Pendencia, PendenciaModel>();
                    cfg.CreateMap<PendenciaModel, Pendencia>();

                });

                return config;
            }
        }

 }

