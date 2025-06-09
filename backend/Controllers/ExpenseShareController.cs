using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dto;
using backend.Models;
using backend.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseShareController : ControllerBase
    {
        private readonly IExpenseShareServices _expenseShareServices;
        private readonly IExpenseServices _expenseService;
        public ExpenseShareController(IExpenseShareServices expenseShareServices, IExpenseServices expenseService)
        {
            _expenseShareServices = expenseShareServices;
            _expenseService = expenseService;
        }

        // Endpoint: POST /expensesShare
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseShareDTO dto)
        {
            if (dto == null || dto.ExpenseId <= 0 || dto.Quote == null)
                return BadRequest("Dati non validi");

            var shares = dto.Quote.Select(q => new ExpenseShare
            {
                UserId = q.UserId,
                Importo = q.Importo
            }).ToList();

            await _expenseShareServices.AddOrUpdateSharesAsync(dto.ExpenseId, shares);
            return Ok();
        }

        // Endpoint: GET /groups/{groupId}/expensesShare
        [HttpGet("/groups/{groupId}/expensesShare")]
        public async Task<IActionResult> GetByGroupId(int groupId)
        {
            var expenses = await _expenseService.GetByGroupIdAsync(groupId);
            if (expenses == null || !expenses.Any())
                return NotFound("Nessuna spesa trovata per questo gruppo.");

            var result = new List<object>();

            foreach (var expense in expenses)
            {
                var shares = await _expenseShareServices.GetByExpenseIdAsync(expense.Id);

                var shareDTOs = shares.Select(s => new ExpenseShareDTO
                {
                    UserId = s.UserId,
                    Importo = s.Importo
                }).ToList();

                result.Add(new
                {
                    ExpenseId = expense.Id,
                    Titolo = expense.Titolo,
                    Quote = shareDTOs
                });
            }

            return Ok(result);
        }


        // Endpoint: GET /expensesShare/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByExpenseId(int id)
        {
            var shares = await _expenseShareServices.GetByExpenseIdAsync(id);
            if (shares == null || shares.Count == 0)
                return NotFound();

            var dto = shares.Select(s => new ExpenseShareDTO
            {
                UserId = s.UserId,
                Importo = s.Importo
            }).ToList();

            return Ok(dto);
        }

        // Endpoint: PUT /expensesShare/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] List<ExpenseShareDTO> dtoList)
        {
            if (dtoList == null || dtoList.Count == 0)
                return BadRequest("Nessun dato da aggiornare");

            var shares = dtoList.Select(d => new ExpenseShare
            {
                ExpenseId = id,
                UserId = d.UserId,
                Importo = d.Importo
            }).ToList();

            await _expenseShareServices.AddOrUpdateSharesAsync(id, shares);
            return Ok();
        }

        // Endpoint: DELETE /expensesShare/{expenseId}
        [HttpDelete("{expenseId}")]
        public async Task<IActionResult> DeleteByExpenseId(int expenseId)
        {
            var shares = await _expenseShareServices.GetByExpenseIdAsync(expenseId);

            if (shares == null || !shares.Any())
            {
                return NotFound($"Nessuna quota trovata per la spesa con ID {expenseId}");
            }

            await _expenseShareServices.DeleteByExpenseIdAsync(expenseId);
            return Ok($"Tutte le quote della spesa con ID {expenseId} sono state eliminate.");
        }

    }
}
