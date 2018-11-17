using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LasEmpanadas.Models;
using LasEmpanadas.Services;

namespace LasEmpanadas.Controllers
{

    public class PedidosController : Controller
    {
    static PedidoService PedidoService = new PedidoService();

        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Pedido p)
        {
            if (ModelState.IsValid)
            {
                PedidoService.CreateAndSaveOrder(p);
                return View(p);
            }
            else
            {
                return View(p);
            }            
        }

        public ActionResult Iniciado()
        {
            return View();
        }

        public ActionResult Lista()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }

        public ActionResult Eliminar()
        {
            return View();
        }

        public ActionResult Elegir()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

    }
}