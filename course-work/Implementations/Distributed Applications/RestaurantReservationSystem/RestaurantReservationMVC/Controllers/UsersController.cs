using Microsoft.AspNetCore.Mvc;
using RestaurantReservationAPI.Authentication;
using RestaurantReservationMVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace RestaurantReservationMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
            _httpClient = httpClient;
        }

        // GET: Users
        public async Task<IActionResult> Index(int? age, int? page)
        {
            var response = await _httpClient.GetAsync("https://localhost:44313/api/users");
            var responseData = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserModel>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (age.HasValue)
            {
                users = users.Where(u => u.Age >= age.Value).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = 4;
            var pagedUsers = users.ToPagedList(pageNumber, pageSize);

            return View(pagedUsers);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            if (!ModelState.IsValid) ModelState.AddModelError(string.Empty, "Registration failed.");

              string _apiBaseUrl = "https://localhost:44313/";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    client.DefaultRequestHeaders.Add(AuthConstants.ApiKeyHeaderName, AuthConstants.ApiKeyHeaderValue);
                var content = JsonContent.Create(model);
                    var response = await client.PostAsync("api/users/register", content);
                }
            
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44313/api/users/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var user = JsonSerializer.Deserialize<UserModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserModel user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                try
                {
                    var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PutAsync($"https://localhost:44313/api/users/{id}", jsonContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error Response: " + errorContent);

                        return View(user);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return View(user);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44313/api/users/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var user = JsonSerializer.Deserialize<UserModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(user);
        }

        // POST: Users/DeleteConfirmed/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44313/api/users/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index)); 
            }
            return RedirectToAction(nameof(Index)); 
        }
    }
}
