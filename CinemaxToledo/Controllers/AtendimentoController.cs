using CompusoftAtendimento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompusoftAtendimento.Controllers
{
    public class AtendimentoController : Controller
    {
         public IActionResult cadastro()
        {
            List<CategoriaProblemaModel> listaCategoriaProblema = (new CategoriaProblemaModel()).listar();
            ViewBag.listacategoriaproblemas = listaCategoriaProblema.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            List<EmpresaModel> listaEmpresa = (new EmpresaModel()).listar();
            ViewBag.listaempresas = listaEmpresa.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.razaosocial
            });

            List<PlataformaModel> listaPlataforma = (new PlataformaModel()).listar();
            ViewBag.listaplataformas = listaPlataforma.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            List<StatusModel> listaStatus = (new StatusModel()).listar();
           ViewBag.listausuarios = listaStatus.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao


            });

            List<LoginModel> listaUsuarios = (new LoginModel()).listar();
            ViewBag.listausuarios = listaUsuarios.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.login
            });

            return View(new AtendimentoModel());
        }

        [HttpPost]
        public IActionResult salvar(AtendimentoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AtendimentoModel catmodel = new AtendimentoModel();
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
            AtendimentoModel catModel = new AtendimentoModel();
            List<AtendimentoModel> lista = catModel.listar();
            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            AtendimentoModel model = new AtendimentoModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            AtendimentoModel model = new AtendimentoModel();
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

