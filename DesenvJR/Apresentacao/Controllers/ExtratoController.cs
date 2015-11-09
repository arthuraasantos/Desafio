using Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apresentacao.Controllers
{
    public class ExtratoController : Controller
    {
        private TransacaoRepository Repository { get; set; }

        public ExtratoController()
        {
            Repository = new TransacaoRepository();
        }
        //
        // GET: /Extrato/
        public ActionResult Listar()
        {
            var listaTransacoes = Repository.List();
            var saldo = listaTransacoes.Sum(b => b.Amount);
            ViewBag.Saldo = saldo;
            return View(listaTransacoes);
        }
	}
}