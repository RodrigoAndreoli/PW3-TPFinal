using LasEmpanadas.Models;
using System.Collections.Generic;
using System.Linq;

namespace LasEmpanadas.Repositories
{
    public class PedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        internal List<Pedido> GetAll() => Db.Pedido.ToList();

        internal Pedido FindOneById(int? Id) {
            Pedido p = Db.Pedido.Where(Element => Element.IdPedido == Id).FirstOrDefault();
            return p;
                }

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

        internal void DeleteById(int? id) {

            Pedido pedido = Db.Pedido.Where(p => p.IdPedido == id).FirstOrDefault();     
            Db.Pedido.Remove(pedido);
            Db.SaveChanges();
        }
            

           
        

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