using WebBlazorAPI.Shared.DTO.Producto;
using WebBlazorAPI.Shared.DTO.User;

namespace WebBlazorAPI.Shared.DTO.VentaTemporal
{
    public class VentaTemporalDTO
    {
        public int Id { get; set; }

        public int Id_producto { get; set; }

        public float Quantity { get; set; } = 1;

        public string Remarks { get; set; } = string.Empty;

        public ProductoDTO? Product { get; set; }
        public decimal Value => Product == null ? 0 : Product.Price * (decimal)Quantity;

        public string? IdUser { get; set; }

        public UsersDTO? Users { get; set; }

    }
}
