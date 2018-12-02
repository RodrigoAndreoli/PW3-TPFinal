using LasEmpanadas.Models;
using LasEmpanadas.Models.DTO;
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
            db = new MasterEntities();
            InvitacionPedidoRepo = new InvitacionPedidoRepository(db);
            UsuarioSvc = new UsuarioService(db);
            EmailSvc = new EmailService();
        }

        public InvitacionPedidoService(MasterEntities db)
        {
            this.db = db;
            InvitacionPedidoRepo = new InvitacionPedidoRepository(db);
            UsuarioSvc = new UsuarioService(db);
            EmailSvc = new EmailService();
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
                EmailSvc.SendEmail(Email, Invitation.Token);
            }
            InvitacionPedido Responsible = new InvitacionPedido
            {
                Completado = false,
                Token = Guid.NewGuid(),
                IdUsuario = Order.IdUsuarioResponsable,
                IdPedido = Order.IdPedido
            };
            InvitacionPedidoRepo.Create(Responsible);
        }

        public InvitacionPedido GetInvitationById(int? id)
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

        internal List<InvitacionPedido> FindAllByPedidoId(int? idPedido)
        {
            return InvitacionPedidoRepo.FindAllByPedidoId(idPedido);
        }

        internal InvitacionPedido FindOneByToken(System.Guid token)
        {
            return InvitacionPedidoRepo.FindOneByToken(token);
        }

        internal void AddEmails(PedidoCompletoDTO Pedido)
        {
            if (Pedido.UsuariosNuevosString != null)
            {
                foreach (string Email in Pedido.UsuariosNuevosString)
                {
                    InvitacionPedido Invitation = new InvitacionPedido
                    {
                        Completado = false,
                        Token = Guid.NewGuid(),
                        IdUsuario = UsuarioSvc.GetIdFromEmail(Email),
                        IdPedido = Pedido.IdPedido
                    };

                    InvitacionPedidoRepo.Create(Invitation);
                }
            }
        }

        internal void DeleteByOrder(Pedido Order)
        {
            List<InvitacionPedido> Rows = InvitacionPedidoRepo.FindAllByPedidoId(Order.IdPedido);
            foreach (var Row in Rows)
            {
                InvitacionPedidoRepo.Delete(Row);
            }
        }

        public bool CheckUsuarioValidoByIDInvitacion(int idUser, int idInvitacion)
        {
            bool valido = false;
            if (InvitacionPedidoRepo.FindOneById(idInvitacion).IdUsuario == idUser)
            {
                valido = true;
            }
            return valido;
        }

        public bool CheckUsuarioValidoByTokenInvitacion(int idUser, System.Guid token)
        {
            bool valido = false;
            if (InvitacionPedidoRepo.FindOneByToken(token).IdUsuario == idUser)
            {
                valido = true;
            }
            return valido;
        }
    }

}