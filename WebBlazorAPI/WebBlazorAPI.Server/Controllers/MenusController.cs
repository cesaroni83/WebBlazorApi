using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Server.Servicios;
using WebBlazorAPI.Shared.DTO.Menu;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : GenericController<Menu, MenuDTO>
    {
        private readonly IMenus _menu;
        public MenusController(IGenericRepository<Menu> repo, IMapper mapper, IMenus menu) : base(repo, mapper)
        {
            _menu = menu;
        }

        [HttpGet("ComboMenu/{Estado}", Name = "MenuCombo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ComboMenu(string Estado)
        {
            var lista = await _menu.GetParendMenu(Estado);
            return Ok(lista);
        }


        [HttpGet("MenuName/{id_menu:int}", Name = "MenuName")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> MenuName(int id_menu)
        {
            try
            {
                var nombre = await _menu.Name_Menu(id_menu);
                return Ok(nombre);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("CancelMenuLogica/{id_menu:int}", Name = "CancelMenuLogica")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelMenu(int id_menu)
        {
            var Registro = await _menu.DeleteMenuLogica(id_menu);
            return Ok(Registro);
        }
    }
}
