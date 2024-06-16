using RecommendationEngineClientSide.DTO;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto);
        Task<SocketResponseDTO> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto);
        Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto);
    }
}
