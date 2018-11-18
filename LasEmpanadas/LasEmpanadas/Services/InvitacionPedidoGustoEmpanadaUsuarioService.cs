using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LasEmpanadas.Models;
using LasEmpanadas.Repositories;

namespace LasEmpanadas.Services
{
    public class InvitacionPedidoGustoEmpanadaUsuarioService
    {
        InvitacionPedidoGustoEmpanadaUsuarioRepository InvitacionPedidoGustoEmpanadaUsuarioRepo = new InvitacionPedidoGustoEmpanadaUsuarioRepository();
        UsuarioService UsuarioSvc = new UsuarioService();
        UsuarioRepository UsuarioRepo = new UsuarioRepository();

        internal void Create(Pedido order)
        {
            foreach (string email in order.EmailsInvitados) {
                foreach (int idGusto in order.GustoEmpanadaDisponibles) {
                    InvitacionPedidoGustoEmpanadaUsuario invitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario
                    {
                        Cantidad = 0,
                        IdGustoEmpanada = idGusto,
                        IdPedido = order.IdPedido,
                        IdUsuario = UsuarioRepo.FindOneByEmail(email).IdUsuario,                        
                    };
                    InvitacionPedidoGustoEmpanadaUsuarioRepo.Create(invitacionPedidoGustoEmpanadaUsuario);
                }
            }
        }
    }
}