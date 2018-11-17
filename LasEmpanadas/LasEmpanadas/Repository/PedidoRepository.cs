using LasEmpanadas.Models;
using System.Linq;

namespace LasEmpanadas.Repository
{
    public class PedidoRepository
    {
        MasterEntities Db = new MasterEntities();

        internal int GetNextId() => Db.Pedido.Max(Element => Element.IdPedido) + 1;

        internal void Create(Pedido Order)
        {
            Db.Pedido.Add(Order);
            Db.SaveChanges();
        }
    }

}