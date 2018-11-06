using LasEmpanadas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Services
{
    public class PedidoService
    {
        private MasterEntities db = new MasterEntities();

        public void Save(Pedido p)
        {
            p.IdEstadoPedido = 1;
            db.Pedido.Add(p);

            List<InvitacionPedidoGustoEmpanadaUsuario> relaciones = new List<InvitacionPedidoGustoEmpanadaUsuario>();

            foreach (int idGusto in p.GustoEmpanadaDisponibles)
            {
                InvitacionPedidoGustoEmpanadaUsuario InvitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario
                {
                    GustoEmpanada = db.GustoEmpanada.Find(idGusto),
                    IdGustoEmpanada = idGusto,
                    Pedido = p,
                    IdPedido = p.IdPedido
                };
                db.InvitacionPedidoGustoEmpanadaUsuario.Add(InvitacionPedidoGustoEmpanadaUsuario);
            }
            db.SaveChanges();

        }

        public List<Pedido> GetAll()
        {
            return new List<Pedido>();
        }

        public List<Pedido> GetAllByUser()
        {
            return new List<Pedido>();
        }
    }
}