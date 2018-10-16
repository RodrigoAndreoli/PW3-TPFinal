using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Models
{
    public class Pedido
    {
        private int id { get; set; }
        private string token { get; set; }
        private String nombre { get; set; }
        private int precioUnidad { get; set; }
        private int precioDocena { get; set; }
        private List<string> gustosDisponibles { get; set; }
        private Usuario responsable { get; set; }
        private List<PedidoUsuario> invitados { get; set; }
        private enum estado {
            Abierto,
            Pendiente,
            Cerrado
        }

    }
}