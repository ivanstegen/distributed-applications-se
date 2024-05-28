using System.ComponentModel.DataAnnotations;

namespace RestaurantReservationAPI.Data.Entities
{
    public abstract class BaseEnity
    {
        [Key]
        public int Id { get; set; }
    }
}
