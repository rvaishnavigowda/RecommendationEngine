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
        Task<SocketResponseDTO> AddMenuAsync(AddMenuRequestDto addMenuRequestDto);
        Task<SocketResponseDTO> UpdateMenuAsync(UpdateMenuRequestDto updateMenuRequestDto);
        Task<SocketResponseDTO> DeleteMenuAsync(DeleteMenuRequestDto deleteMenuRequestDto);
    }
}
