using RecommendationEngineClientSide.DTO;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<NotificationDTO> FetchNotificationsAsync(DailyMenuRequestDto userDetails);
        Task<NotificationDTO> FetchFeedbackQuestion(string userName);
        Task<SocketResponseDTO> UpdateMenuUpgradeFeedback(UserMenuUpgradeListDTO userMenuUpgradeList);
        Task<EmployeeUpdateDTO> FetchUserProfileQuestion(string userName);
        Task<SocketResponseDTO> SubmitUserProfileAnswers(UserProfileDetailDTO userProfileDetail);
        Task<DailyMenuResponseDto> GetDailyMenuAsync(DailyMenuRequestDto dailyMenuRequestDto);
        Task<OrderDetailRequestDto> PlaceOrderAsync(OrderDetailRequestDto orderDetailRequestDto);
        Task<DailyOrderDetailsDTO> GetOrderList(DailyMenuRequestDto dailyMenuRequest);
        Task<SocketResponseDTO> GiveFeedbackAsync(FeedbackDto feedbackDto);
    }
}
