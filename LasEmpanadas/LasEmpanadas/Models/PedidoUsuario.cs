using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Models
{
    public class PedidoUsuario
    {
        public int id { get; set; }
        public Usuario usuario { get; set; }
        public List<Tuple<int, string>> cantidadYGusto { get; set; } // Cantidad, Gusto
        public enum seleccionLista { SI, NO }
    }   
}