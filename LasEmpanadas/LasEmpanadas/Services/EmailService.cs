using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;

namespace LasEmpanadas.Services
{
    public class EmailService
    {
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
               "Nueva invitacion a pedido", "http://localhost:52521/Pedido/Elegir?token="+token.ToString());
                return 1;
            } catch (Exception)
            {
                return 0;
            }
        }

        internal void ResendEmails(PedidoCompletoDTO Pedido)
        {
            switch (Pedido.Reenviar)
            {
                case 2:
                    foreach(Usuario u in Pedido.usuarios)
                    {
                        InvitacionPedido i = Pedido.invitaciones.Find(x => x.IdPedido == Pedido.IdPedido);
                        SendEmail(u.Email, i.Token);
                    }
                    foreach (Usuario u in Pedido.usuariosNuevos)
                    {
                        InvitacionPedido i = Pedido.invitaciones.Find(x => x.IdPedido == Pedido.IdPedido);
                        SendEmail(u.Email, i.Token);
                    }
                    break;

                case 3:
                    foreach (Usuario u in Pedido.usuariosNuevos)
                    {

                    }
                    break;

                case 4:
                    foreach (Usuario u in Pedido.usuariosNuevos)
                    {

                    }
                    break;
            }
        }
    }
}