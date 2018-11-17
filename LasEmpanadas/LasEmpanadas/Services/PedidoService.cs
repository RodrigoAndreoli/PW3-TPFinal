using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        private MasterEntities db = new MasterEntities();

        public void CreateAndSaveOrder(Pedido p)
        {
            this.SignUpGuests(p.EmailUsuario);
            p.IdPedido = db.Pedido.Max(x => x.IdPedido) + 1;
            p.FechaCreacion = DateTime.Now;
            p.IdEstadoPedido = 1;
            p.FechaModificacion = null;
            p.IdUsuarioResponsable = 1; //Placeholder, no tenemos sesion para levantar el idUsuario.
            db.Pedido.Add(p);

            foreach (int idGusto in p.GustoEmpanadaDisponibles)
            {
                InvitacionPedidoGustoEmpanadaUsuario InvitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario();
                InvitacionPedidoGustoEmpanadaUsuario.GustoEmpanada = db.GustoEmpanada.Find(idGusto);
                InvitacionPedidoGustoEmpanadaUsuario.IdGustoEmpanada = idGusto;
                InvitacionPedidoGustoEmpanadaUsuario.Pedido = p;
                InvitacionPedidoGustoEmpanadaUsuario.IdPedido = p.IdPedido;
                InvitacionPedidoGustoEmpanadaUsuario.IdUsuario = p.IdUsuarioResponsable;
                db.InvitacionPedidoGustoEmpanadaUsuario.Add(InvitacionPedidoGustoEmpanadaUsuario);
            }
            /// Aca se iran armando las invitaciones
            //foreach(int idInvitado in p.InvitacionPedido)
            //{
            //    InvitacionPedido inv = new InvitacionPedido();
            //    inv.IdPedido = p.IdPedido;
            //    inv.Token = new Guid(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            //}
            db.SaveChanges();

        }

        public List<Pedido> GetAll()
        {
            return new List<Pedido>();
        }

        public List<Pedido> GetAllByUser()
        {
            return new List<Pedido>();
        }

        private void SignUpGuests(string[] emailInvitados)
        {
            foreach(string email in emailInvitados)
            {
                Usuario newUser = new Usuario();
                newUser.Email = email;
                db.Usuario.Add(newUser);
            }
            db.SaveChanges();
        }


    }
}