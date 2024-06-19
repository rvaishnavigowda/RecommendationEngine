using RecommendationEngineClientSide.DTO;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto);
        Task<OrderDetailRequestDto> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto);
        Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto);
    }
}
