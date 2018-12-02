using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using LasEmpanadas.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class PedidoController : Controller
    {
        PedidoService PedidoSvc = new PedidoService();
        InvitacionPedidoGustoEmpanadaUsuarioService InvPedidoGustoSvc = new InvitacionPedidoGustoEmpanadaUsuarioService();
        InvitacionPedidoService InvitacionPedidoService = new InvitacionPedidoService();
        GustoEmpanadaService GustoEmpanadaService = new GustoEmpanadaService();
        public ActionResult Iniciar()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.TodosLosGustos = GustoEmpanadaService.GetAllAsView();
            ViewBag.LosGustos = GustoEmpanadaService.FindAll();
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

        public ActionResult Lista(int? IdUser)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Pedido> OrderList = PedidoSvc.FindPedidosByUser(IdUser);
            return View(OrderList);
        }

        public ActionResult Editar(int? IdPedido)
        {
            PedidoCompletoDTO p = PedidoSvc.ObtenerPedidoCompleto(IdPedido);
            ViewBag.TodosLosGustos = GustoEmpanadaService.GetAllAsView();
            return View(p);
        }

        [HttpPost]
        public ActionResult Editar(PedidoCompletoDTO Pedido, int EnvioDeEmail)
        {
            Pedido P = PedidoSvc.BuildPedido(Pedido);
            PedidoSvc.Edit(P);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int? IdPedido)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            PedidoCompletoDTO OrderDTO = PedidoSvc.ObtenerPedidoCompleto(IdPedido);
            return View(OrderDTO);
        }

        public ActionResult EliminarConfirmado(int IdPedido)
        {
            Pedido Order = PedidoSvc.FindOneById(IdPedido);
            PedidoSvc.DeleteOrder(Order);
            return RedirectToAction("Lista");
        }
        /// <summary>
        /// TODO: Tengo que pasar todo el choclo a un servicio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ElegirGustos(int id)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int idUser = int.Parse(Session["idUser"].ToString());
            if (InvitacionPedidoService.CheckUsuarioValidoByIDInvitacion(idUser, id))
            {
                InvitacionPedido miInvitacion = InvitacionPedidoService.GetInvitationById(id);
                Pedido PedidoAEditar = PedidoSvc.GetPedidoById(miInvitacion.IdPedido);
                PedidoAEditar.GustoEmpanada = PedidoSvc.GetGustosDisponibles(miInvitacion.IdPedido);
                InvitacionPedidoGustoEmpanadaUsuario invitacionAUtilizar = InvPedidoGustoSvc.OpenInvitation(miInvitacion);
                invitacionAUtilizar.Pedido = PedidoAEditar;
                return View("Elegir", invitacionAUtilizar);
            }
            return RedirectToAction("Lista", new { idUser = idUser});
        }
        /// <summary>
        /// TODO: Tengo que pasar todo el choclo a un servicio
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ActionResult Elegir(System.Guid token)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int idUser = int.Parse(Session["idUser"].ToString());
            if (InvitacionPedidoService.CheckUsuarioValidoByTokenInvitacion(idUser, token))
            {
                InvitacionPedido miInvitacion = InvitacionPedidoService.GetInvitationByToken(token);
                Pedido PedidoAEditar = PedidoSvc.GetPedidoById(miInvitacion.IdPedido);
                PedidoAEditar.GustoEmpanada = PedidoSvc.GetGustosDisponibles(miInvitacion.IdPedido);
                InvitacionPedidoGustoEmpanadaUsuario invitacionAUtilizar = InvPedidoGustoSvc.OpenInvitation(miInvitacion);
                invitacionAUtilizar.Pedido = PedidoAEditar;
                return View(invitacionAUtilizar);
            }
            return RedirectToAction("Lista", new { idUser = idUser });
        }

        [HttpPost]
        public ActionResult Elegir(InvitacionPedido invitacionCompletada)
        {
            InvitacionPedidoService.CompletarInvitacion(invitacionCompletada.Token);
            return RedirectToAction("Lista", new { idUser = invitacionCompletada.IdUsuario });
        }

        public ActionResult Detalle(int? IdPedido)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            PedidoCompletoDTO OrderDTO = PedidoSvc.ObtenerPedidoCompleto(IdPedido);

            return View(OrderDTO);
        }

        public string ObtenerPedidoCompleto (int IdPedido)
        {
            List<GustoEmpanadaDTO> gustoEmpanadaDTO = new List<GustoEmpanadaDTO>();
            PedidoCompletoDTO p = PedidoSvc.ObtenerPedidoCompleto(IdPedido);
            List<InvitacionPedido> invitaciones = InvitacionPedidoService.FindAllByPedidoId(IdPedido);

            foreach (GustoEmpanada g in p.gustoEmpanadas){
                GustoEmpanadaDTO ge = new GustoEmpanadaDTO();
                ge.Id = g.IdGustoEmpanada;
                ge.Gusto = g.Nombre;
                gustoEmpanadaDTO.Add(ge);
            }
            string jsonArray = JsonConvert.SerializeObject(gustoEmpanadaDTO);

            return jsonArray;
        }


    }

}