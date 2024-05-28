using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationMVC.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [Required]
        public int TableId { get; set; }

        public string SpecialRequests { get; set; }

        [Required]
        public int VipGuests { get; set; }
    }
}
