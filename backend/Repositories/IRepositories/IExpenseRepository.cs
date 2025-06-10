using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories.IRepositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllAsync();
        Task<Expense> GetByIdAsync(int id);
        Task AddAsync(Expense expense);            
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(int id);
        Task<int> SaveChangesAsync();
        Task<List<Expense>> GetByGroupIdAsync(int groupId);

    }
}
