using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Server.Servicios;
using WebBlazorAPI.Shared.DTO.Persona;
using WebBlazorAPI.Shared.Modelo;

namespace WebBlazorAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : GenericController<Persona, PersonaDTO>
    {
        private readonly IPersonas _persona;
        public PersonasController(IGenericRepository<Persona> repo, IMapper mapper, IPersonas persona) : base(repo, mapper)
        {
            _persona = persona;
        }

        [HttpGet("GetPersonaByUser/{email}", Name = "GetPersonaByUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPersonaByEmail(string email)
        {
            var lista = await _persona.GetPersonaByUser(email);
            return Ok(lista);
        }


        [HttpGet("PersonasActivo/{Estado}", Name = "PersonaActivo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PersonaDefault(string Estado)
        {
            var lista = await _persona.GetListPersonaActivo(Estado);
            return Ok(lista);
        }

        [HttpGet("ComboPersona/{Estado}", Name = "PersonaCombo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PersonaCombo(string Estado)
        {
            var lista = await _persona.GetPersonaCombo(Estado);
            return Ok(lista);
        }
        [HttpGet("ComboPersonaByType/{Type}/{Estado}", Name = "PersonaComboByType")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PersonaComboByType(string Type, string Estado)
        {
            var lista = await _persona.GetPersonaType(Type, Estado);
            return Ok(lista);
        }

        [HttpDelete("CancelPersonaLogica/{id_persona:int}", Name = "CancelPersona")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelPais(int id_persona)
        {
            var Registro = await _persona.DeletePersonaLogica(id_persona);
            return Ok(Registro);
        }
    }
}
