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
        InvitacionPedidoService InvPedidoSvc = new InvitacionPedidoService();
        InvitacionPedidoGustoEmpanadaUsuarioService InvPedidoGustoSvc = new InvitacionPedidoGustoEmpanadaUsuarioService();

        public ActionResult Iniciar()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
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

        public ActionResult Elegir(System.Guid token)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            InvitacionPedido miInvitacion = InvPedidoSvc.GetInvitationByToken(token);            
            Pedido PedidoAEditar = PedidoSvc.GetPedidoById(miInvitacion.IdPedido);
            InvitacionPedidoGustoEmpanadaUsuario invitacionAElegir = InvPedidoGustoSvc.OpenInvitation(miInvitacion);
            invitacionAElegir.Pedido = PedidoAEditar;
            return View(invitacionAElegir);
        }

        [HttpPost]
        public ActionResult Elegir(InvitacionPedidoGustoEmpanadaUsuario seleccionUsuario)
        {
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