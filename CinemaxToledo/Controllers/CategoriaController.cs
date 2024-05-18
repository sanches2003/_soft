using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaxToledo.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro ()
        {
            return View(new CategoriaModel());
        }


        //HTTPPOST quando for retornar post
        [HttpPost]
        public IActionResult salvar(CategoriaModel model)
        {
            if (ModelState.IsValid)
            {
            try
            {          
            CategoriaModel catmodel = new CategoriaModel();
            catmodel.salvar(model);
                ViewBag.mensagem = "Dados salvos com sucesso!";
                ViewBag.classe = "alert-success";
                return View("cadastro", new CategoriaModel());
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
            CategoriaModel catModel = new CategoriaModel();
            List<CategoriaModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            CategoriaModel model = new CategoriaModel();
            return View("cadastro", model.selecionar(id));
             }

        public IActionResult excluir(int id)
        {
            CategoriaModel model = new CategoriaModel();
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
