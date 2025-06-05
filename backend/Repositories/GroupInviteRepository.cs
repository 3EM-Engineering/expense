using backend.Models;
using backend.Repositories.IRepositories;

public class GroupInviteService : IGroupInviteService
{
    private readonly IRepository<GroupInviteModel> _repository;

    public GroupInviteService(IRepository<GroupInviteModel> repository)
    {
        _repository = repository;
    }

    public GroupInviteModel Create(int groupId, string email)
    {
        var invite = new GroupInviteModel
        {
            GroupId = groupId,
            Email = email,
            Status = InviteStatus.Pending,
            Token = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow
        };

        _repository.Add(invite);
        _repository.Save();

        return invite;
    }

    public GroupInviteModel? GetById(int id) => _repository.Get(id);

    public IEnumerable<GroupInviteModel> GetAll() => _repository.GetAll() ?? new List<GroupInviteModel>();

    public void UpdateStatus(int id, InviteStatus status)
    {
        var invite = _repository.Get(id);
        if (invite != null)
        {
            invite.Status = status;
            _repository.Update(invite);
            _repository.Save();
        }
    }

    public void Delete(int id)
    {
        var invite = _repository.Get(id);
        if (invite != null)
        {
            _repository.Delete(invite);
            _repository.Save();
        }
    }
}
