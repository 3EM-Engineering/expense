using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories.IRepositories;
using backend.Services.IServices;

namespace backend.Services
{
    public class ExpenseShareServices : IExpenseShareServices
    {
        private readonly IExpenseShareRepository _repository;

        public ExpenseShareServices(IExpenseShareRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExpenseShare>> GetByExpenseIdAsync(int expenseId)
        {
            return await _repository.GetByExpenseIdAsync(expenseId);
        }

        public async Task AddOrUpdateSharesAsync(int expenseId, List<ExpenseShare> shares)
        {
            await _repository.DeleteByExpenseIdAsync(expenseId); // elimina le expesesShare precedenti
            foreach (var share in shares)
            {
                share.ExpenseId = expenseId;
                await _repository.AddAsync(share);
            }

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteByExpenseIdAsync(int expenseId)
        {
            await _repository.DeleteByExpenseIdAsync(expenseId);
            await _repository.SaveChangesAsync();
        }
    }
}
