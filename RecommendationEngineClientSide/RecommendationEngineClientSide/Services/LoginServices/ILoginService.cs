using RecommendationEngineClientSide.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services.LoginServices
{
    public interface ILoginService
    {
        Task<string> HandleLoginAsync(LoginRequestDto loginRequestDto);
    }
}
