using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services.IServices
{
    public interface IExpenseShareServices
    {
        Task<List<ExpenseShare>> GetByExpenseIdAsync(int expenseId);
        Task AddOrUpdateSharesAsync(int expenseId, List<ExpenseShare> shares);
        Task DeleteByExpenseIdAsync(int expenseId);
    }
}
