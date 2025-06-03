using backend.Model;

namespace backend.Repositories.IRepositories
{
    public interface IGroupRepository
    {
        Task<List<Gruppo>> GetAllAsync();
        Task<Gruppo> GetByIdAsync(int id);
        Task AddAsync(Gruppo gruppo);
        void Update(Gruppo gruppo);
        void Delete(Gruppo gruppo);
        Task<bool> SaveChangesAsync();
    }
}
