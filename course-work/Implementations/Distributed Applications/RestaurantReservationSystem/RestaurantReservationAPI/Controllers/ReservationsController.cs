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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            return Ok(await _reservationService.GetAllReservationsAsync());
        }

        [HttpGet("{id}")]
      //  [Authorize]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
       // [Authorize]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation)
        {
            await _reservationService.CreateReservationAsync(reservation);
            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            var result = await _reservationService.UpdateReservationAsync(reservation);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
       // [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
