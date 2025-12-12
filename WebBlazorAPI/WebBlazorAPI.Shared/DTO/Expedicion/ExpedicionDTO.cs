using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlazorAPI.Shared.DTO.Ciudad;
using WebBlazorAPI.Shared.DTO.Venta;

namespace WebBlazorAPI.Shared.DTO.Expedicion
{
    public class ExpedicionDTO
    {
        [Key]
        public int Id_Expedicion { get; set; }

        [Display(Name = "Destinatario")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Destinatario { get; set; } = string.Empty;

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_ciudad { get; set; }

        public CiudadDTO? Ciudades { get; set; }

        [Display(Name = "CAP")]
        [MaxLength(10, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string? Cap { get; set; } = string.Empty;

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Direccion { get; set; } = null!;

        [Display(Name = "Telefono")]
        //[Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(20, ErrorMessage = "El Campo {0} no puede mas de {2} Caracteres")]
        public string? Telefono { get; set; } = null!;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observacion")]
        public string? Observacion { get; set; }

        public int Id_Venta { get; set; }
        public VentaDTO? Ventas { get; set; }
    }
}
