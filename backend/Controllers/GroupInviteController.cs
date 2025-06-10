using backend.Dtos;
using backend.Models;
using backend.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupInviteController : ControllerBase
    {
        private readonly IGroupInviteService _service;

        public GroupInviteController(IGroupInviteService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<GroupInviteModel> Create([FromBody] CreateInviteDto dto)
        {
            var result = _service.Create(dto.GroupId, dto.Email);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupInviteModel> GetById(int id)
        {
            var invite = _service.GetById(id);
            if (invite == null)
                return NotFound();
            return Ok(invite);
        }

        [HttpGet]
        public ActionResult<IEnumerable<GroupInviteModel>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] InviteStatus status)
        {
            _service.UpdateStatus(id, status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }

}
