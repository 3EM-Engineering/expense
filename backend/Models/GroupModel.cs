using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class GroupModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Nome { get; set; }

        [Required]
        public string CreatoreId { get; set; }

        public User Creatore { get; set; }

        public List<GroupMember> Membri { get; set; } = new List<GroupMember>();

        public List<Expense> SpeseCollegate { get; set; } = new List<Expense>();
    }
}
