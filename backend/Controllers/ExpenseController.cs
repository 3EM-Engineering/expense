using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dto;
using Microsoft.AspNetCore.Authorization;
using backend.Models;
using backend.Services.IServices;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseServices _service;

        public ExpenseController(IExpenseServices service)
        {
            _service = service;
        }

        // GET: api/expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetById(int id)
        {
            var expense = await _service.GetByIdAsync(id);
            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        // GET: api/groups/{groupId}/expenses
        [HttpGet("/api/groups/{groupId}/expenses")]
        public async Task<ActionResult<List<Expense>>> GetByGroupId(int groupId)
        {
            var all = await _service.GetAllAsync();
            var expenses = all.FindAll(e => e.GruppoId == groupId);
            return Ok(expenses);
        }

        // POST: api/expenses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseDTO expense)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value.Errors.Select(er => er.ErrorMessage).ToList()
                    });

                return BadRequest(new
                {
                    Message = "Model binding failed",
                    Errors = errors
                });
            }

            var created = await _service.CreateAsync(expense);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ExpenseDTO updatedExpense)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var userId = GetUserId();
            if (existing.CreatoreId != userId)
                return Forbid();

            updatedExpense.Id = id;
            await _service.UpdateAsync(updatedExpense);
            return NoContent();
        }

        // DELETE: api/expenses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var userId = GetUserId();
            if (existing.CreatoreId != userId)
                return Forbid();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/groups/{groupId}/leave
        [HttpPost("/api/groups/{groupId}/leave")]
        public ActionResult LeaveGroup(int groupId)
        {
            // Placeholder per la logica leave group (fuori dal contesto di ExpenseService)
            return NoContent();
        }

        private int GetUserId()
        {
            if (Request.Headers.TryGetValue("X-User-Id", out var userIdHeader) &&
                int.TryParse(userIdHeader, out int userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("User ID not provided");
        }
    }
}
