using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infra.Repository;
using Infra.Entity;
using System.IO;


namespace Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        private TransacaoRepository Repository { get; set; }

        public HomeController()
        {
            Repository = new TransacaoRepository();
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NovaImportacao()
        {
            ViewBag.MsgErro = null;
            ViewBag.Mensagem = null;
            return View();
        }

        [HttpPost]
        public ActionResult ImportarArquivo(string url, HttpPostedFileBase arquivo)
        {
            try
            {
                if (arquivo == null)
                {
                    ViewBag.MsgErro = "Selecione o arquivo antes de realizar a importação";
                }
                else
                {
                    string pasta = HttpContext.Server.MapPath("~/Images/");
                    string nomeArquivo = "ofxFile" + DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + arquivo.FileName;
                    arquivo.SaveAs(pasta + nomeArquivo);
                    List<Transacao> listaTransacoes = new List<Transacao>();
                    listaTransacoes = ImportacaoRepository.LerArquivoOFX(pasta + nomeArquivo);
                    Repository.SaveList(listaTransacoes);
                }
                ModelState.Clear();
                ViewBag.Mensagem = "Importação realizada com sucesso";
                return View("NovaImportacao");
            }
            catch (FileNotFoundException ex)
            {
                ViewBag.MsgErro = @"Parece que não foi possível encontrar seu arquivo. Veja se a mensagem \n 
                                     lhe ajuda a identificar o motivo. Mensagem:  \n" + ex.Message;
                return View("Index");
                throw;
            }
            catch (Exception exs)
            {
                ViewBag.MsgErro = @"Que pena, um erro inesperado foi encontrado ao tentar importar. \n
                                     Entre em contato comigo agora(Tel: 21 974151478), precisamos resolver nosso problema.\n
                                     Caso queira tentar analisar o motivo, estou lhe passando a mensagem que gerou o problema. \n
                                     Mensagem: " + exs.Message;
                return View("Index");
            }
        }
    }
}