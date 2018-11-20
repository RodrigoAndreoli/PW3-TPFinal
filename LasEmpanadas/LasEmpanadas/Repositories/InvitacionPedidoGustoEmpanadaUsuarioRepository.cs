using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class InvitacionPedidoGustoEmpanadaUsuarioRepository
    {
        MasterEntities Db = new MasterEntities();

        internal List<InvitacionPedidoGustoEmpanadaUsuario> GetAll() => Db.InvitacionPedidoGustoEmpanadaUsuario.ToList();

        internal InvitacionPedidoGustoEmpanadaUsuario FindOneById(int Id) => Db.InvitacionPedidoGustoEmpanadaUsuario.SingleOrDefault(Element => Element.IdInvitacionPedidoGustoEmpanadaUsuario == Id);

        internal InvitacionPedidoGustoEmpanadaUsuario Create(InvitacionPedidoGustoEmpanadaUsuario InvitationContent)
        {
            Db.InvitacionPedidoGustoEmpanadaUsuario.Add(InvitationContent);
            Db.SaveChanges();

            return InvitationContent;
        }

        internal InvitacionPedidoGustoEmpanadaUsuario Update(InvitacionPedidoGustoEmpanadaUsuario InvitationContent)
        {
            InvitacionPedidoGustoEmpanadaUsuario InvitationContentFromDb = FindOneById(InvitationContent.IdPedido);
            InvitationContentFromDb.Cantidad = InvitationContent.Cantidad;
            Db.SaveChanges();

            return InvitationContentFromDb;
        }

        internal void Delete(InvitacionPedidoGustoEmpanadaUsuario InvitationContent)
        {
            Db.InvitacionPedidoGustoEmpanadaUsuario.Remove(InvitationContent);
            Db.SaveChanges();
        }

        internal List<InvitacionPedidoGustoEmpanadaUsuario> FindAllByPedido(int? idPedido)
        {
            return Db.InvitacionPedidoGustoEmpanadaUsuario.Where(x => x.IdPedido == idPedido).ToList();
        }

    }

}