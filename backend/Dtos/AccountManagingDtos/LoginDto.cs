using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.AccountManagingDtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
