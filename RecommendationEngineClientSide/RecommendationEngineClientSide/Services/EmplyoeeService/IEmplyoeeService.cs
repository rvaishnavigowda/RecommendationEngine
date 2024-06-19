using RecommendationEngineClientSide.DTO;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<NotificationDTO> FetchNotificationsAsync(string userName);
        Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto);
        Task<OrderDetailRequestDto> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto);
        Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto);
    }
}
