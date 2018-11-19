using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class PedidoController : Controller
    {
        PedidoService PedidoSvc = new PedidoService();

        public ActionResult Iniciar()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Pedido Order)
        {

            if (ModelState.IsValid)
            {
                Pedido CreatedOrder = PedidoSvc.CreateOrder(Order);
                return RedirectToAction("Iniciado", new { id = CreatedOrder.IdPedido });
            }
            else
            {
                return View(Order);
            }
        }

        public ActionResult Iniciado()
        {
            return View();
        }

        public ActionResult Lista()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Pedido> OrderList = PedidoSvc.GetList();
            return View(OrderList);
        }

        public ActionResult Editar()
        {
            return View();
        }

        public ActionResult Eliminar()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult Elegir()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult Detalle()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

    }

}