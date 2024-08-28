using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompusoftAtendimento.Controllers
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

        public IActionResult cadastresecargo()
        {
            List<CargoModel> lista = (new CargoModel()).listar();
            ViewBag.listacargos = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            return View(new CargoModel());
        }

        [HttpPost]
        public IActionResult salvarcadastresecargo(CargoModel model)
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
            return View("cadastresecargo");
        }

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
            return RedirectToAction("listar");
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
