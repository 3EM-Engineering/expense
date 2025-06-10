using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories.IRepositories
{
    public interface IExpenseShareRepository
    {
        Task<List<ExpenseShare>> GetByExpenseIdAsync(int expenseId);
        Task AddAsync(ExpenseShare share);
        Task DeleteByExpenseIdAsync(int expenseId);
        Task SaveChangesAsync();
    }
}
