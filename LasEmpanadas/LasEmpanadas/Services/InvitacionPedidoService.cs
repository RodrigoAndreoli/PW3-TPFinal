using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;

namespace LasEmpanadas.Services
{
    public class InvitacionPedidoService
    {
        InvitacionPedidoRepository InvitacionPedidoRepo = new InvitacionPedidoRepository();

        UsuarioService UsuarioSvc = new UsuarioService();
        EmailService EmailSvc = new EmailService();

        internal void Create(Pedido Order)
        {
            foreach (string Email in Order.EmailsInvitados)
            {
                InvitacionPedido Invitation = new InvitacionPedido
                {
                    Completado = false,
                    Token = Guid.NewGuid(),
                    IdUsuario = UsuarioSvc.GetIdFromEmail(Email),
                    IdPedido = Order.IdPedido
                };

                InvitacionPedidoRepo.Create(Invitation);
                EmailSvc.SendEmail(Email,Invitation.Token);
            }
        }

    }

}