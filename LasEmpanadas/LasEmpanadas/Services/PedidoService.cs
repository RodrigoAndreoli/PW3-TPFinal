using LasEmpanadas.Models;
using LasEmpanadas.Repository;
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
        /// <param name="P"></param>
        internal void CreateOrder(Pedido P)
        {
            Pedido Order = new Pedido
            {
                IdPedido = PedidoRepo.GetNextId(),

                //Placeholder, no tenemos sesion para levantar el idUsuario.
                IdUsuarioResponsable = 1,

                NombreNegocio = P.NombreNegocio,
                Descripcion = P.Descripcion,

                //Inicializa el pedido en estado ABIERTO.
                IdEstadoPedido = 1,

                PrecioUnidad = P.PrecioUnidad,
                PrecioDocena = P.PrecioDocena,
                FechaCreacion = DateTime.Now,
                FechaModificacion = null
            };

            PedidoRepo.Create(Order);

            UsuarioSvc.CheckEmailList(P.EmailUsuario);

            //para cada gusto de empanada y usuario, creo un objeto InvitacionPedidoGustoEmpanadaUsuario
            //foreach (int IdGusto in P.GustoEmpanadaDisponibles)
            //{
            //    foreach (string Email in P.EmailUsuario)
            //    {
            //        InvitacionPedidoGustoEmpanadaUsuario InvitationQuantityPerFlavor = new InvitacionPedidoGustoEmpanadaUsuario();
            //        InvitationQuantityPerFlavor.IdInvitacionPedidoGustoEmpanadaUsuario =
            //        Db.InvitacionPedidoGustoEmpanadaUsuario.Max(Element => Element.IdInvitacionPedidoGustoEmpanadaUsuario) + 1;
            //        InvitationQuantityPerFlavor.GustoEmpanada = Db.GustoEmpanada.Find(IdGusto);
            //        InvitationQuantityPerFlavor.IdGustoEmpanada = IdGusto;
            //        InvitationQuantityPerFlavor.Pedido = P;
            //        InvitationQuantityPerFlavor.IdPedido = P.IdPedido;
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