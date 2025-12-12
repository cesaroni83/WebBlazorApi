using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.DTO.Empresa
{
    public class EmpresaDropDTO
    {
        [Display(Name = "ID Empresa")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        public int Id_empresa { get; set; }

        [Display(Name = "Nombre Empresa")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Nombre_Empresa { get; set; } = null!;
    }
}
