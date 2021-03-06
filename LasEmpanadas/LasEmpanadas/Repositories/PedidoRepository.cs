﻿using LasEmpanadas.Models;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class PedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        public PedidoRepository(MasterEntities db)
        {
            Db = db;
        }

        internal List<Pedido> GetAll() => Db.Pedido.ToList();

        internal Pedido FindOneById(int? Id) => Db.Pedido.SingleOrDefault(Element => Element.IdPedido == Id);

        internal int GetNextId() => Db.Pedido.Max(Element => Element.IdPedido) + 1;

        internal List<GustoEmpanada> GetGustoEmpanadasDisponibles(int? idPedido)
        {
            Pedido p = Db.Pedido.Single(e => e.IdPedido == idPedido);
            List<GustoEmpanada> gustos = new List<GustoEmpanada>();
            foreach(GustoEmpanada gusto in p.GustoEmpanada)
            {
                gustos.Add(gusto);
            }
            return gustos;
        }

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

        internal void Cerrar(int id)
        {
            Pedido pedidoACerrar = Db.Pedido.Find(id);
            pedidoACerrar.IdEstadoPedido = 2;
            string[] email = new string[pedidoACerrar.InvitacionPedido.Count];
            int[] gustos = new int[pedidoACerrar.GustoEmpanada.Count];
            for(int i = 0; i < pedidoACerrar.InvitacionPedido.Count; i++)
            {
                email[i] = pedidoACerrar.InvitacionPedido.ElementAt(i).Usuario.Email;
            }
            for (int i = 0; i < pedidoACerrar.GustoEmpanada.Count; i++)
            {
                gustos[i] = pedidoACerrar.GustoEmpanada.ElementAt(i).IdGustoEmpanada;
            }
            pedidoACerrar.GustoEmpanadaDisponibles = gustos;
            pedidoACerrar.EmailsInvitados = email;
            Db.SaveChanges();
        }

        //internal Pedido Attach(Pedido Order)
        //{
        //    Db.Pedido.Attach(Order);
        //    foreach(var Gusto in Order.GustoEmpanada)
        //    {
        //        Db.GustoEmpanada.Attach(Gusto);
        //    }
        //    Db.SaveChanges();

        //    return Order;
        //}

        public List<Pedido> FindPedidosByUser(int? IdUser)
        {
            List<Pedido> MisPedidos = Db.Pedido.Where(x => x.IdUsuarioResponsable == IdUser).ToList();
            List<InvitacionPedido> MisPedidosInvolucrados = Db.InvitacionPedido.Where(x => x.IdUsuario == IdUser).ToList();

            List<int> PedidosReferidosAMi = new List<int>();

            foreach (Pedido p in MisPedidos){
                if (PedidosReferidosAMi != null && !PedidosReferidosAMi.Contains(p.IdPedido))
                {
                    PedidosReferidosAMi.Add(p.IdPedido);
                }
            }

            foreach (InvitacionPedido p in MisPedidosInvolucrados)
            {
                if (PedidosReferidosAMi != null && !PedidosReferidosAMi.Contains(p.IdPedido))
                {
                    PedidosReferidosAMi.Add(p.IdPedido);
                }
            }

            return Db.Pedido.Where(x => PedidosReferidosAMi.Contains(x.IdPedido)).ToList();

        }

    }

}