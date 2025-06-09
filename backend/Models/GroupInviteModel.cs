namespace backend.Models
{
    public class GroupInviteModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Email { get; set; }
        public InviteStatus Status { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum InviteStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
