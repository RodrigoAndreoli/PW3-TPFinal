using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        private MasterEntities Db = new MasterEntities();

        public void CreateAndSaveOrder(Pedido Order)
        {
            this.SignUpGuests(Order.EmailUsuario);

            Order.IdPedido = Db.Pedido.Max(Element => Element.IdPedido) + 1;
            Order.FechaCreacion = DateTime.Now;
            Order.IdEstadoPedido = 1;
            Order.FechaModificacion = null;
            Order.IdUsuarioResponsable = 1; //Placeholder, no tenemos sesion para levantar el idUsuario.
            Db.Pedido.Add(Order);

            foreach (int IdGusto in Order.GustoEmpanadaDisponibles)
            {
                InvitacionPedidoGustoEmpanadaUsuario InvitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario();
                InvitacionPedidoGustoEmpanadaUsuario.GustoEmpanada = Db.GustoEmpanada.Find(IdGusto);
                InvitacionPedidoGustoEmpanadaUsuario.IdGustoEmpanada = IdGusto;
                InvitacionPedidoGustoEmpanadaUsuario.Pedido = Order;
                InvitacionPedidoGustoEmpanadaUsuario.IdPedido = Order.IdPedido;
                InvitacionPedidoGustoEmpanadaUsuario.IdUsuario = Order.IdUsuarioResponsable;
                Db.InvitacionPedidoGustoEmpanadaUsuario.Add(InvitacionPedidoGustoEmpanadaUsuario);
            }

            /// Aca se iran armando las invitaciones
            //foreach(int IdInvitado in Order.InvitacionPedido)
            //{
            //    InvitacionPedido Invitation = new InvitacionPedido();
            //    Invitation.IdPedido = Order.IdPedido;
            //    Invitation.Token = new Guid(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            //}

            Db.SaveChanges();
        }

        public List<Pedido> GetAll()
        {
            return new List<Pedido>();
        }

        public List<Pedido> GetAllByUser()
        {
            return new List<Pedido>();
        }

        private void SignUpGuests(string[ ] GuestsEmails)
        {
            foreach (string Email in GuestsEmails)
            {
                Usuario NewUser = new Usuario();
                NewUser.Email = Email;
                Db.Usuario.Add(NewUser);
            }

            Db.SaveChanges();
        }

    }

}