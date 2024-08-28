using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace CompusoftAtendimento.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            return View(new StatusModel());

        }

        //HTTPPOST quando for retornar post
        [HttpPost]
        public IActionResult salvar(StatusModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusModel catmodel = new StatusModel();
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
            StatusModel catModel = new StatusModel();
            List<StatusModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            StatusModel model = new StatusModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            StatusModel model = new StatusModel();
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
