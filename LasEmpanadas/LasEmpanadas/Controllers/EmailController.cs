using LasEmpanadas.Models;
using LasEmpanadas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LasEmpanadas.Controllers
{
    public class EmailController : Controller
    {
        InvitacionPedidoService InvitacionPedidoService = new InvitacionPedidoService();
        UsuarioService UsuarioService = new UsuarioService();
        EmailService EmailService = new EmailService();
        // GET: Email
        public int sendEmail(int idUsuario)
        {
            InvitacionPedido invitacionPedido = InvitacionPedidoService.findOneByUserId(idUsuario);
            Usuario Usuario = UsuarioService.FindOneById(idUsuario);
            return EmailService.SendEmail(Usuario.Email, invitacionPedido.Token);
        }
    }
}