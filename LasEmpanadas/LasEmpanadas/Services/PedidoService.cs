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

        public void CreateAndSaveOrder(Pedido p)
        {
            Pedido Pedido = new Pedido();
            Pedido.IdPedido = db.Pedido.Max(x => x.IdPedido) + 1;
            Pedido.NombreNegocio = p.NombreNegocio;
            Pedido.Descripcion = p.Descripcion;
            Pedido.PrecioDocena = p.PrecioDocena;
            Pedido.PrecioUnidad = p.PrecioUnidad;
            Pedido.Descripcion = p.Descripcion;
            Pedido.FechaCreacion = DateTime.Now;
            Pedido.IdUsuarioResponsable = 1;
            Pedido.IdEstadoPedido = 1;
            Pedido.FechaCreacion = DateTime.Now;
            Pedido.FechaModificacion = null;
            Pedido.IdUsuarioResponsable = 1; //Placeholder, no tenemos sesion para levantar el idUsuario.
            db.Pedido.Add(Pedido);
            db.SaveChanges();

            foreach (int idGusto in p.GustoEmpanadaDisponibles)
            {
                InvitacionPedidoGustoEmpanadaUsuario InvitacionPedidoGustoEmpanadaUsuario = new InvitacionPedidoGustoEmpanadaUsuario();
                InvitacionPedidoGustoEmpanadaUsuario.GustoEmpanada = db.GustoEmpanada.Find(idGusto);
                InvitacionPedidoGustoEmpanadaUsuario.IdGustoEmpanada = idGusto;
                InvitacionPedidoGustoEmpanadaUsuario.Pedido = Pedido;
                InvitacionPedidoGustoEmpanadaUsuario.IdPedido = Pedido.IdPedido;
                InvitacionPedidoGustoEmpanadaUsuario.IdUsuario = Pedido.IdUsuarioResponsable;
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