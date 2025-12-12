using AutoMapper;
using WebBlazorAPI.Shared.DTO.Categoria;
using WebBlazorAPI.Shared.DTO.Ciudad;
using WebBlazorAPI.Shared.DTO.Empresa;
using WebBlazorAPI.Shared.DTO.ImagenProducto;
using WebBlazorAPI.Shared.DTO.Menu;
using WebBlazorAPI.Shared.DTO.Pais;
using WebBlazorAPI.Shared.DTO.Persona;
using WebBlazorAPI.Shared.DTO.Producto;
using WebBlazorAPI.Shared.DTO.Provincia;
using WebBlazorAPI.Shared.DTO.Sucursal;
using WebBlazorAPI.Shared.DTO.User;
using WebBlazorAPI.Shared.DTO.VentaTemporal;
using WebBlazorAPI.Shared.Enums;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.AutoMaper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Pais, PaisDTO>().ReverseMap();
            CreateMap<Pais, PaisDropDTO>().ReverseMap();
            CreateMap<Provincia, ProvinciaDTO>().ReverseMap();
            CreateMap<Provincia, ProvinciaDropDTO>().ReverseMap();
            CreateMap<Ciudad, CiudadDTO>().ReverseMap();
            CreateMap<Ciudad, CiudadDropDTO>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<Menu, MenuDropDTO>().ReverseMap();
            CreateMap<Empresa, EmpresaDTO>().ReverseMap();
            CreateMap<Empresa, EmpresaDropDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDropDTO>().ReverseMap();
            CreateMap<Persona, PersonaDTO>().ReverseMap();
            CreateMap<Persona, PersonaDropDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDropDTO>().ReverseMap();
            CreateMap<ImagenProd, ImagenProdDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Producto, ProductoDropDTO>().ReverseMap();
            CreateMap<User, UsersDTO>().ReverseMap();
            CreateMap<TemporalSale, VentaTemporalDTO>().ReverseMap();

        }
    }
}
