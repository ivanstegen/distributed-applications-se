using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;


namespace RestaurantReservationAPI.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync();
        Task<ReservationDTO> GetReservationByIdAsync(int id);
        Task<ReservationDTO> CreateReservationAsync(ReservationDTO reservation);
        Task<bool> UpdateReservationAsync(ReservationDTO reservation);
        Task<bool> DeleteReservationAsync(int id);
    }
}
