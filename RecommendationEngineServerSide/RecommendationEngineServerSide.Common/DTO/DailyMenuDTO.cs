using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class DailyMenuDTO : SocketResponseDTO
    {
        public string UserName { get; set; }

        public DateTime CurrentDate { get; set; }

        public IList<DailyMenuList> MenuList { get; set; }
    }

    public class DailyMenuList
    {
        public string MenuName { get; set; }
        public decimal Price { get; set; }

        public int Rating { get; set; }
    }

   



}
