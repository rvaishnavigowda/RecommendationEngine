using RecommendationEngineServerSide.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.RegisterService
{
    public interface IRegisterService
    {
        Task<UserDTO> LoginValidation(UserDTO UserDTO);
    }
}
