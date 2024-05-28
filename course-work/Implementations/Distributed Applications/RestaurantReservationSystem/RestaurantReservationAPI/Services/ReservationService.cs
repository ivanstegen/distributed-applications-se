using Microsoft.EntityFrameworkCore;
using RestaurantReservationAPI.Data;
using RestaurantReservationAPI.Data.Entities;
using RestaurantReservationAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReservationAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RestaurantContext _context;

        public ReservationService(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync()
        {
            var reservationDTOs = new List<ReservationDTO>();
            foreach (var item in _context.Reservations)
            {
                var reservationDTO = new ReservationDTO();
                reservationDTO.Id = item.Id;
                reservationDTO.ReservationDate = item.ReservationDate;
                reservationDTO.TableId = item.TableId;
                reservationDTO.NumberOfGuests = item.NumberOfGuests;
                reservationDTO.VipGuests = item.VipGuests;
                reservationDTO.SpecialRequests = item.SpecialRequests;
                reservationDTOs.Add(reservationDTO);
            }
            return reservationDTOs;
        }

        public async Task<ReservationDTO> GetReservationByIdAsync(int id)
        {
            var item = await _context.Reservations.Include(r => r.Table).FirstOrDefaultAsync(r => r.Id == id);
            var reservationDTO = new ReservationDTO();
            reservationDTO.Id = item.Id;
            reservationDTO.ReservationDate = item.ReservationDate;
            reservationDTO.TableId = item.TableId;
            reservationDTO.NumberOfGuests = item.NumberOfGuests;
            reservationDTO.VipGuests = item.VipGuests;
            reservationDTO.SpecialRequests = item.SpecialRequests;
            return reservationDTO;
        }

        public async Task<ReservationDTO> CreateReservationAsync(ReservationDTO reservationDto)
        {
            var reservation = new Reservation();
            reservation.Id = reservationDto.Id;
            reservation.ReservationDate = reservationDto.ReservationDate;
            reservation.TableId = reservationDto.TableId;
            reservation.NumberOfGuests = reservationDto.NumberOfGuests;
            reservation.SpecialRequests = reservationDto.SpecialRequests;
            reservation.VipGuests= reservationDto.VipGuests;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservationDto;
        }

        public async Task<bool> UpdateReservationAsync(ReservationDTO reservationDto)
        {
            var reservation = new Reservation();
            reservation.Id = reservationDto.Id;
            reservation.ReservationDate = reservationDto.ReservationDate;
            reservation.TableId = reservationDto.TableId;
            reservation.NumberOfGuests = reservationDto.NumberOfGuests;
            reservation.VipGuests = reservationDto.VipGuests;
            reservation.SpecialRequests = reservationDto.SpecialRequests;
            _context.Reservations.Update(reservation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
