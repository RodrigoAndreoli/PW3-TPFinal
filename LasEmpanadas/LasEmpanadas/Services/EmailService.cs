using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace LasEmpanadas.Services
{
    public class EmailService
    {

        internal int SendConfirmMail(string Email,String Mensaje)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("lasempanadas.empanadas@gmail.com",
               "@Test1234");
            try
            {
                smtp.Send("lasempanadas.empanadas@gmail.com", Email,
                   "Confirmacion de pedido",Mensaje
                   );
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


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
                   "Nueva invitacion a pedido", "http://localhost:52521/Pedido/Elegir?token=" + token.ToString());
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal void ResendEmails(PedidoCompletoDTO Pedido)
        {
            List<Usuario> usuarios = new List<Usuario>();
            foreach (InvitacionPedido inv in Pedido.invitaciones)
            {
                usuarios.Add(inv.Usuario);
            }
            switch (Pedido.Reenviar)
            {
                case 2:
                    foreach (Usuario u in usuarios)
                    {
                        InvitacionPedido i = Pedido.invitaciones.Find(x => x.IdPedido == Pedido.IdPedido);
                        SendEmail(u.Email, i.Token);
                    }
                    if (Pedido.UsuariosNuevosString != null)
                    {
                        foreach (string u in Pedido.UsuariosNuevosString)
                        {
                            InvitacionPedido i = Pedido.invitaciones.Find(x => x.IdPedido == Pedido.IdPedido);
                            SendEmail(u, i.Token);
                        }
                    }
                    break;

                case 3:
                    if (Pedido.UsuariosNuevosString != null)
                    {
                        foreach (string u in Pedido.UsuariosNuevosString)
                        {
                            InvitacionPedido i = Pedido.invitaciones.Find(x => x.IdPedido == Pedido.IdPedido);
                            SendEmail(u, i.Token);
                        }
                    }
                    break;

                case 4:
                    foreach (Usuario u in usuarios)
                    {
                        InvitacionPedido i = Pedido.invitaciones.Find(x => x.IdPedido == Pedido.IdPedido);
                        if (i.Completado == false)
                        {
                            SendEmail(u.Email, i.Token);
                        }
                    }
                    break;
            }
        }
    }
}