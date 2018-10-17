using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LasEmpanadas.Models
{
    public class Pedido
    {
        private int id { get; set; }
        public string token { get; set; }
        [Display(Name ="Nombre del local")]
        public String nombre { get; set; }
        [Display(Name = "Precio por unidad")]
        public int precioUnidad { get; set; }
        [Display(Name = "Precio por docena")]
        public int precioDocena { get; set; }
        [Display(Name = "Gustos disponibles")]
        public List<string> gustosDisponibles { get; set; }
        public Usuario responsable { get; set; }
        [Display(Name = "Invitados")]
        public List<PedidoUsuario> invitados { get; set; }
        public enum estado {
            Abierto,
            Pendiente,
            Cerrado
        }

    }
}