using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class UserDTO:SocketResponseDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? UserRole { get; set; }

    }

    public class ResponseUserDTO: UserDTO
    {
        
    }

}
