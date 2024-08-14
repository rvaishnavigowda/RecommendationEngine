using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.EmplyoeeService
{
    public interface IEmplyoeeService
    {
        Task<List<string>> GetNotification(DailyMenuDTO userDetails);
        Task<NotificationDTO> GetMonthlyNotification(string userName);
        Task AddMenuImprovementFeedback(UserMenuUpgradeDTO feedback);
        Task<EmployeeUpdateDTO> GetUserPreference(string userName);
        Task UpdateUserProfile(UserProfileDetailDTO profile);
        Task<DailyMenuDTO> GetDailyMenuList(DailyMenuDTO dailyMenu);
        Task<OrderDetailDTO> PlaceOrder(OrderDetailDTO orderDetailDTO);
        Task<List<string>> GetOrderDetails(DateTime orderDate, string userName);
        Task GiveFeedback(FeedbackDTO feedbackDTO);
    }
}
