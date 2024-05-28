using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReservationAPI.Services
{
    public class TableService : ITableService
    {
        private readonly RestaurantContext _context;

        public TableService(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            var tableDTOs = new List<TableDTO>();
            foreach (var item in _context.Tables)
            {
                var tableDTO = new TableDTO();
                tableDTO.Number = item.Number;
                tableDTO.Seats = item.Seats;    
                tableDTO.Id = item.Id;
                tableDTO.Material = item.Material;
                tableDTO.IsPopular = item.IsPopular;
                tableDTO.Location = item.Location;
                tableDTOs.Add(tableDTO);
            }
            return tableDTOs;
        }

        public async Task<TableDTO> GetTableByIdAsync(int id)
        {
            var item = await _context.Tables.FirstOrDefaultAsync(r => r.Id == id);
            var tableDTO = new TableDTO();
            tableDTO.Number = item.Number;
            tableDTO.Seats = item.Seats;
            tableDTO.Id = item.Id;
            tableDTO.Material = item.Material;
            tableDTO.IsPopular = item.IsPopular;
            tableDTO.Location = item.Location;
            return tableDTO;
        }

        public async Task<TableDTO> CreateTableAsync(TableDTO tableDto)
        {
            var table = new Table();
            table.Number = tableDto.Number;
            table.Seats = tableDto.Seats;
            table.Id = tableDto.Id;
            table.Material = tableDto.Material;
            table.IsPopular = tableDto.IsPopular;
            table.Location = tableDto.Location;
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return tableDto;
        }

        public async Task<bool> UpdateTableAsync(TableDTO tableDto)
        {
            var table = new Table();
            table.Number = tableDto.Number;
            table.Seats = tableDto.Seats;
            table.Id = tableDto.Id;
            table.Material = tableDto.Material;
            table.IsPopular = tableDto.IsPopular;
            table.Location = tableDto.Location;
            _context.Tables.Update(table);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return false;

            _context.Tables.Remove(table);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
