using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        private UsuarioService usuarioService = new UsuarioService();

        private MasterEntities Db = new MasterEntities();

        public void CreateAndSaveOrder(Pedido p)
        {
            //creo un nuevo pedido

            //this.SignUpGuests(p.EmailUsuario);
            p.IdPedido = Db.Pedido.Max(x => x.IdPedido) + 1;
            p.FechaCreacion = DateTime.Now;
            p.IdEstadoPedido = 1;
            p.FechaModificacion = null;
            p.IdUsuarioResponsable = 1; //Placeholder, no tenemos sesion para levantar el idUsuario.
            Db.Pedido.Add(p);
            Db.SaveChanges();

            //para cada email de usuario, creo un nuevo objeto InvitacionPedido
            foreach (string email in p.EmailUsuario)
            {

                InvitacionPedido invitacionPedido = new InvitacionPedido();
                invitacionPedido.IdPedido = p.IdPedido;
                invitacionPedido.Pedido = p;
                //Si el usuario no existe, lo registra
                if (usuarioService.FindByEmail(email) == null)
                {
                    usuarioService.RegisterUserByEmail(email);
                }

                invitacionPedido.Usuario = usuarioService.FindByEmail(email);
                invitacionPedido.Token = Guid.Empty;//hay que generar el token
                //invitacionPedido.Token = new Guid(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                invitacionPedido.Completado = false;
                Db.InvitacionPedido.Add(invitacionPedido);
                Db.SaveChanges();
            }

            //para cada gusto de empanada y usuario, creo un objeto InvitacionPedidoGustoEmpanadaUsuario
            foreach (int idGusto in p.GustoEmpanadaDisponibles)
            {
                foreach (string email in p.EmailUsuario)
                {
                    InvitacionPedidoGustoEmpanadaUsuario invitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario();
                    invitacionPedidoGustoEmpanadaUsuario.IdInvitacionPedidoGustoEmpanadaUsuario =
                        Db.InvitacionPedidoGustoEmpanadaUsuario.Max(i => i.IdInvitacionPedidoGustoEmpanadaUsuario) + 1;
                    invitacionPedidoGustoEmpanadaUsuario.GustoEmpanada = Db.GustoEmpanada.Find(idGusto);
                    invitacionPedidoGustoEmpanadaUsuario.IdGustoEmpanada = idGusto;
                    invitacionPedidoGustoEmpanadaUsuario.Pedido = p;
                    invitacionPedidoGustoEmpanadaUsuario.IdPedido = p.IdPedido;
                    invitacionPedidoGustoEmpanadaUsuario.IdUsuario = usuarioService.FindByEmail(email).IdUsuario;
                    invitacionPedidoGustoEmpanadaUsuario.Usuario = usuarioService.FindById(invitacionPedidoGustoEmpanadaUsuario.IdUsuario);
                    invitacionPedidoGustoEmpanadaUsuario.Cantidad = 0;
                    Db.InvitacionPedidoGustoEmpanadaUsuario.Add(invitacionPedidoGustoEmpanadaUsuario);
                    Db.SaveChanges();

                }
            }


        }

        public List<Pedido> GetAll()
        {
            return new List<Pedido>();
        }

        public List<Pedido> GetAllByUser()
        {
            return new List<Pedido>();
        }

        //private void SignUpGuests(string[] emailInvitados)
        //{
        //    foreach(string email in emailInvitados)
        //    {
        //        Usuario newUser = new Usuario();
        //        newUser.Email = email;
        //        db.Usuario.Add(newUser);
        //    }
        //    db.SaveChanges();
        //}





    }

}