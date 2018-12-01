using LasEmpanadas.Models;
using LasEmpanadas.Repositories;
using System;
using System.Collections.Generic;

namespace LasEmpanadas.Services
{
    public class InvitacionPedidoService
    {
        InvitacionPedidoRepository InvitacionPedidoRepo;

        UsuarioService UsuarioSvc;
        EmailService EmailSvc;
        private MasterEntities db;

        public InvitacionPedidoService()
        {
            this.db = new MasterEntities();
            this.InvitacionPedidoRepo = new InvitacionPedidoRepository(db);
            this.UsuarioSvc = new UsuarioService(db);
            this.EmailSvc = new EmailService();
        }

        public InvitacionPedidoService(MasterEntities db)
        {
            this.db = db;
            this.InvitacionPedidoRepo = new InvitacionPedidoRepository(db);
            this.UsuarioSvc = new UsuarioService(db);
            this.EmailSvc = new EmailService();
        }

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

        public InvitacionPedido GetInvitationById(int id)
        {
            return InvitacionPedidoRepo.FindOneById(id);
        }
        public InvitacionPedido GetInvitationByToken(System.Guid token)
        {
            return InvitacionPedidoRepo.FindOneByToken(token);
        }

        internal InvitacionPedido FindOneByUserId(int idUsuario)
        {
            return InvitacionPedidoRepo.FindOneById(idUsuario);
        }

        internal List<InvitacionPedido> FindOneByPedidoId(int? idPedido)
        {
            return InvitacionPedidoRepo.FindOneByPedidoId(idPedido);
        }

        internal void DeleteByOrder(Pedido Order)
        {
            List<InvitacionPedido> Rows = InvitacionPedidoRepo.FindAllByPedido(Order.IdPedido);
            foreach (var Row in Rows)
            {
                InvitacionPedidoRepo.Delete(Row);
            }
        }

    }

}