using backend.Dto;

namespace backend.Services.IServices
{
    public interface IGroupService
    {
        Task<List<GroupExpenceDto>> GetAllAsync();
        Task<GroupExpenceDto> GetByIdAsync(int id);
        Task<GroupExpenceDto> CreateAsync(GroupExpenceDto dto);
        Task UpdateAsync(int id, GroupExpenceDto dto);
        Task DeleteAsync(int id);
    }
}
