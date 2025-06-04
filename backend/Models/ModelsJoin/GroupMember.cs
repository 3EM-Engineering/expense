using backend.Models;

namespace backend.Models
{
    public class GroupMember
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string GroupId { get; set; }
        public GroupModel Group { get; set; }
    }
}