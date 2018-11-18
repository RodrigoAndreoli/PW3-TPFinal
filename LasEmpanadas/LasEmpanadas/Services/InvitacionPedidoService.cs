using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LasEmpanadas.Models;
using LasEmpanadas.Repositories;

namespace LasEmpanadas.Services
{
    public class InvitacionPedidoService
    {
        UsuarioRepository usuarioRepo = new UsuarioRepository();
        InvitacionPedidoRepository invitacionPedidoRepo = new InvitacionPedidoRepository();

        internal void Create(Pedido p)
        {
            foreach (string email in p.EmailsInvitados) {
                InvitacionPedido invitacionPedido = new InvitacionPedido
                {
                    Completado = false,
                    Token = Guid.Empty,
                    IdUsuario = usuarioRepo.FindOneByEmail(email).IdUsuario,
                    IdPedido = p.IdPedido,                    
                };
                invitacionPedidoRepo.Create(invitacionPedido);
            }
        }
    }
}