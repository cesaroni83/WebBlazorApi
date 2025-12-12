using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.DTO.Categoria
{
    public class CategoriaDropDTO
    {
        public int Id_Categoria { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Descripcion_Cat { get; set; } = null!;
    }
}
