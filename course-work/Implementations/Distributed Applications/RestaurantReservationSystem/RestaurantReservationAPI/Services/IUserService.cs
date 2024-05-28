using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;

namespace RestaurantReservationAPI.Services
{
    public interface IUserService
    {
        Task<AuthResult> RegisterUserAsync(UserDTO model);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<AuthResult> UpdateUserAsync(UserEditDTO model);
        Task<AuthResult> DeleteUserAsync(int id);
    }
}
