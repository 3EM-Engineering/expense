using backend.Dto;
using backend.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/group
        [HttpGet]
        public async Task<ActionResult<List<GroupExpenceDto>>> GetAll()
        {
            var groups = await _groupService.GetAllAsync();
            return Ok(groups);
        }

        // GET: api/group/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupExpenceDto>> GetById(string id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound("Gruppo non trovato");
            return Ok(group);
        }

        // POST: api/group
        [HttpPost]
        public async Task<ActionResult<GroupExpenceDto>> Create([FromBody] GroupExpenceDto dto)
        {
            if (dto == null)
                return BadRequest("Dati mancanti");

            var created = await _groupService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/group/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] GroupExpenceDto dto)
        {
            if (dto == null || dto.Id != id)
                return BadRequest("Dati non validi");

            try
            {
                await _groupService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/group/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _groupService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
