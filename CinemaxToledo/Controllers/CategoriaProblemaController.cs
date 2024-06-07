using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompusoftAtendimento.Controllers
{
    public class CategoriaProblemaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            return View(new CategoriaProblemaModel());
        }


        [HttpPost]
        public IActionResult salvar(CategoriaProblemaModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    CategoriaProblemaModel catmodel = new CategoriaProblemaModel();
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

            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            CategoriaProblemaModel catModel = new CategoriaProblemaModel();
            List<CategoriaProblemaModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            CategoriaProblemaModel model = new CategoriaProblemaModel();
            return View("cadastro", model.selecionar(id));
        }

        /*
        public IActionResult prealterar(int id)
        {
            CategoriaProblemaModel model = new CategoriaProblemaModel();
            ***List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            return View("cadastro", model.selecionar(id));
        }
        */
        public IActionResult excluir(int id)
        {
            CategoriaProblemaModel model = new CategoriaProblemaModel();
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
