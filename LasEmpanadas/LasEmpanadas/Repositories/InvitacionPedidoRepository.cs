using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Repositories
{
    public class InvitacionPedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        internal int GetNextId() => Db.InvitacionPedido.Max(Element => Element.IdInvitacionPedido) + 1;

        internal void Create(InvitacionPedido i)
        {
            i.IdInvitacionPedido = GetNextId();
            Db.InvitacionPedido.Add(i);
            Db.SaveChanges();
        }
    }
}