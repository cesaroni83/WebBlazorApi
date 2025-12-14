using WebBlazorAPI.Shared.DTO.Provincia;

namespace WebBlazorAPI.Server.Servicios
{
    public interface IProvincias
    {

        Task<List<ProvinciaDropDTO>> GetProvinciaCombo(int id_pais, string Estado_Activo);

        Task<bool> DeleteProvinciaLogica(int id_Provincia);

        Task<List<ProvinciaDTO>> GetProvinciaByPais(int id_pais);

        Task<List<ProvinciaDTO>> GetListProvinciaActivo(string Estado_Activo);
    }
}
