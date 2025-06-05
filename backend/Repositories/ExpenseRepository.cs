using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;
namespace backend.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .Include(e => e.Quote)
                .ToListAsync();
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .Include(e => e.Quote)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }
    }
}
