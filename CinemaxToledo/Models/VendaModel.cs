using AutoMapper;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace CinemaxToledo.Models
{
    public class VendaModel
    {

        public int id { get; set; }
        public DateTime dataVenda { get; set; }

        public Decimal valor{ get; set; }
        public string? tipoIngresso { get; set; }

        public int idStatus { get; set; }


        public String? idPreferencia { get; set; }
        public String? url { get; set; }


        public VendaModel salvar(VendaModel model)
        {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Venda cat = mapper.Map<Venda>(model);

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaRepositorio repositorio =
                new VendaRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;
        }

        public VendaModel selecionar(int id)
        {
            VendaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaRepositorio repositorio =
                    new VendaRepositorio(contexto);
                //select * from categoria c where c.id = id
                Venda cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<VendaModel>(cat);
            }

            return model;
        }

        public VendaModel selecionarIdPreferencia(String idPreferencia)
        {
            VendaModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                VendaRepositorio repositorio =
                    new VendaRepositorio(contexto);
                //select * from categoria c where c.id = id
                Venda cat = repositorio.Recuperar(c => c.idPreferencia == idPreferencia);
                model = mapper.Map<VendaModel>(cat);
            }

            return model;
        }

        public async Task<RetornoMercadoPago> gerarPagamentoMercadoPago(MercadoPagoModel model)
        {

            RetornoMercadoPago ret = new RetornoMercadoPago();
            try
            {

                string cidade = model.cidade;
                string estado = model.estado;



                // Adicione as credenciais
                MercadoPagoConfig.AccessToken = "TEST-4098717526832232-051620-930227379a66676944f367f45c293015-731269695";


                String[] split = model.nome.Split(' ');
                // ...
                var payer = new PreferencePayerRequest
                {
                    Name = split[0],
                    Surname = split[split.Length - 1],
                    Email = model.email,
                    Phone = new PhoneRequest
                    {
                        AreaCode = "",
                        Number = model.telefone,
                    },

                    Identification = new IdentificationRequest
                    {
                        Type = "DNI",
                        Number = model.idPagamento.ToString(),
                    },

                    Address = new AddressRequest
                    {
                        StreetName = model.logradouro,
                        StreetNumber = model.numero,
                        ZipCode = model.cep,
                    },
                };
                // ...


                // ...
                var item = new PreferenceItemRequest
                {
                    Id = model.idPagamento.ToString(),
                    Title = "Venda Cinema Toledo",
                    Description = "Compra de ingressos Cinema Toledo",
                    CategoryId = "Cinema",
                    Quantity = 1,
                    CurrencyId = "BRL",
                    UnitPrice = model.valor,
                };
                // ...


                var request = new PreferenceRequest
                {
                    // ...
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "http://felipesanches-001-site1.htempurl.com/Venda/retornoMercadoPago",
                        Failure = "ENDPOINT_Retorno_falha",
                        Pending = "ENDPOINT_Retorno_pendencias",
                    },
                    AutoReturn = "approved",
                    Payer = payer,
                    Items = new List<PreferenceItemRequest>()
                };
                request.Items.Add(item);

                // Cria a preferência usando o client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);
                ret.url = preference.InitPoint;
                ret.idPreferencia = preference.Id;
                ret.status = "SUCESSO";
                // preference.
                return ret;

            }
            catch (Exception ex)
            {
                ret.status = "ERRO";
                ret.erro = ex.Message;
                return ret;
            }
        }
    }
    
}


