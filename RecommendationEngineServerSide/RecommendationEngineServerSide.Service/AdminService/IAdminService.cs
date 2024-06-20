using RecommendationEngineServerSide.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.AdminService
{
    public interface IAdminService
    {
        Task AddMenu(MenuDTO menuDTO);

        Task<FetchMenuDTO> GetMenuDetailsByName(string menuName);
        Task UpdateMenu(UpdateMenuDTO updateMenuDTO);

        Task DeleteMenu(DeleteMenuDTO deleteMenuDTO);
        Task<MenuListDTO> GetAllMenu();
    }
}
