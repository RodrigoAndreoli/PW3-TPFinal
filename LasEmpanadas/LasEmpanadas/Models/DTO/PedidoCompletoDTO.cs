using System;
using System.Collections.Generic;

namespace LasEmpanadas.Models.DTO
{
    public class PedidoCompletoDTO
    {
        public int IdPedido { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public string NombreNegocio { get; set; }
        public string Descripcion { get; set; }
        public int IdEstadoPedido { get; set; }
        public int PrecioUnidad { get; set; }
        public int PrecioDocena { get; set; }
        public int Reenviar { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }

        public List<InvitacionPedidoGustoEmpanadaUsuario> CantidadEmpanadasPorGustosYUsuarios { get; set; }
        public List<InvitacionPedido> invitaciones { get; set; }
        public List<Usuario> usuarios { get; set; }
        public List<Usuario> usuariosNuevos { get; set; }
        public List<GustoEmpanada> gustoEmpanadas { get; set; }
    }
}