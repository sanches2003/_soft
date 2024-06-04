using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaxToledo.Controllers
{
    public class CargoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            return View(new CargoModel());
        }


        //HTTPPOST quando for retornar post
        [HttpPost]
        public IActionResult salvar(CargoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CargoModel catmodel = new CargoModel();
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
            CargoModel catModel = new CargoModel();
            List<CargoModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            CargoModel model = new CargoModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            CargoModel model = new CargoModel();
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
