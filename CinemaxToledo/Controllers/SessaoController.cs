using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaxToledo.Controllers
{
    public class SessaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.titulo
            });
            return View(new SessaoModel());
        }



        [HttpPost]
        public IActionResult salvar(SessaoModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    SessaoModel catmodel = new SessaoModel();
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

            List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.titulo
            });

            return View("cadastro", model);
        }


        public IActionResult listar()
        {
            SessaoModel catModel = new SessaoModel();
            List<SessaoModel> lista = catModel.listar();
            return View(lista);//lista por parametro para a view
        }

        //fiz no fds:
        public virtual JsonResult diaHora(int id, DateTime date, DateTime time)
        {
            SessaoModel produto =
                (new SessaoModel()).selecionar(id);
            produto.diaSessao = date;
            produto.horaSessao = time;
            (new SessaoModel()).salvar(produto);

            return new JsonResult(produto);
        }

        public IActionResult prealterar(int id)
        {
            SessaoModel model = new SessaoModel();
            List<FilmeModel> lista = (new FilmeModel()).listar();
            ViewBag.listafilmes = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.titulo
            });
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            SessaoModel model = new SessaoModel();
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
