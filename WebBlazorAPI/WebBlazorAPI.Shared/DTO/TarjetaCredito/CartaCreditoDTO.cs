using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlazorAPI.Shared.DTO.Venta;

namespace WebBlazorAPI.Shared.DTO.TarjetaCredito
{
    public class CartaCreditoDTO
    {
        [Key]
        public int Id_Carta { get; set; }

        [Display(Name = "Numero Carta")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public string NumeroCarta { get; set; } = string.Empty;


        [Display(Name = "Titular")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public string Titular { get; set; } = string.Empty;


        [Display(Name = "Fecha Espiracion")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public string FechaExpiracion { get; set; } = string.Empty;


        [Display(Name = "CVV")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public string CVV { get; set; } = string.Empty;

        public int Id_Venta { get; set; }
        public VentaDTO? Ventas { get; set; }
    }
}
