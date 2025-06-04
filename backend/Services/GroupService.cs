using backend.Dto;
using backend.Models;
using backend.Repositories.IRepositories;
using backend.Services.IServices;

namespace backend.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GroupExpenceDto>> GetAllAsync()
        {
            var gruppi = await _repository.GetAllAsync();

            return gruppi.Select(static g => new GroupExpenceDto
            {
                Id = g.Id,
                Nome = g.Nome,
                CreatoreId = g.CreatoreId,
                MembriIds = g.Membri.Select(m => m.UserId).ToList(),
                SpeseIds = g.SpeseCollegate.Select(s => s.Id).ToList()
            }).ToList();
        }

        public async Task<GroupExpenceDto> GetByIdAsync(string id)
        {
            var gruppo = await _repository.GetByIdAsync(id);

            if (gruppo == null) return null;

            return new GroupExpenceDto
            {
                Id = gruppo.Id,
                Nome = gruppo.Nome,
                CreatoreId = gruppo.CreatoreId,
                MembriIds = gruppo.Membri.Select(m => m.UserId).ToList(),
                SpeseIds = gruppo.SpeseCollegate.Select(s => s.Id).ToList()
            };
        }

        public async Task<GroupExpenceDto> CreateAsync(GroupExpenceDto dto)
        {
            var gruppo = new Models.GroupModel
            {
                Id = Guid.NewGuid().ToString(), // Genera un ID unico
                Nome = dto.Nome,
                CreatoreId = dto.CreatoreId,
                Membri = new List<GroupMember>()
            };

            // Caricamento Membri da DB (da aggiungere se necessario)

            await _repository.AddAsync(gruppo);
            await _repository.SaveChangesAsync();

            dto.Id = gruppo.Id;
            return dto;
        }

        public async Task UpdateAsync(string id, GroupExpenceDto dto)
        {
            var gruppo = await _repository.GetByIdAsync(id);
            if (gruppo == null) throw new Exception("Gruppo non trovato");

            gruppo.Nome = dto.Nome;
            gruppo.CreatoreId = dto.CreatoreId;

            // Aggiornamento membri (aggiungi la logica se serve)

            _repository.Update(gruppo);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var gruppo = await _repository.GetByIdAsync(id);
            if (gruppo == null) throw new Exception("Gruppo non trovato");

            _repository.Delete(gruppo);
            await _repository.SaveChangesAsync();
        }
    }
}
