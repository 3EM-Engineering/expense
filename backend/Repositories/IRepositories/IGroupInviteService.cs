using backend.Models;

namespace backend.Repositories.IRepositories
{
    public interface IGroupInviteService
    {
        GroupInviteModel Create(int groupId, string email);
        GroupInviteModel? GetById(int id);
        IEnumerable<GroupInviteModel> GetAll();
        void UpdateStatus(int id, InviteStatus status);
        void Delete(int id);
    }

}
