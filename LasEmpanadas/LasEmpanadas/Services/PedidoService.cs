using LasEmpanadas.Models;
using LasEmpanadas.Models.Views;
using LasEmpanadas.Repositories;
using LasEmpanadas.Services;
using System;

namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        PedidoRepository PedidoRepo = new PedidoRepository();

        UsuarioService UsuarioSvc = new UsuarioService();
        InvitacionPedidoService InvitacionPedidoSvc = new InvitacionPedidoService();
        InvitacionPedidoGustoEmpanadaUsuarioService InvitacionPedidoGustoEmpanadaUsuarioSvc = new InvitacionPedidoGustoEmpanadaUsuarioService();


        /// <summary>
        /// Crea y guarda un nuevo pedido
        /// </summary>
        /// <param name="Order"></param>
        internal void CreateOrder(Pedido Order)
        {
            //Creo un nuevo registro en la tabla Pedido
            Order.IdUsuarioResponsable = 1;
            Order.IdEstadoPedido = 1;
            Order.FechaCreacion = DateTime.Now;
            PedidoRepo.Create(Order);

            //Chequeo la lista de emails.Si no existe, creo un usuario nuevo.
            UsuarioSvc.CheckEmailList(Order.EmailsInvitados);

            //Creo un nuevo registro en la tabla InvitacionPedido.
            InvitacionPedidoSvc.Create(Order);
            InvitacionPedidoGustoEmpanadaUsuarioSvc.Create(Order);
        }

    }

}

