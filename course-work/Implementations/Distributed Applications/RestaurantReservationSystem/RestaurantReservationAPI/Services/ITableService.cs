using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;

namespace RestaurantReservationAPI.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIdAsync(int id);
        Task<TableDTO> CreateTableAsync(TableDTO table);
        Task<bool> UpdateTableAsync(TableDTO table);
        Task<bool> DeleteTableAsync(int id);
    }
}
