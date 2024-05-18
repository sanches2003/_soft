using AutoMapper;
using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace CinemaxToledo.Controllers
{
    public class VendaController : Controller
    {
        public IActionResult Index()
        {
            List<VendaIngressoModel> lista = new List<VendaIngressoModel>();
            // lista = new VendaIngressoModel().listar(1);
            return View(lista);//lista por parametro para a view

        }

        public IActionResult excluirProduto(int id)
        {
            (new VendaIngressoModel()).excluir(id);

            var lista = (new VendaIngressoModel()).listar(
           HttpContext.Session.GetInt32("idVenda").Value);

            return View("Index", lista);
        }

        public IActionResult Finalizar()
        {
            //retornar uma view para login ou cadastro de cliente
            //salvar na sessão o id cliente logado ou que fez cadastro

            //depois criar outro metodo para finalizar com geração de pagamento

            //alterar o status da venda para aguardando pagamento:
            var idVenda = HttpContext.Session.GetInt32("idVenda").Value;
            VendaModel compras = new VendaModel().selecionar(idVenda);
            compras.idStatus = 3; // aguardando pagamento
            var produtos = (new VendaIngressoModel()).listar(idVenda);
            decimal total = 0;
            foreach (var item in produtos)
            {
                total += item.valor;
            }
            compras.valor = total;
            //        compras.idCliente = HttpContext.Session.GetInt32("idCliente").Value;

           

            //gerar o pagamento:

            //ClienteModel cliente = new ClienteModel().selecionar(HttpContext.Session.GetInt32("idCliente").Value);
            Task<RetornoMercadoPago> retorno = new VendaModel().gerarPagamentoMercadoPago
            (new MercadoPagoModel()
            {
                email = "fekeike2010@gmail.com", //como ficaria: cliente.email,
                cidade = "Presidente Prudente",
                cep = "19015-230",
                estado = "São Paulo",
                idPagamento = idVenda,
                logradouro = "Avenida Brasil",
                nome = "Cliente teste",
                nomePlano = "Venda Ingresso Toledo",
                numero = "20",
                telefone = "18991680259",
                valor = total,                          
            });

            if(retorno.Result.status == "SUCESSO")
            {
                compras.idPreferencia = retorno.Result.idPreferencia;
                compras.url = retorno.Result.url;
            }
            else
            {
                compras.idStatus = 4; //cancelada
            }
            compras.dataVenda = DateTime.Now;
            new VendaModel().salvar(compras);
            return Redirect(retorno.Result.url);

            //redirecionar o usuario
        }

        public virtual JsonResult alterarQtde(int id, int qtde){
            VendaIngressoModel produto =
                (new VendaIngressoModel()).selecionar(id);
            produto.qtde = qtde;
            decimal valorInteira = new FilmeModel().selecionar(produto.idFilme).valorInteira;
            decimal valorMeia = new FilmeModel().selecionar(produto.idFilme).valorMeia;
            if (produto.tipoIngresso == "I")
            {
                produto.valor = valorInteira * qtde;
            }
            else{
                produto.valor = valorMeia * qtde;
            }
            (new VendaIngressoModel()).salvar(produto);

            

            return new JsonResult(produto);
        }


        //o parametro id = id_produto
        public IActionResult comprarProduto(int id, string tipoIngresso)
        {
            //buscar o valor do produto
            var produto = (new FilmeModel()).selecionar(id);
            var valorInteira = produto.valorInteira;
            var valorMeia = produto.valorMeia;
            decimal valorAuxiliar = 0;

            if(tipoIngresso == "I")
            {
                valorAuxiliar = valorInteira;
            }
            else
            {
                valorAuxiliar = valorMeia;
            }
            //validar se a compra não iniciou
            if (HttpContext.Session.GetInt32("idVenda") == null)
            {

                VendaModel compras = new VendaModel()
                {
                    dataVenda = DateTime.Now,
                    idStatus = 1,
                    valor = valorAuxiliar
                                    //criar coluna valor auxiliar
                };
                (new VendaModel()).salvar(compras);

                
                //inserir na sessão
                HttpContext.Session.SetInt32("idVenda", compras.id);

            }

            //inserir na tabela de compras produtos
            var prod = new VendaIngressoModel()
            {
                idVenda = HttpContext.Session.GetInt32("idVenda").Value,
                idFilme = id,//parametro
                qtde = 1,
                valor = valorAuxiliar, //criar coluna valor auxiliar
                tipoIngresso = tipoIngresso,

            };
            (new VendaIngressoModel()).salvar(prod);

            var lista = (new VendaIngressoModel()).listar(
                HttpContext.Session.GetInt32("idVenda").Value);

            return View("Index", lista);
        }

        public IActionResult Finalizacao()
        {
            return View();
        }

        [HttpGet]
        public IActionResult retornoMercadoPago(string collection_id, string collection_status, string payment_id,
       string status, string external_reference, string payment_type, string merchant_order_id,
       string preference_id, string site_id, string processing_mode, string merchant_account_id)
        {
            //obter o pagamento pelo idPreferencia(preference_id);
            VendaModel compras = new VendaModel().selecionarIdPreferencia(
                preference_id);
            compras.idStatus = 2;

            new VendaModel().salvar(compras);

            if (status == "approved")
            {
                //baixar como pago

                return RedirectToAction("Finalizacao", "Venda");
            }
            else
            {

                return View();
            }

        }
    }
}