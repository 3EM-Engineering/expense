using backend.Dto;

namespace backend.Services.IServices
{
    public interface IGroupService
    {
        Task<List<GroupExpenceDto>> GetAllAsync();
        Task<GroupExpenceDto> GetByIdAsync(string id);
        Task<GroupExpenceDto> CreateAsync(GroupExpenceDto dto);
        Task UpdateAsync(string id, GroupExpenceDto dto);  // <-- qui
        Task DeleteAsync(string id);                        // <-- e qui
    }

}
