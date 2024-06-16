using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.ChefServices
{
    public interface IChefService
    {
        Task<SocketResponseDTO> GetMenuListAsync(DateTime date);
        Task<SocketResponseDTO> AddDailyMenuAsync(NewDailyMenuDto newDailyMenuDto);
    }
}
