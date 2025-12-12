using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.DTO.Provincia
{
    public class ProvinciaDropDTO
    {
        [Key]
        [Display(Name = "ID Provincia")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_provincia { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Nombre_provincia { get; set; } = null!;
    }
}
