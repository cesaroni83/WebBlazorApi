using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.Modelo
{
    public class Categoria
    {
        [Key]
        public int Id_Categoria { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Descripcion_Cat { get; set; } = null!;

        [Display(Name = "Informacion De Categoria")]
        [MaxLength(100, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Informacion_Cat { get; set; } = string.Empty;

        public DateTime Date_reg { get; set; } = DateTime.Now;

        [Display(Name = "Estado Catergoria")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(10, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Estado_Cat { get; set; } = string.Empty;

        // 🔹 Relación inversa (1 categoría tiene muchos productos)
        public ICollection<Producto>? Productos { get; set; }
    }
}
