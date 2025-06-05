using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User : IdentityUser
    {
        public List<GroupMember> GroupMemberships { get; set; } = new List<GroupMember>();
    }
}
