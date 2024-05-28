using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.DTO
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        public int Age { get; set; }
        public int ReservationId { get; set; }
    }
}
