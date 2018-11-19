﻿using LasEmpanadas.Models;
using LasEmpanadas.Repositories;

namespace LasEmpanadas.Services
{
    public class InvitacionPedidoGustoEmpanadaUsuarioService
    {
        InvitacionPedidoGustoEmpanadaUsuarioRepository InvitacionPedidoGustoEmpanadaUsuarioRepo = new InvitacionPedidoGustoEmpanadaUsuarioRepository();

        UsuarioService UsuarioSvc = new UsuarioService();

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
        
    }

}