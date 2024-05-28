using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;
using RestaurantReservationAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetTables()
        {
            return Ok(await _tableService.GetAllTablesAsync());
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<TableDTO>> GetTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpPost]
       // [Authorize]
        public async Task<ActionResult<TableDTO>> PostTable(TableDTO table)
        {
            await _tableService.CreateTableAsync(table);
            return CreatedAtAction("GetTable", new { id = table.Id }, table);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutTable(int id, TableDTO table)
        {
            if (id != table.Id)
            {
                return BadRequest();
            }

            var result = await _tableService.UpdateTableAsync(table);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
