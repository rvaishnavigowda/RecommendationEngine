using RecommendationEngineClientSide.ChefDTO;
using RecommendationEngineClientSide.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.AdminServices
{
    public interface IAdminService
    {
        Task<MenuListDto> GetAllMenuAsync();
        Task<SocketResponseDTO> AddMenuAsync(AddMenuRequestDto addMenuRequestDto);
        Task<FetchMenuDTO> FetchMenuDetailsAsync(FetchMenuRequestDTO fetchMenuRequestDto);
        Task<SocketResponseDTO> UpdateMenuAsync(UpdateMenuRequestDto updateMenuRequestDto);
        Task<SocketResponseDTO> DeleteMenuAsync(DeleteMenuRequestDto deleteMenuRequestDto);
    }
}
