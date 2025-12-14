using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Server.Servicios;
using WebBlazorAPI.Shared.DTO.Provincia;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : GenericController<Provincia, ProvinciaDTO>
    {
        private readonly IProvincias _provincia;
        public ProvinciasController(IGenericRepository<Provincia> repo, IMapper mapper, IProvincias provincia) : base(repo, mapper)
        {
            _provincia = provincia;
        }

        [HttpGet("ProvinciaByPais/{id_pais:int}", Name = "ProvinciaByPais")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ProvinciaByPais(int id_pais)
        {
            var lista = await _provincia.GetProvinciaByPais(id_pais);
            return Ok(lista);
        }

        [HttpGet("ProvinciasActiva/{Default_name}", Name = "ProvinciaActiva")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PaisDefault(string Default_name)
        {
            var lista = await _provincia.GetListProvinciaActivo(Default_name);
            return Ok(lista);
        }

        [HttpGet("ComboProvincia/{id_pais:int}/{Estado}", Name = "ProvinciaCombo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ProvinciaCombo(int id_pais, string Estado)
        {
            var lista = await _provincia.GetProvinciaCombo(id_pais, Estado);
            return Ok(lista);
        }


        [HttpDelete("CancelProvinciaLogica/{id_provincia:int}", Name = "CancelProvincia")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelProvincia(int id_provincia)
        {
            var Registro = await _provincia.DeleteProvinciaLogica(id_provincia);
            return Ok(Registro);
        }
    }
}
