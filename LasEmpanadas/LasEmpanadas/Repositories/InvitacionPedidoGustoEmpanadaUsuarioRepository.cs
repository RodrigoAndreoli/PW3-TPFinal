using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LasEmpanadas.Models;

namespace LasEmpanadas.Repositories
{
    public class InvitacionPedidoGustoEmpanadaUsuarioRepository
    {
        MasterEntities Db = new MasterEntities();

        internal int GetNextId() => Db.InvitacionPedidoGustoEmpanadaUsuario.Max(Element => Element.IdInvitacionPedidoGustoEmpanadaUsuario) + 1;

        internal void Create(InvitacionPedidoGustoEmpanadaUsuario i)
        {
            i.IdInvitacionPedidoGustoEmpanadaUsuario = GetNextId();
            Db.InvitacionPedidoGustoEmpanadaUsuario.Add(i);
            Db.SaveChanges();
        }
    }
}