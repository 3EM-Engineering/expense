using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories.IRepositories;
using backend.Services.IServices;

namespace backend.Services
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IExpenseRepository _repository;

        public ExpenseServices(IExpenseRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Expense>> GetAllExpensesAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<Expense> CreateExpenseAsync(Expense expense)
        {
            // Puoi aggiungere qui eventuali validazioni o logiche business
            return _repository.CreateAsync(expense);
        }

        public Task UpdateExpenseAsync(Expense expense)
        {
            return _repository.UpdateAsync(expense);
        }

        public Task DeleteExpenseAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
