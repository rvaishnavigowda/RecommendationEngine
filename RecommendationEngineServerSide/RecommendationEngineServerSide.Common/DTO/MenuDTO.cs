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
        public DateTime dateCreated { get; set; }
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
    public class FetchMenuRequestDTO
    {
        public string MenuName { get; set; }
    }

    public class FetchMenuResponseDTO
    {
        public string MenuName { get; set; }
        public string MenuType { get; set; }
        public decimal MenuPrice { get; set; }
    }

    public class FetchMenuDTO:SocketResponseDTO
    {
        public IList<FetchMenuResponseDTO> MenuList { get; set; }
    }

}
