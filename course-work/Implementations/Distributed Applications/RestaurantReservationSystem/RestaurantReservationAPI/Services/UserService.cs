using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReservationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantContext _context;
        private readonly IAuthService _authService;

        public UserService(RestaurantContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<AuthResult> RegisterUserAsync(UserDTO model)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                return new AuthResult { IsSuccess = false, Message = "Username already exists" };
            }
            var user = new User();
            user.Username = model.Username;
            user.Email = model.Email;
            user.Password = _authService.HashPassword(model.Password);
            user.Id = model.Id;
            user.Age = model.Age;
            user.ReservationId = model.ReservationId;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResult { IsSuccess = true, Message = "User registered successfully" };
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var userDTOs = new List<UserDTO>();
            foreach (var item in _context.Users)
            {
                var userDTO = new UserDTO();
                userDTO.Username = item.Username;
                userDTO.Email = item.Email;
                userDTO.ReservationId = item.ReservationId;
                userDTO.Id = item.Id;
                userDTO.Age = item.Age;
                userDTOs.Add(userDTO);
            }
            return userDTOs;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var item = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            var userDTO = new UserDTO();
            userDTO.Username = item.Username;
            userDTO.Email = item.Email;
            userDTO.ReservationId = item.ReservationId;
            userDTO.Id = item.Id;
            userDTO.Age = item.Age;
            return userDTO;
        }

        public async Task<AuthResult> UpdateUserAsync(UserEditDTO model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user == null)
            {
                return new AuthResult { IsSuccess = false, Message = "User not found" };
            }

            user.Username = model.Username;
            user.Email = model.Email;
            //user.Password = _authService.HashPassword(model.Password);
            user.Id = model.Id;
            user.Age = model.Age;
            user.ReservationId = model.ReservationId;
            //if (!string.IsNullOrEmpty(model.Password))
            //{
            //    user.Password = _authService.HashPassword(model.Password);
            //}

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new AuthResult { IsSuccess = true, Message = "User updated successfully" };
        }

        public async Task<AuthResult> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new AuthResult { IsSuccess = false, Message = "User not found" };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new AuthResult { IsSuccess = true, Message = "User deleted successfully" };
        }
    }
}
