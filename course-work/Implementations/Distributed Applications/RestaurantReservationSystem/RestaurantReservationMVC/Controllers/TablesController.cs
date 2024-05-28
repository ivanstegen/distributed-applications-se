using Microsoft.AspNetCore.Mvc;
using RestaurantReservationMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using RestaurantReservationAPI.Authentication;

namespace RestaurantReservationMVC.Controllers
{
    public class TablesController : Controller
    {
        private readonly HttpClient _httpClient;

        public TablesController(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int? seats, int? page)
        {
            var response = await _httpClient.GetAsync("https://localhost:44313/api/tables");
            var responseData = await response.Content.ReadAsStringAsync();
            var tables = JsonSerializer.Deserialize<List<TableModel>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (seats.HasValue)
            {
                tables = tables.Where(t => t.Seats == seats.Value).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = 3;
            var pagedTables = tables.ToPagedList(pageNumber, pageSize);

            return View(pagedTables);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TableModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:44313/api/tables", model);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var apiTable = await _httpClient.GetFromJsonAsync<RestaurantReservationAPI.Data.Entities.Table>($"https://localhost:44313/api/tables/{id}");
            var model = new TableModel
            {
                Id = apiTable.Id,
                Number = apiTable.Number,
                Seats = apiTable.Seats,
                Location = apiTable.Location,
                IsPopular = apiTable.IsPopular,
                Material = apiTable.Material
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TableModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiTable = new RestaurantReservationAPI.Data.Entities.Table
                    {
                        Id = model.Id,
                        Number = model.Number,
                        Seats = model.Seats,
                        Location = model.Location,
                        IsPopular = model.IsPopular,
                        Material = model.Material
                    };
                    var response = await _httpClient.PutAsJsonAsync($"https://localhost:44313/api/tables/{model.Id}", apiTable);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var apiTable = await _httpClient.GetFromJsonAsync<RestaurantReservationAPI.Data.Entities.Table>($"https://localhost:44313/api/tables/{id}");
            var model = new TableModel
            {
                Id = apiTable.Id,
                Number = apiTable.Number,
                Seats = apiTable.Seats,
                Location = apiTable.Location,
                IsPopular = apiTable.IsPopular,
                Material = apiTable.Material
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44313/api/tables/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                
            }
            return RedirectToAction("Index");
        }
    }
}
