using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaxToledo.Controllers
{
    public class EstudioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            return View(new EstudioModel());
        }


        //HTTPPOST quando for retornar post
        [HttpPost]
        public IActionResult salvar(EstudioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EstudioModel catmodel = new EstudioModel();
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
            return View("cadastro");
        }


        public IActionResult listar()
        {
            EstudioModel catModel = new EstudioModel();
            List<EstudioModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            EstudioModel model = new EstudioModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            EstudioModel model = new EstudioModel();
            try
            {
                model.excluir(id);
                ViewBag.mensagem = "Dados excluídos com sucesso!";
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
