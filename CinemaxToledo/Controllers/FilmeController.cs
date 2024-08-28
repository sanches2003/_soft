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



            AtendimentoModel model = new AtendimentoModel();
            model.imagem = "";
            return View(model);
        }


        [HttpPost]
        public IActionResult salvar(AtendimentoModel model)
        {
            model.imagem = "";
            if (ModelState.IsValid)
            {

                try
                {
                    AtendimentoModel catmodel = new AtendimentoModel();
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



            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            AtendimentoModel catModel = new AtendimentoModel();
            List<AtendimentoModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            AtendimentoModel model = new AtendimentoModel();
            List<PlataformaModel> lista = (new PlataformaModel()).listar();
            ViewBag.listacategorias = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            AtendimentoModel model1 = new AtendimentoModel();
            List<CargoModel> lista1 = (new CargoModel()).listar();
            ViewBag.listaestudios = lista1.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
       
            AtendimentoModel model3 = new AtendimentoModel();
            List<AtendimentoModel> lista3 = (new AtendimentoModel()).listar();
            ViewBag.imagemsalva = lista3.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.arquivoImagem.ToString()
            });
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            AtendimentoModel model = new AtendimentoModel();
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

