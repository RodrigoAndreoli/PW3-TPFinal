using LasEmpanadas.Models;
using LasEmpanadas.Models.Views;
using LasEmpanadas.Repositories;
using System;

namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        PedidoRepository PedidoRepo = new PedidoRepository();

        UsuarioService UsuarioSvc = new UsuarioService();

        /// <summary>
        /// Crea y guarda un nuevo pedido
        /// </summary>
        /// <param name="OrderView"></param>
        internal void CreateOrder(Pedido p)
        {
            Pedido Order = new Pedido
            {
                IdPedido = PedidoRepo.GetNextId(),

                //Placeholder, no tenemos sesion para levantar el idUsuario.
                IdUsuarioResponsable = 1,

                NombreNegocio = p.NombreNegocio,
                Descripcion = p.Descripcion,

                //Inicializa el pedido en estado ABIERTO.
                IdEstadoPedido = 1,

                PrecioUnidad = p.PrecioUnidad,
                PrecioDocena = p.PrecioDocena,
                FechaCreacion = DateTime.Now,
                FechaModificacion = null
            };

            PedidoRepo.Create(Order);

            UsuarioSvc.CheckEmailList(p.EmailsInvitados);

            //para cada gusto de empanada y usuario, creo un objeto InvitacionPedidoGustoEmpanadaUsuario
            //foreach (int IdGusto in OrderView.GustosEmpanadas)
            //{
            //    foreach (string Email in OrderView.EmailUsuario)
            //    {
            //        InvitacionPedidoGustoEmpanadaUsuario InvitationQuantityPerFlavor = new InvitacionPedidoGustoEmpanadaUsuario();
            //        InvitationQuantityPerFlavor.IdInvitacionPedidoGustoEmpanadaUsuario =
            //        Db.InvitacionPedidoGustoEmpanadaUsuario.Max(Element => Element.IdInvitacionPedidoGustoEmpanadaUsuario) + 1;
            //        InvitationQuantityPerFlavor.GustoEmpanada = Db.GustoEmpanada.Find(IdGusto);
            //        InvitationQuantityPerFlavor.IdGustoEmpanada = IdGusto;
            //        InvitationQuantityPerFlavor.Pedido = OrderView;
            //        InvitationQuantityPerFlavor.IdPedido = OrderView.IdPedido;
            //        InvitationQuantityPerFlavor.IdUsuario = UsuarioSvc.FindByEmail(Email).IdUsuario;
            //        InvitationQuantityPerFlavor.Usuario = UsuarioSvc.FindById(InvitationQuantityPerFlavor.IdUsuario);
            //        InvitationQuantityPerFlavor.Cantidad = 0;
            //        Db.InvitacionPedidoGustoEmpanadaUsuario.Add(InvitationQuantityPerFlavor);
            //        Db.SaveChanges();

            //    }
            //}
        }

    }

}