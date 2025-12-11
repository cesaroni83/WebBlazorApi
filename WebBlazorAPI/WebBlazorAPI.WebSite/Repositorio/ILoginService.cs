namespace WebBlazorAPI.WebSite.Repositorio
{
    public interface ILoginService
    {
        Task LoginAsync(string token);
        Task LogoutAsync();
    }
}
