using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaxToledo.Controllers
{
    public class FilmeController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        public FilmeController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            List<PlataformaModel> lista = (new PlataformaModel()).listar();
            ViewBag.listacategorias = lista.Select(c => new SelectListItem()           
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            List<CargoModel> lista1 = (new CargoModel()).listar();
            ViewBag.listaestudios = lista1.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            List<AtoresModel> lista2 = (new AtoresModel()).listar();
            ViewBag.listaatores = lista2 .Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.nomeAtor
            });



            FilmeModel model = new FilmeModel();
            model.imagem = "";
            return View(model);
        }


        [HttpPost]
        public IActionResult salvar(FilmeModel model)
        {
            model.imagem = "";
            if (ModelState.IsValid)
            {

                try
                {
                    FilmeModel catmodel = new FilmeModel();
                    catmodel.salvar(model, webHostEnvironment);
                    ViewBag.mensagem = "Dados salvos com sucesso!";
                    ViewBag.classe = "alert-success";
                }
                catch (Exception ex)
                {

                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                }
            }
            else
            {
                ViewBag.mensagem = "ops... Erro ao salvar! verifique os campos";
                ViewBag.classe = "alert-danger";

            }

            //criando listas

            List<PlataformaModel> lista = (new PlataformaModel()).listar();
            ViewBag.listacategorias = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            List<CargoModel> lista1 = (new CargoModel()).listar();
            ViewBag.listaestudios = lista1.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            List<AtoresModel> lista2 = (new AtoresModel()).listar();
            ViewBag.listaatores = lista2.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.nomeAtor
            });

            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            FilmeModel catModel = new FilmeModel();
            List<FilmeModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            FilmeModel model = new FilmeModel();
            List<PlataformaModel> lista = (new PlataformaModel()).listar();
            ViewBag.listacategorias = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            FilmeModel model1 = new FilmeModel();
            List<CargoModel> lista1 = (new CargoModel()).listar();
            ViewBag.listaestudios = lista1.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            FilmeModel model2 = new FilmeModel();
            List<AtoresModel> lista2 = (new AtoresModel()).listar();
            ViewBag.listaatores = lista2.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.nomeAtor
            });
            FilmeModel model3 = new FilmeModel();
            List<FilmeModel> lista3 = (new FilmeModel()).listar();
            ViewBag.imagemsalva = lista3.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.arquivoImagem.ToString()
            });
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            FilmeModel model = new FilmeModel();
            try
            {

                model.excluir(id);
                ViewBag.mensagem = "Dados excluidos com sucesso!";
                ViewBag.classe = "alert-success";
            }
            catch (Exception ex)
            {

                ViewBag.mensagem = "Ops... Não foi possível excluir!" + ex.Message;
                ViewBag.classe = "alert-danger";
            }

            return View("listar", model.listar());
        }
    }
}

