using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="Can not be negative number")]
        public int Age { get; set; }
        public int ReservationId { get; set; }
    }
}
