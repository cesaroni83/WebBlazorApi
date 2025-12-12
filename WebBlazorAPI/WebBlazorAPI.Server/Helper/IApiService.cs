using WebBlazorAPI.Shared.Enums;

namespace WebBlazorAPI.Server.Helper
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string servicePrefix, string controller);

        Task<Response> GetPaisesConProvinciasYCiudadesAsync();
    }
}
