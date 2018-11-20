using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;

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

        internal InvitacionPedido FindOneByUserId(int idUsuario)
        {
            return InvitacionPedidoRepo.FindOneById(idUsuario);
        }

        internal List<InvitacionPedido> FindOneByPedidoId(int? idPedido)
        {
            return InvitacionPedidoRepo.FindOneByPedidoId(idPedido);
        }

        internal int CountCompleteById(int? id)
        {
            return InvitacionPedidoRepo.FindAllByOrder(id).Count;
        }
    }

}