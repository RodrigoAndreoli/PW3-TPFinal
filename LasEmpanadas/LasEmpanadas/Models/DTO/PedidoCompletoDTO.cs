using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public System.DateTime FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }

        public List<Usuario> usuarios { get; set; }
        public List<GustoEmpanada> gustoEmpanadas { get; set; }
    }
}