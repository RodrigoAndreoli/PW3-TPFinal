using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;


namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        PedidoRepository PedidoRepo = new PedidoRepository();
        LoginService Loginsvc = new LoginService();
        UsuarioService UsuarioSvc = new UsuarioService();
        InvitacionPedidoService InvitacionPedidoSvc = new InvitacionPedidoService();
        InvitacionPedidoGustoEmpanadaUsuarioService InvitacionPedidoGustoEmpanadaUsuarioSvc = new InvitacionPedidoGustoEmpanadaUsuarioService();


        /// <summary>
        /// Crea y guarda un nuevo pedido
        /// </summary>
        /// <param name="Order"></param>
        internal Pedido CreateOrder(Pedido Order)
        {
            //Placeholder, no tenemos sesion para levantar el idUsuario.
            Order.IdUsuarioResponsable = Loginsvc.GetLoggedUserId();
            //Inicializa el pedido en estado ABIERTO.
            Order.IdEstadoPedido = 1;
            Order.FechaCreacion = DateTime.Now;
            Order.FechaModificacion = null;
            Pedido CreatedOrder = PedidoRepo.Create(Order);

            //Chequeo la lista de emails.Si no existe, creo un usuario nuevo.
            UsuarioSvc.CheckEmailList(Order.EmailsInvitados);
            //Creo un nuevo registro en la tabla InvitacionPedido.
            InvitacionPedidoSvc.Create(Order);
            InvitacionPedidoGustoEmpanadaUsuarioSvc.Create(Order);
            return CreatedOrder;
        }

        internal List<Pedido> GetList()
        {
            List<Pedido> OrderList = PedidoRepo.GetAll();

            return OrderList;
        }

        internal Pedido GetPedidoById(int idPedido)
        {
            return PedidoRepo.FindOneById(idPedido);
        }

        internal List<GustoEmpanada> GetGustosDisponibles(int idPedido)
        {
            return PedidoRepo.GetGustoEmpanadasDisponibles(idPedido);
        }
        
    }

}

