using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompusoftAtendimento.Controllers
{
    public class PendenciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            return View(new PendenciaModel());
        }


        [HttpPost]
        public IActionResult salvar(PendenciaModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    PendenciaModel catmodel = new PendenciaModel();
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
            PendenciaModel catModel = new PendenciaModel();
            List<PendenciaModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            PendenciaModel model = new PendenciaModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            PendenciaModel model = new PendenciaModel();
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
