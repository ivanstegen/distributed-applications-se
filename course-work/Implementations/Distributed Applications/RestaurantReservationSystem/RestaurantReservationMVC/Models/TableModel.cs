using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationMVC.Models
{
    public class TableModel
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Seats { get; set; }

        public string Location { get; set; }

        public bool IsPopular { get; set; }

        public string Material { get; set; }
    }
}
