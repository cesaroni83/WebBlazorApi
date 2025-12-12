using System.ComponentModel.DataAnnotations;

namespace WebBlazorAPI.Shared.DTO.Menu
{
    public class MenuDropDTO
    {
        public int Id_menu { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre al Menu")]
        public string Descripcion { get; set; } = string.Empty;
    }
}
