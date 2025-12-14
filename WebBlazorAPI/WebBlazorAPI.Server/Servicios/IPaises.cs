using WebBlazorAPI.Shared.DTO.Pais;

namespace WebBlazorAPI.Server.Servicios
{
    public interface IPaises
    {
        Task<List<PaisDTO>> GetListPaisActivo(string Estado_Activo);
        Task<List<PaisDropDTO>> GetPaisCombo(string Estado_Activo);
        Task<bool> DeletePaisLogica(int id_pais);
        Task<List<PaisDTO>> GetListaAllPaises();
    }
}
