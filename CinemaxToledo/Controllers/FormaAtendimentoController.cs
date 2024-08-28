using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompusoftAtendimento.Controllers
{
    public class FormaAtendimentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro ()
        {
            return View(new FormaAtendimentoModel());
        }


        [HttpPost]
        public IActionResult salvar(FormaAtendimentoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {          
                    FormaAtendimentoModel catmodel = new FormaAtendimentoModel();
                    catmodel.salvar(model);
                    ViewBag.mensagem = "Dados salvos com sucesso!";
                    ViewBag.classe = "alert-success";
                }
                catch(Exception ex)
                {
                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                }
            }
            return RedirectToAction("listar");
        }

        
       public IActionResult listar()
        {
            FormaAtendimentoModel catModel = new FormaAtendimentoModel();
            List<FormaAtendimentoModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            FormaAtendimentoModel model = new FormaAtendimentoModel();
            return View("cadastro", model.selecionar(id));
             }

        public IActionResult excluir(int id)
        {
            FormaAtendimentoModel model = new FormaAtendimentoModel();
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
