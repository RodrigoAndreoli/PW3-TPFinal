using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Models.Views
{
    public class PedidoView
    {
        [Required(ErrorMessage = "Requerido")]
        public string NombreNegocio { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int PrecioUnidad { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int PrecioDocena { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public int[] GustosEmpanadas { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string[] EmailsInvitados { get; set; }

    }
}