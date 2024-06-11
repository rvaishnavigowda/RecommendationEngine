using RecommendationEngineClientSide.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services
{
    public interface IAdminService
    {
        Task<string> AddMenuAsync(AddMenuRequestDto addMenuRequestDto);
        Task<string> UpdateMenuAsync(UpdateMenuRequestDto updateMenuRequestDto);
        Task<string> DeleteMenuAsync(DeleteMenuRequestDto deleteMenuRequestDto);
    }
}
