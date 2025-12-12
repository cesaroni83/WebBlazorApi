using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBlazorAPI.Shared.DTO.Categoria;
using WebBlazorAPI.Shared.DTO.ImagenProducto;
using WebBlazorAPI.Shared.DTO.Venta;
using WebBlazorAPI.Shared.DTO.VentaTemporal;

namespace WebBlazorAPI.Shared.DTO.Producto
{
    public class ProductoDTO
    {
        [Key]
        public int Id_producto { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Inventario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Stock { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Id_Categoria { get; set; }


        [Display(Name = "Estado Producto")]
        [Required(ErrorMessage = "El Campo {0} es Obligatorio!")]
        [MaxLength(10, ErrorMessage = "El Campo {0} no puede mas de {1} Caracteres")]
        public string Estado_producto { get; set; } = string.Empty;

        public CategoriaDTO? Categorias { get; set; }
        // public List<ImagenProdDTO>? Imagenes { get; set; }
        public ICollection<ImagenProdDTO>? ProductImages { get; set; }


        [Display(Name = "Imágenes")]
        public int ProductImagesNumber => ProductImages == null ? 0 : ProductImages.Count;

        [Display(Name = "Imagén")]
        //public byte[]? MainImage { get; set; }
        public string? MainImage { get; set; }

        public ICollection<VentaTemporalDTO>? VentaTemporal { get; set; }

        public ICollection<VentaDetailDTO>? VentaDetail { get; set; }


    }
}
