using backend.Dto;

public interface IExpenseServices
{
    Task<List<ExpenseDTO>> GetAllAsync();
    Task<ExpenseDTO> GetByIdAsync(int id);
    Task<ExpenseDTO> CreateAsync(ExpenseDTO dto);
    Task<ExpenseDTO> UpdateAsync(ExpenseDTO dto);
    Task DeleteAsync(int id);
    Task SaveChangeAsync();
    Task AddAsync(ExpenseDTO dto);
}
