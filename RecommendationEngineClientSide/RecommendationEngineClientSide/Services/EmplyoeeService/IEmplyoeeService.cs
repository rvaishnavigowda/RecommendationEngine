using RecommendationEngineClientSide.DTO;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<NotificationDTO> FetchNotificationsAsync(string userName);
        Task<NotificationDTO> FetchFeedbackQuestion(string userName);
        Task<EmployeeUpdateDTO> FetchUserProfileQuestion(string userName);
        Task<SocketResponseDTO> SubmitUserProfileAnswers(UserProfileDetailDTO userProfileDetail);
        Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto);
        Task<OrderDetailRequestDto> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto);
        Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto);
    }
}
