using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.DTO;
using RestaurantReservationAPI.Services;
using System.Threading.Tasks;

namespace RestaurantReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _authService.LoginAsync(model);
            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
