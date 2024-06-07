using CinemaxToledo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaxToledo.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult cadastro()
        {
            List<CargoModel> lista = (new CargoModel()).listar();
            ViewBag.listacargos = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            return View(new LoginModel());
        }

        public IActionResult cadastrese()
        {
            List<CargoModel> lista = (new CargoModel()).listar();
            ViewBag.listacargos = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });

            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult salvarcadastrese(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LoginModel catmodel = new LoginModel();
                    catmodel.salvar(model);
                    ViewBag.mensagem = "";
                    ViewBag.classe = "";
                }
                catch (Exception ex)
                {
                    ViewBag.mensagem = "ops... Erro ao salvar!" + ex.Message + "/" + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                }
            }
            return View("login");
        }

        //HTTPPOST quando for retornar post
        [HttpPost]
        public IActionResult salvar(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LoginModel catmodel = new LoginModel();
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

            LoginModel catModel = new LoginModel();
            List<LoginModel> lista = catModel.listar();

            return View(lista); //passando a lista por parametro para a view
        }

        public IActionResult prealterar(int id)
        {
            List<CargoModel> lista = (new CargoModel()).listar();
            ViewBag.listacargos = lista.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.descricao
            });
            LoginModel model = new LoginModel();
            return View("cadastro", model.selecionar(id));
        }

        public IActionResult excluir(int id)
        {
            LoginModel model = new LoginModel();
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

        public IActionResult login()
        {
            return (View());

        }
        public IActionResult logar(String txtlogin, String txtsenha, bool boolativo)
        {
            LoginModel model = (new LoginModel()).validarLogin(txtlogin, txtsenha);
            if (model == null)
            {
                //não encontrou:
                ViewBag.mensagem = "Dados inválidos";
                //ViewBag.class = "alert-danger";
                return View("login");
            }
            else if (model.ativo==true)
            {
                //encontrou
                HttpContext.Session.SetInt32("idLogin", model.id );
                HttpContext.Session.SetString("nomeLogin", model.login);
                HttpContext.Session.SetInt32("ativoTrue", model.ativo == true?1:0);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetInt32("idLogin", model.id);
                HttpContext.Session.SetString("nomeLogin", model.login);
                HttpContext.Session.SetInt32("ativoTrue", model.ativo == false ? 1 : 0);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult sair()
        {
            //O que ele faz: 
            //Limpa a sessão;
            HttpContext.Session.Clear();

            //Redireciona para o Login
            return RedirectToAction("login", "Login");

        }
    }
}
