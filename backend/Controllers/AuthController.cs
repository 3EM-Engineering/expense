using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        public AuthController(
                AuthService authService
            )
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public string GenerateToken(User user)
        {
            return _authService.GenerateToken(user);
        }
    }
}
