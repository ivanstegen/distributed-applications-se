using Microsoft.AspNetCore.Mvc;
using RestaurantReservationMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using System.Text.Json;
using RestaurantReservationAPI.Authentication;

namespace RestaurantReservationMVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReservationsController(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName,AuthConstants.ApiKeyHeaderValue);
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int? numberOfGuests, int? page)
        {
            var response = await _httpClient.GetAsync("https://localhost:44313/api/reservations");
            var responseData = await response.Content.ReadAsStringAsync();
            var reservations = JsonSerializer.Deserialize<List<ReservationModel>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (numberOfGuests.HasValue)
            {
                reservations = reservations.Where(r => r.NumberOfGuests == numberOfGuests.Value).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = 2;
            var pagedReservations = reservations.ToPagedList(pageNumber, pageSize);

            return View(pagedReservations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiReservation = new RestaurantReservationAPI.Data.Entities.Reservation
                    {
                        Id = model.Id,
                        NumberOfGuests = model.NumberOfGuests,
                        ReservationDate = model.ReservationDate,
                        SpecialRequests = model.SpecialRequests,
                        TableId = model.TableId,
                        VipGuests = model.VipGuests
                    };
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:44313/api/reservations", apiReservation);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var apiReservation = await _httpClient.GetFromJsonAsync<RestaurantReservationAPI.Data.Entities.Reservation>($"https://localhost:44313/api/reservations/{id}");
            var model = new ReservationModel
            {
                Id = apiReservation.Id,
                NumberOfGuests = apiReservation.NumberOfGuests,
                ReservationDate = apiReservation.ReservationDate,
                SpecialRequests = apiReservation.SpecialRequests,
                TableId = apiReservation.TableId,
                VipGuests = apiReservation.VipGuests
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiReservation = new RestaurantReservationAPI.Data.Entities.Reservation
                    {
                        Id = model.Id,
                        NumberOfGuests = model.NumberOfGuests,
                        ReservationDate = model.ReservationDate,
                        SpecialRequests = model.SpecialRequests,
                        TableId = model.TableId,
                        VipGuests = model.VipGuests
                    };
                    var response = await _httpClient.PutAsJsonAsync($"https://localhost:44313/api/reservations/{model.Id}", apiReservation);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var apiReservation = await _httpClient.GetFromJsonAsync<RestaurantReservationAPI.Data.Entities.Reservation>($"https://localhost:44313/api/reservations/{id}");
            var model = new ReservationModel
            {
                Id = apiReservation.Id,
                NumberOfGuests = apiReservation.NumberOfGuests,
                ReservationDate = apiReservation.ReservationDate,
                SpecialRequests = apiReservation.SpecialRequests,
                TableId = apiReservation.TableId,
                VipGuests = apiReservation.VipGuests
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44313/api/reservations/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
            }
            return RedirectToAction("Index");
        }
    }
}
