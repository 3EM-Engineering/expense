using backend.Model;
using backend.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupModel>> GetAllAsync()
        {
            return await _context.Gruppi
                .Include(g => g.Membri)
                .Include(g => g.SpeseCollegate)
                .ToListAsync();
        }

        public async Task<GroupModel> GetByIdAsync(int id)
        {
            return await _context.Gruppi
                .Include(g => g.Membri)
                .Include(g => g.SpeseCollegate)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(GroupModel gruppo)
        {
            await _context.Gruppi.AddAsync(gruppo);
        }

        public void Update(GroupModel gruppo)
        {
            _context.Gruppi.Update(gruppo);
        }

        public void Delete(GroupModel gruppo)
        {
            _context.Gruppi.Remove(gruppo);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
