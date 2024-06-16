using RecommendationEngineClientSide.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync("EmployeeController/HandleGetDailyMenu", dailyMenuRequestDto);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<DailyMenuResponseDto>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<SocketResponseDTO> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync("EmployeeController/HandlePlaceOrder", orderDetailRequestDto);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<SocketResponseDTO>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto)
        {
            var response = await _httpClient.PostAsJsonAsync("EmployeeController/HandleGiveFeedback", feedbackDto);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<SocketResponseDTO>(await response.Content.ReadAsStreamAsync());
        }
    }
}
