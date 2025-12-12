using System.ComponentModel.DataAnnotations;
using WebBlazorAPI.Shared.DTO.Pais;

namespace WebBlazorAPI.Shared.DTO.Provincia
{
    public class ProvinciaDTO
    {
        [Key]
        [Display(Name = "ID Provincia")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_provincia { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_pais { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Nombre_provincia { get; set; } = string.Empty;

        [Display(Name = "Informacion De Provincia")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Informacion_provincia { get; set; } = string.Empty;

        [Display(Name = "Estado Provincia")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(10, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Estado_provincia { get; set; } = string.Empty;

        public PaisDTO? Paises { get; set; } = null;
    }
}
