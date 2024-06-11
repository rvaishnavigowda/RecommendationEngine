using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class MenuDTO:SocketResponseDTO
    {
        public string MenuName {  get; set; }
        
        public string MenuType { get; set; }

        public decimal MenuPrice { get; set; }
    }

    public class UpdateMenuDTO : MenuDTO
    {
        
    }

    public class DeleteMenuDTO: MenuDTO
    {

    }

    public class ResponseMenuDTO:MenuDTO
    {

    }
}
