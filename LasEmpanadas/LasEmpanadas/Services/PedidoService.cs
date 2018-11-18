using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
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
        /// <param name="OrderDTO"></param>
        internal Pedido CreateOrder(Pedido OrderDTO)
        {
            Pedido Order = new Pedido
            {
                IdPedido = PedidoRepo.GetNextId(),

                //Placeholder, no tenemos sesion para levantar el idUsuario.
                IdUsuarioResponsable = 1,

                NombreNegocio = OrderDTO.NombreNegocio,
                Descripcion = OrderDTO.Descripcion,

                //Inicializa el pedido en estado ABIERTO.
                IdEstadoPedido = 1,

                PrecioUnidad = OrderDTO.PrecioUnidad,
                PrecioDocena = OrderDTO.PrecioDocena,
                FechaCreacion = DateTime.Now,
                FechaModificacion = null
            };

            PedidoRepo.Create(Order);

            //Chequeo la lista de emails.Si no existe, creo un usuario nuevo.
            UsuarioSvc.CheckEmailList(Order.EmailsInvitados);

            //Creo un nuevo registro en la tabla InvitacionPedido.
            InvitacionPedidoSvc.Create(Order);

            InvitacionPedidoGustoEmpanadaUsuarioSvc.Create(Order);

            return Order;
        }

    }

}

