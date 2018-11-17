using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Models
{
    //[MetadataType(typeof(PedidoMetadata))]
    public partial class Pedido
    {
        public int[] GustoEmpanadaDisponibles { get; set; }
        public string[] EmailUsuario { get; set; }

    }
}