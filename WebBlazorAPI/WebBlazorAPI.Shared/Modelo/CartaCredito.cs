using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.Modelo
{
    public class CartaCredito
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
        public DateTime Date_reg { get; set; } = DateTime.Now;
        public int Id_Venta { get; set; }
        public Venta? Ventas { get; set; }
    }
}
