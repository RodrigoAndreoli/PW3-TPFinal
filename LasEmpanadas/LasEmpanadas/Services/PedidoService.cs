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
        /// <param name="OrderView"></param>
        internal void CreateOrder(Pedido order)
        {
            //creo un nuevo registro en la tabla PedidoRepo
            order.IdUsuarioResponsable = 1;
            order.IdEstadoPedido = 1;
            order.FechaCreacion = DateTime.Now;
            PedidoRepo.Create(order);
            //chequeo la lista de emails.Si no existe, creo un usuario nuevo
            UsuarioSvc.CheckEmailList(order.EmailsInvitados);
            //creo un nuevo registro en la tabla InvitacionPedido
            InvitacionPedidoSvc.Create(order);
            InvitacionPedidoGustoEmpanadaUsuarioSvc.Create(order);
        }
    }
    }

