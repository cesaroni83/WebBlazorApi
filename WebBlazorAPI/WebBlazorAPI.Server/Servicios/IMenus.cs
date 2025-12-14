using WebBlazorAPI.Shared.DTO.Menu;

namespace WebBlazorAPI.Server.Servicios
{
    public interface IMenus 
    {
        Task<bool> DeleteMenuLogica(int id);
        Task<List<MenuDropDTO>> GetParendMenu(string Estado);
        Task<string> Name_Menu(int id_menu);
    }
}
