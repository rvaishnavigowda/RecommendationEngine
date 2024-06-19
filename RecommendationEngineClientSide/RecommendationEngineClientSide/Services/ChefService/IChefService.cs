using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.ChefServices
{
    public interface IChefService
    {
        Task<NotificationDTO> FetchNotificationsAsync(string userName);
        Task<MenuListDto> GetMenuListAsync(DateTime date);
        Task<SocketResponseDTO> AddDailyMenuAsync(NewDailyMenuDto newDailyMenuDto);
    }
}
