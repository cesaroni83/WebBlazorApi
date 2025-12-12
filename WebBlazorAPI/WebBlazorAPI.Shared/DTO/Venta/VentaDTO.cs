using System.ComponentModel.DataAnnotations;
using WebBlazorAPI.Shared.DTO.Ciudad;
using WebBlazorAPI.Shared.DTO.Expedicion;
using WebBlazorAPI.Shared.DTO.TarjetaCredito;
using WebBlazorAPI.Shared.DTO.User;
using WebBlazorAPI.Shared.Enums;

namespace WebBlazorAPI.Shared.DTO.Venta
{
    public class VentaDTO
    {
        [Key]
        public int Id_Venta { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha/Hora")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        public UsersDTO? User { get; set; }

        public string? UserId { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_ciudad { get; set; }

        public CiudadDTO? Ciudades { get; set; }

        public int Id_Expedicion { get; set; }

        public ExpedicionDTO? VentaExpedicion { get; set; }

        public int Id_Carta { get; set; }
        public CartaCreditoDTO? VentaCartaCredito { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        public VentaStatus VentaStado { get; set; }

        public ICollection<VentaDetailDTO>? VentaDetalles { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Líneas")]
        public int Lines => VentaDetalles == null ? 0 : VentaDetalles.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public float Quantity => VentaDetalles == null ? 0 : VentaDetalles.Sum(sd => sd.Quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => VentaDetalles == null ? 0 : VentaDetalles.Sum(sd => sd.Value);
    }
}
