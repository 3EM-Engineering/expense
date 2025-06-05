using backend.Models;

namespace backend.Services.IServices
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
