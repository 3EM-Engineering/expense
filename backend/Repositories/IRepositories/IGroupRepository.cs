using backend.Model;

namespace backend.Repositories.IRepositories
{
    public interface IGroupRepository
    {
        Task<List<GroupModel>> GetAllAsync();
        Task<GroupModel> GetByIdAsync(int id);
        Task AddAsync(GroupModel gruppo);
        void Update(GroupModel gruppo);
        void Delete(GroupModel gruppo);
        Task<bool> SaveChangesAsync();
    }
}
