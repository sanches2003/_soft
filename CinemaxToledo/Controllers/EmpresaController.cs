using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompusoftAtendimento.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult cadastro()
        {
            return View(new EmpresaModel());
        }

        [HttpPost]
        public IActionResult salvar(EmpresaModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    EmpresaModel catmodel = new EmpresaModel();
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

            return RedirectToAction("listar");
        }


        public IActionResult listar()
        {
            EmpresaModel catModel = new EmpresaModel();
            List<EmpresaModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }


        public IActionResult prealterar(int id)
        {
            EmpresaModel model = new EmpresaModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            EmpresaModel model = new EmpresaModel();
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
