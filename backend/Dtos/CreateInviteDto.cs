namespace backend.Dtos
{
    public class CreateInviteDto
    {
        public int GroupId { get; set; }
        public string Email { get; set; } = string.Empty;
    }

}
