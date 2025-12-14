using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Server.Servicios;
using WebBlazorAPI.Shared.DTO.Pais;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : GenericController<Pais, PaisDTO>
    {
        private readonly IPaises _pais;
        public PaisesController(IGenericRepository<Pais> repo, IMapper mapper, IPaises pais) : base(repo, mapper)
        {
            _pais = pais;
        }


        [HttpGet("PaisesActivo/{Estado}", Name = "PaisActivo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PaisDefault(string Estado)
        {
            var lista = await _pais.GetListPaisActivo(Estado);
            return Ok(lista);
        }

        [HttpGet("ComboPais/{Estado}", Name = "PaisCombo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PaisCombo(string Estado)
        {
            var lista = await _pais.GetPaisCombo(Estado);
            return Ok(lista);
        }

        [HttpDelete("CancelPais/{id_pais:int}", Name = "CancelPais")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelPais(int id_pais)
        {
            var Registro = await _pais.DeletePaisLogica(id_pais);
            return Ok(Registro);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPaisAll()
        {
            var lista = await _pais.GetListaAllPaises();
            return Ok(lista);
        }

    }
}
