using backend.Dto;

namespace backend.Services
{
    public interface IExpenseServices
    {
        Task<List<ExpenseDTO>> GetAllAsync();
        Task<ExpenseDTO> GetByIdAsync(int id);
        Task<ExpenseDTO> CreateAsync(ExpenseDTO dto);
        Task<ExpenseDTO> UpdateAsync(ExpenseDTO dto);
        Task DeleteAsync(int id);
        Task AddAsync(ExpenseDTO dto);
        Task<List<ExpenseDTO>> GetByGroupIdAsync(int groupId);
    }
}
