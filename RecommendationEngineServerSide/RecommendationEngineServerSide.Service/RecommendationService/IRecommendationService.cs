using RecommendationEngineServerSide.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.RecommendationService
{
    public interface IRecommendationService
    {
        Task<List<ListMenuDTO>> GetPoorRatedMenuList();
        Task<List<MenuItemDTO>> GetRecommendedMenuItems();
        Task<List<MenuItemDTO>> GetPersonalizedDailyMenu(int userId, DateTime date);
    }
}
