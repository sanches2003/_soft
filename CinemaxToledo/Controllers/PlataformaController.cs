using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaxToledo.Controllers
{
    public class PlataformaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro ()
        {
            return View(new PlataformaModel());
        }


        //HTTPPOST quando for retornar post
        [HttpPost]
        public IActionResult salvar(PlataformaModel model)
        {
            if (ModelState.IsValid)
            {
            try
            {          
            PlataformaModel catmodel = new PlataformaModel();
            catmodel.salvar(model);
                ViewBag.mensagem = "Dados salvos com sucesso!";
                ViewBag.classe = "alert-success";
                return View("cadastro", new PlataformaModel());
            }
            catch(Exception ex)
            {
                ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                ViewBag.classe = "alert-danger";
            }
            }
            return View("cadastro");      
        }

        
       public IActionResult listar()
        {
            PlataformaModel catModel = new PlataformaModel();
            List<PlataformaModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            PlataformaModel model = new PlataformaModel();
            return View("cadastro", model.selecionar(id));
             }

        public IActionResult excluir(int id)
        {
            PlataformaModel model = new PlataformaModel();
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
