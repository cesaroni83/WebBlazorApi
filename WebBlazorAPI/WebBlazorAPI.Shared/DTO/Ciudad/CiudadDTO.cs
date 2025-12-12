using System.ComponentModel.DataAnnotations;
using WebBlazorAPI.Shared.DTO.Provincia;

namespace WebBlazorAPI.Shared.DTO.Ciudad
{
    public class CiudadDTO
    {
        [Key]
        [Display(Name = "ID Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_ciudad { get; set; }


        [Display(Name = "ID Provincia")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_provincia { get; set; }


        [Display(Name = "Nombre Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Nombre_ciudad { get; set; } = null!;

        [Display(Name = "Informacion De Ciudad")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Informacion_ciudad { get; set; } = string.Empty;

        [Display(Name = "Estado Ciudad")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(10, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Estado_ciudad { get; set; } = string.Empty;
        public ProvinciaDTO? Provincias { get; set; } = null;

    }
}
