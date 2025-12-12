using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.Modelo
{
    public class Expedicion
    {
        [Key]
        public int Id_Expedicion { get; set; }

        [Display(Name = "Destinatario")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Destinatario { get; set; } = string.Empty;

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_ciudad { get; set; }

        public Ciudad? Ciudades { get; set; }

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

        public DateTime Date_reg { get; set; } = DateTime.Now;

        public int Id_Venta { get; set; }
        public Venta? Ventas { get; set; }

    }
}
