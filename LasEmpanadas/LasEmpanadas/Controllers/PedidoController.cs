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
        EmailService EmailService = new EmailService();
        PedidoService PedidoSvc = new PedidoService();
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
            return View(p);
        }

        [HttpPost]
        public ActionResult Editar(PedidoCompletoDTO Pedido, int reenviar)
        {
            Pedido P = PedidoSvc.BuildPedido(Pedido);
            PedidoSvc.Edit(P);
            EmailService.SendEmailToManyUsers(Pedido.IdPedido, reenviar);
            return RedirectToAction("Index");
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

        public string ObtenerPedidoCompleto (int IdPedido)
        {
            List<GustoEmpanadaDTO> gustoEmpanadaDTO = new List<GustoEmpanadaDTO>();
            PedidoCompletoDTO p = PedidoSvc.ObtenerPedidoCompleto(IdPedido);

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