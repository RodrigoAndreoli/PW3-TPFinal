using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LasEmpanadas.Models.DTO
{
    public class ConfirmarcionGustoDTO
    {
        public int IdUsuario { get; set; }
        public System.Guid Token { get; set; }
        public List<GustosEmpanadasCantidad> GustosEmpanadasCantidad { get; set; }
    }
}