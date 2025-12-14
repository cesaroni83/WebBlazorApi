using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlazorAPI.Server.RepositorioGeneral;

namespace WebBlazorAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TDto> : ControllerBase where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repo;
        private readonly IMapper _mapper;

        public GenericController(IGenericRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TDto>>> Get()
        {
            var data = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TDto>>(data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TDto>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<TDto>(entity));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TDto>> Post(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var saved = await _repo.AddAsync(entity);
            return Ok(_mapper.Map<TDto>(saved));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var ok = await _repo.UpdateAsync(entity);
            return ok ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? Ok() : BadRequest();
        }
    }
}