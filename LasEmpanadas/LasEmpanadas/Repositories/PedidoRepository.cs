﻿using LasEmpanadas.Models;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class PedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        internal List<Pedido> GetAll() => Db.Pedido.ToList();

        internal Pedido FindOneById(int Id) => Db.Pedido.SingleOrDefault(Element => Element.IdPedido == Id);

        internal int GetNextId() => Db.Pedido.Max(Element => Element.IdPedido) + 1;

        internal void Close(Pedido Order)
        {
            Pedido OrderFromDb = FindOneById(Order.IdPedido);
            OrderFromDb.IdEstadoPedido = 2;
            Db.SaveChanges();
        }

        internal Pedido Create(Pedido Order)
        {
            Order.IdPedido = GetNextId();
            Db.Pedido.Add(Order);
            Db.SaveChanges();

            return Order;
        }

        internal Pedido Update(Pedido Order)
        {
            Pedido OrderFromDb = FindOneById(Order.IdPedido);
            OrderFromDb.NombreNegocio = Order.NombreNegocio;
            OrderFromDb.Descripcion = Order.Descripcion;
            OrderFromDb.PrecioUnidad = Order.PrecioUnidad;
            OrderFromDb.PrecioDocena = Order.PrecioDocena;
            OrderFromDb.FechaModificacion = Order.FechaModificacion;
            Db.SaveChanges();

            return OrderFromDb;
        }

        internal void Delete(Pedido Order)
        {
            Db.Pedido.Remove(Order);
            Db.SaveChanges();
        }

    }

}