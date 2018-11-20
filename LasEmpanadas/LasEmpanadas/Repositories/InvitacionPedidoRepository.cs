﻿using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class InvitacionPedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        internal List<InvitacionPedido> GetAll() => Db.InvitacionPedido.ToList();

        internal InvitacionPedido FindOneById(int Id) => Db.InvitacionPedido.SingleOrDefault(Element => Element.IdInvitacionPedido == Id);

        internal void Complete(InvitacionPedido Invitation)
        {
            InvitacionPedido InvitationFromDb = FindOneById(Invitation.IdInvitacionPedido);
            InvitationFromDb.Completado = true;
            Db.SaveChanges();
        }

        internal InvitacionPedido Create(InvitacionPedido Invitation)
        {
            Db.InvitacionPedido.Add(Invitation);
            Db.SaveChanges();

            return Invitation;
        }

        internal void Delete(InvitacionPedido Invitation)
        {
            Db.InvitacionPedido.Remove(Invitation);
            Db.SaveChanges();
        }

        internal List<InvitacionPedido> FindAllByPedidoId(int? idPedido)
        {
            return Db.InvitacionPedido.Where(x => x.IdPedido == idPedido).ToList();
        }

        internal List<InvitacionPedido> FindAllIncompleteByPedidoId(int idPedido)
        {
            return Db.InvitacionPedido.Where(x => (x.IdPedido == idPedido && x.Completado == false)).ToList();
        }
    }

}