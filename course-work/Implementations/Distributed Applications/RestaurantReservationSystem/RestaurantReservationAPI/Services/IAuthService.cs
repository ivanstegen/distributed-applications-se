using RestaurantReservationAPI.DTO;
using System.Threading.Tasks;

namespace RestaurantReservationAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(Login model);
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }
}
