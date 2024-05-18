using AutoMapper;
using Newtonsoft.Json.Linq;
using Repositorio;
using Repositorio.Contexto;
using Repositorio.Entidades;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace CinemaxToledo.Models
{
    public class FilmeModel
    {
        [Display(Name = "Código")]
        public int id { get; set; }


        [Display(Name = "Título")]
        public String titulo { get; set; }

        [Display(Name = "Descrição")]
        public String descricao { get; set; }

        [Display(Name = "Início do Cartaz")]
        public DateTime inicioCartaz { get; set; }

        [Display(Name = "Final do Cartaz")]
        public DateTime finalCartaz { get; set; }

        [Display(Name = "Valor da Entrada Meia")]
        public Decimal valorMeia { get; set; }

        [Display(Name = "Valor da Entrada Inteira")]
        public Decimal valorInteira { get; set; }

        [Display(Name = "Duração")]
        public String duracao { get; set; }

        [Display(Name = "Está disponível?")]
        public bool estaDisponivel { get; set; }


        //vincular imagem ao filme
        [Display(Name = "Imagem")]
        public String imagem { get; set; }


        [Required(ErrorMessage="Imagem obrigatória")]
        public IFormFile arquivoImagem { get; set; }

        //chave estrangeira:

        //public int idVendaFilme { get; set; }
        // public int idAtoresFilme { get; set; }
        [Display(Name = "Categoria")]
        public int idCategoria { get; set; }

        [Display(Name = "Estúdio")]
        public int idEstudio { get; set; }

        [Display(Name = "Atores")]
        public int idAtores { get; set; }
        

        public PlataformaModel? categoria { get; set; }
        
        public EstudioModel? estudio { get; set; }
   
        public AtoresModel? atores { get; set; }

        public SessaoModel? sessao { get; set; }


        public virtual List<SessaoModel>? sessoes { get; set; }

        public FilmeModel salvar(FilmeModel model,
            IWebHostEnvironment webHostEnvironment)
        {

            //Categoria cat = new Categoria();
            //cat.id = model.id;
            //cat.descricao = model.descricao;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Filme cat = mapper.Map<Filme>(model);

            cat.imagem = Upload(model.arquivoImagem, webHostEnvironment);


            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                FilmeRepositorio repositorio =
                new FilmeRepositorio(contexto);

                if (model.id == 0)
                    repositorio.Inserir(cat);
                else
                    repositorio.Alterar(cat);

                contexto.SaveChanges();
            }
            model.id = cat.id;
            return model;


        }

        //lista sessão


        public List<FilmeModel> listar()
        {
            List<FilmeModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                FilmeRepositorio repositorio =
                    new FilmeRepositorio(contexto);
                List<Filme> lista = repositorio.ListarTodos();
                listamodel = mapper.Map<List<FilmeModel>>(lista);
            }

            return listamodel;
        }

        public FilmeModel selecionar(int id)
        {
            FilmeModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                FilmeRepositorio repositorio =
                    new FilmeRepositorio(contexto);
                //select * from categoria c where c.id = id
                Filme cat = repositorio.Recuperar(c => c.id == id);
                model = mapper.Map<FilmeModel>(cat);
            }
       
            return model;
        }

        public void excluir(int id)
        {

            using (CinemaxContexto contexto = new CinemaxContexto())
            {
                FilmeRepositorio repositorio =
                    new FilmeRepositorio(contexto);
                Filme cat = repositorio.Recuperar(c => c.id == id);
                repositorio.Excluir(cat);
                contexto.SaveChanges();
            }
        }

        private string Upload(IFormFile arquivoImagem, IWebHostEnvironment webHostEnvironment)
        {
            string nomeUnicoArquivo = null;
            if (arquivoImagem != null)
            {
                string pastaFotos = Path.Combine(webHostEnvironment.WebRootPath, "Imagens");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + arquivoImagem.FileName;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    arquivoImagem.CopyTo(fileStream);
                }
            }

            return nomeUnicoArquivo;
        }
    }
}
