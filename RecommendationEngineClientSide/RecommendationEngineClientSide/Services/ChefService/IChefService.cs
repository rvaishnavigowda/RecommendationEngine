using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.ChefServices
{
    public interface IChefService
    {
        Task<MenuListDto> FetchMonthlyNotificationASync(DateTime date);
        Task<NotificationDTO> FetchNotificationsAsync(string userName);
        Task<MenuListDto> GetMenuListAsync(DateTime date);
        Task<SocketResponseDTO> AddDailyMenuAsync(NewDailyMenuDto newDailyMenuDto);
        Task<SocketResponseDTO> UpgradeFoodItemAsync(UpgradeMenuDto upgradeMenu);
        Task<SocketResponseDTO> RemoveFoodItemAsync(string itemName);
    }
}
