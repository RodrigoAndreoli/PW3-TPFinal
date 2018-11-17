using System.ComponentModel.DataAnnotations;

namespace LasEmpanadas.Models
{
    [MetadataType(typeof(PedidoMetadata))]
    public partial class Pedido
    {
        public int[] GustoEmpanadaDisponibles
        {
            get; set;
        }
        public string[] EmailsInvitados { get; set; }


    }

}