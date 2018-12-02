using System;
using System.Collections.Generic;
using LasEmpanadas.Models;
using LasEmpanadas.Repositories;

namespace LasEmpanadas.Services
{
    public class InvitacionPedidoGustoEmpanadaUsuarioService
    {
        InvitacionPedidoGustoEmpanadaUsuarioRepository InvitacionPedidoGustoEmpanadaUsuarioRepo;

        UsuarioService UsuarioSvc;
        private MasterEntities db;

        public InvitacionPedidoGustoEmpanadaUsuarioService()
        {
            this.db = new MasterEntities();
            InvitacionPedidoGustoEmpanadaUsuarioRepo = new InvitacionPedidoGustoEmpanadaUsuarioRepository(this.db);
            UsuarioSvc = new UsuarioService(db);
        }

        public InvitacionPedidoGustoEmpanadaUsuarioService(MasterEntities db)
        {
            this.db = db;
            InvitacionPedidoGustoEmpanadaUsuarioRepo = new InvitacionPedidoGustoEmpanadaUsuarioRepository(this.db);
            UsuarioSvc = new UsuarioService(db);
        }

        internal void Create(Pedido Order)
        {
            foreach (string Email in Order.EmailsInvitados)
            {
                foreach (int IdGusto in Order.GustoEmpanadaDisponibles)
                {
                    InvitacionPedidoGustoEmpanadaUsuario InvitationContent = new InvitacionPedidoGustoEmpanadaUsuario
                    {
                        Cantidad = 0,
                        IdGustoEmpanada = IdGusto,
                        IdPedido = Order.IdPedido,
                        IdUsuario = UsuarioSvc.GetIdFromEmail(Email)
                    };

                    InvitacionPedidoGustoEmpanadaUsuarioRepo.Create(InvitationContent);
                }
            }
        }

        public InvitacionPedidoGustoEmpanadaUsuario OpenInvitation(InvitacionPedido miInvitacion)
        {
            InvitacionPedidoGustoEmpanadaUsuario invAEditar = new InvitacionPedidoGustoEmpanadaUsuario();
            invAEditar.IdPedido = miInvitacion.IdPedido;
            invAEditar.IdUsuario = miInvitacion.IdUsuario;
            return invAEditar;
        }


        internal List<InvitacionPedidoGustoEmpanadaUsuario> FindAllByPedido(int? idPedido)
        {
            return InvitacionPedidoGustoEmpanadaUsuarioRepo.FindAllByPedido(idPedido);
        }

        internal List<InvitacionPedidoGustoEmpanadaUsuario> FindAllByPedido(int idPedido)
        {
            return InvitacionPedidoGustoEmpanadaUsuarioRepo.FindAllByPedido(idPedido);
        }

        internal void DeleteByOrder(Pedido Order)
        {
            List<InvitacionPedidoGustoEmpanadaUsuario> Rows = InvitacionPedidoGustoEmpanadaUsuarioRepo.FindAllByPedido(Order.IdPedido);
            foreach (var Row in Rows)
            {
                InvitacionPedidoGustoEmpanadaUsuarioRepo.Delete(Row);
            }
        }

    }

}