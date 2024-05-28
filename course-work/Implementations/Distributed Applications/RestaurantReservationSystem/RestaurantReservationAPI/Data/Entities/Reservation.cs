using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservationAPI.Data.Entities
{
    public class Reservation : BaseEnity
    {
        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [Required]
        public int TableId { get; set; }

        public Table Table { get; set; }
        [MaxLength(500)]
        public string SpecialRequests { get; set; } 

        [Required]
        public int VipGuests { get; set; } 
    }
}
