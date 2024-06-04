using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CinemaxToledo.Controllers
{
    public class HomeSiteController : Controller
    {

        private readonly ILogger<HomeSiteController> _logger;

        public IActionResult Index()
        {
            FilmeModel model = new FilmeModel();
            return View(model.listar());
        }

        public HomeSiteController(ILogger<HomeSiteController> logger)
        {
            _logger = logger;
        }

        public IActionResult detalhes(int id)
        {
            FilmeModel model = new FilmeModel();
            FilmeModel produto = model.selecionar(id);
            produto.categoria = (new PlataformaModel()).selecionar(produto.idCategoria);
            produto.estudio = (new CargoModel()).selecionar(produto.idEstudio);
            produto.atores = (new AtoresModel()).selecionar(produto.idAtores);
            //produto.sessoes = (new SessaoModel()).listarSessao(produto.sessao);
            return View(produto);

        }

    }
}