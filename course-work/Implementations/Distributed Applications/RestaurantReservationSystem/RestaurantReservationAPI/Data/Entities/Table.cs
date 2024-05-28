using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Data.Entities
{
    public class Table : BaseEnity
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public int Seats { get; set; }
        public string Location { get; set; }
        public bool IsPopular { get; set; }
        public string Material { get; set; }
    }
}
