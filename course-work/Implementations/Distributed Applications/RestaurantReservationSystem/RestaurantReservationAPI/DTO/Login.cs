using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.DTO
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
