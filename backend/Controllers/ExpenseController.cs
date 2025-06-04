using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dto;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using backend.Models;

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
            var expense = await _service.GetExpenseByIdAsync(id);
            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        // GET: api/groups/{groupId}/expenses
        [HttpGet("/api/groups/{groupId}/expenses")]
        public async Task<ActionResult<List<Expense>>> GetByGroupId(int groupId)
        {
            var all = await _service.GetAllExpensesAsync();
            var expenses = all.FindAll(e => e.GruppoId == groupId);
            return Ok(expenses);
        }

        // POST: api/expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> Create([FromBody] Expense expense)
        {
            var created = await _service.CreateExpenseAsync(expense);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Expense updatedExpense)
        {
            var existing = await _service.GetExpenseByIdAsync(id);
            if (existing == null)
                return NotFound();

            var userId = GetUserId();
            if (existing.CreatoreId != userId)
                return Forbid();

            updatedExpense.Id = id;
            await _service.UpdateExpenseAsync(updatedExpense);
            return NoContent();
        }

        // DELETE: api/expenses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _service.GetExpenseByIdAsync(id);
            if (existing == null)
                return NotFound();

            var userId = GetUserId();
            if (existing.CreatoreId != userId)
                return Forbid();

            await _service.DeleteExpenseAsync(id);
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
