using RecommendationEngineServerSide.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.ChefService
{
    public interface IChefService
    {
        Task<List<string>> GetNotification(string userName);
        Task<MenuListDTO> GetMenuList(DateTime date);
        Task AddDailyMenu(NewDailyMenuDTO menuList);

    }
}
