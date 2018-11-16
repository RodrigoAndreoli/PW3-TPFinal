using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LasEmpanadas.Models
{
    internal class PedidoMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PedidoMetadata()
        {
            this.InvitacionPedido = new HashSet<InvitacionPedido>();
            this.InvitacionPedidoGustoEmpanadaUsuario = new HashSet<InvitacionPedidoGustoEmpanadaUsuario>();
            this.GustoEmpanada = new HashSet<GustoEmpanada>();
        }

        public int IdPedido { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int IdEstadoPedido { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre del negocio")]
        public string NombreNegocio { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Precio por unidad")]
        public int PrecioUnidad { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Precio por docena")]
        public int PrecioDocena { get; set; }

        public System.DateTime FechaCreacion { get; set; }

        public Nullable<System.DateTime> FechaModificacion { get; set; }

        public virtual EstadoPedido EstadoPedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvitacionPedido> InvitacionPedido { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvitacionPedidoGustoEmpanadaUsuario> InvitacionPedidoGustoEmpanadaUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<GustoEmpanada> GustoEmpanada { get; set; }
    }
}
