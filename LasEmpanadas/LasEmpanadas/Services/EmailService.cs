using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using LasEmpanadas.Models;

namespace LasEmpanadas.Services
{
    public class EmailService
    {
        InvitacionPedidoService InvitacionPedidoService = new InvitacionPedidoService();
        UsuarioService UsuarioService = new UsuarioService();
        internal int SendEmail(string email, Guid token)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("lasempanadas.empanadas@gmail.com",
               "@Test1234");
            try
            {
            smtp.Send("lasempanadas.empanadas@gmail.com", email,
               "Nueva invitacion a pedido", "http://localhost:52521/Pedido/Elegir/"+token.ToString());
                return 1;
            } catch (Exception)
            {
                return 0;
            }
        }

        internal void SendEmailToManyUsers(int idPedido, int reenviar)
        {
            List<Usuario> Usuarios = UsuarioService.FindAll();
            List<InvitacionPedido> invitaciones = new List<InvitacionPedido>();
            switch (reenviar)
            {
                case 1:
                    return;
                    break;

                case 2:
                    // Enviar a todos
                    invitaciones = InvitacionPedidoService.FindAllByPedidoId(idPedido);
                    foreach (Usuario u in Usuarios)
                    {
                        InvitacionPedido InvitacionPedido = invitaciones.Find(x => x.IdUsuario == u.IdUsuario);
                        SendEmail(u.Email, InvitacionPedido.Token);
                    }
                    break;

                case 3:
                    break;

                case 4:
                    invitaciones = InvitacionPedidoService.FindAllIncompleteByPedidoId(idPedido);
                    break;
            }
        }
    }
}