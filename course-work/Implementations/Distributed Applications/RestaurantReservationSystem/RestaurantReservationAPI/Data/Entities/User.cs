using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Data.Entities
{
    public class User : BaseEnity
    {
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
        public int Age { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
