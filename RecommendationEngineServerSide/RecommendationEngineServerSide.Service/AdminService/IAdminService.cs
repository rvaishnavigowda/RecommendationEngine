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

        Task UpdateMenu(UpdateMenuDTO updateMenuDTO);

        Task DeleteMenu(DeleteMenuDTO deleteMenuDTO);
    }
}
