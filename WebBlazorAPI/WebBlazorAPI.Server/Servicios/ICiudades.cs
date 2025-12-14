using WebBlazorAPI.Shared.DTO.Ciudad;

namespace WebBlazorAPI.Server.Servicios
{
    public interface ICiudades
    {

        Task<List<CiudadDropDTO>> GetCiudadCombo(int id_provincia, string Estado_Activo);
        Task<bool> DeleteCiudadLogica(int id_Ciudad);
        Task<List<CiudadDTO>> GetCiudadByProvincia(int id_provincia);

        Task<List<CiudadDTO>> GetListCiudadActivo(string Estado_Activo);

        Task<CiudadDTO> GetCiudad(int id);
    }
}
