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
            Pedido Pedido = new Pedido();
            Pedido.NombreNegocio = p.NombreNegocio;
            Pedido.PrecioDocena = p.PrecioDocena;
            Pedido.PrecioUnidad = p.PrecioUnidad;
            Pedido.IdEstadoPedido = 1;
            db.Pedido.Add(Pedido);

            foreach (int idGusto in p.GustoEmpanadaDisponibles)
            {
                InvitacionPedidoGustoEmpanadaUsuario InvitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario();
                InvitacionPedidoGustoEmpanadaUsuario.GustoEmpanada = db.GustoEmpanada.Find(idGusto);
                InvitacionPedidoGustoEmpanadaUsuario.IdGustoEmpanada = idGusto;
                InvitacionPedidoGustoEmpanadaUsuario.Pedido = p;
                InvitacionPedidoGustoEmpanadaUsuario.IdPedido = p.IdPedido;

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