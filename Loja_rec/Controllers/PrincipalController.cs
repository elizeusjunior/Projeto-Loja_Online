using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loja_rec.Models;

namespace Loja_rec.Controllers
{
    public class PrincipalController : Controller
    {

        // GET: Principal
        public ActionResult Index()
        {
            return RedirectToAction("Listar");
        }

        public ActionResult Inserir()
        {
            return View();
        }


        [HttpPost] //atributos
        public ActionResult Inserir(string descricao, int preco, int quantidade, string sku)
        {
            Produto p = new Produto();
            p.Descricao = descricao;
            p.Preco = preco;
            p.Quantidade = quantidade;
            p.Sku = sku;
            string msg = p.inserir();
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }

        public ActionResult Listar()
        {
            List<Produto> lista = Produto.ListarProduto();
            return View(lista);
        }

        public ActionResult Excluir(int id)
        {
            Produto p = new Produto();
            p.Id = id;
            string msg = p.Excluir();
            TempData["msg"] = msg;
            return RedirectToAction("Listar");
        }

        public ActionResult Editar(int id)
        {
            var prod = new Produto();
            prod.Id = id;
            var produto = prod.getById();
            return View(produto);
        }
            
        [HttpPost]
        public ActionResult Editar(int id, string descricao, double preco, int quantidade, string sku)
        {
            var prod = new Produto();
            prod.Id = id;
            prod.Descricao = descricao;
            prod.Preco = preco;
            prod.Quantidade = quantidade;
            prod.Sku = sku;
            string msg = prod.Editar();
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }
    }
}