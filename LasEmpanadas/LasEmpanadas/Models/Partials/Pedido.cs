using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Models
{
    public partial class Pedido
    {
        public int[] GustoEmpanadaDisponibles { get; set; }
        public string[] EmailUsuario { get; set; }

    }
}