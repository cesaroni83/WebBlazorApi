using System.ComponentModel.DataAnnotations;
using WebBlazorAPI.Shared.DTO.Producto;

namespace WebBlazorAPI.Shared.DTO.Venta
{
    public class VentaDetailDTO
    {
        [Key]
        [Required]
        public int Id_Detalle { get; set; }

        public VentaDTO? Ventas { get; set; }

        public int Id_Venta { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        public ProductoDTO? Productos { get; set; }

        public int Id_producto { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => Productos == null ? 0 : (decimal)Quantity * Productos.Price;
    }
}
