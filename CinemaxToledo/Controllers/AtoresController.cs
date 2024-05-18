using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaxToledo.Controllers
{
    public class AtoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            return View(new AtoresModel());
        }

        [HttpPost]
        public IActionResult salvar(AtoresModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    AtoresModel catmodel = new AtoresModel();
                    catmodel.salvar(model);
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

            List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            AtoresModel catModel = new AtoresModel();
            List<AtoresModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            AtoresModel model = new AtoresModel();
            List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            AtoresModel model = new AtoresModel();
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
