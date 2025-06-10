using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ExpenseShareRepository : IExpenseShareRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseShareRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseShare>> GetByExpenseIdAsync(int expenseId)
        {
            return await _context.ExpenseShares
                .Where(es => es.ExpenseId == expenseId)
                .ToListAsync();
        }

        public async Task AddAsync(ExpenseShare share)
        {
            await _context.ExpenseShares.AddAsync(share);
        }

        public async Task DeleteByExpenseIdAsync(int expenseId)
        {
            var shares = await _context.ExpenseShares
                .Where(es => es.ExpenseId == expenseId)
                .ToListAsync();

            _context.ExpenseShares.RemoveRange(shares);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
