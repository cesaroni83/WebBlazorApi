using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Server.Servicios;
using WebBlazorAPI.Server.Servicios.Implementacion;
using WebBlazorAPI.Shared.DTO.Ciudad;
using WebBlazorAPI.Shared.DTO.Provincia;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController : GenericController<Ciudad, CiudadDTO>
    {
        private readonly ICiudades _ciudad;
        public CiudadesController(IGenericRepository<Ciudad> repo, IMapper mapper, ICiudades ciudad) : base(repo, mapper)
        {
            _ciudad = ciudad;
        }


        [HttpGet("CiudadesByProvincia/{id_provincia:int}", Name = "CiudadesByProvincia")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CiudadesByProvincia(int id_provincia)
        {
            var lista = await _ciudad.GetCiudadByProvincia(id_provincia);
            return Ok(lista);
        }

        [HttpGet("CiudadesActive/{Default_name}", Name = "CiudadActive")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CiudadDefault(string Default_name)
        {
            var lista = await _ciudad.GetListCiudadActivo(Default_name);
            return Ok(lista);
        }

        [HttpGet("ComboCiudades/{id_provincia:int}/{Estado}", Name = "CiudadCombo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CiudadCombo(int id_provincia, string Estado)
        {
            var lista = await _ciudad.GetCiudadCombo(id_provincia, Estado);
            return Ok(lista);
        }
        [HttpDelete("CancelCiudadLogica/{id_ciudad:int}", Name = "CancelCiudad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelCiudad(int id_ciudad)
        {
            var Registro = await _ciudad.DeleteCiudadLogica(id_ciudad);
            return Ok(Registro);
        }


        [HttpGet("GetCiudad/{id_ciudad:int}", Name = "GetCiudad")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCiudad(int id_ciudad)
        {
            var lista = await _ciudad.GetCiudad(id_ciudad);
            return Ok(lista);
        }
    }
}
