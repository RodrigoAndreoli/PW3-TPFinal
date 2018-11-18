﻿using LasEmpanadas.Models;
using System;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class PedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        internal int GetNextId() => Db.Pedido.Max(Element => Element.IdPedido) + 1;        

        internal void Create(Pedido Order)
        {
            Order.IdPedido = GetNextId();
            Db.Pedido.Add(Order);
            Db.SaveChanges();
        }
    }

}