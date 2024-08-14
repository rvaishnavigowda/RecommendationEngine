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

        public string MenuType { get; set; }
        public double Rating { get; set; }
    }

    public class ListMenuDTO
    {
        public string MenuItemName { get; set; }
        public string MenuItemType { get; set; }
        public decimal Price { get; set; }

        //public double AverageRating { get; set; }
        //public double SentimentScore { get; set; }
        public double Rating { get; set; }
        public int OrderCount { get; set; }

        //public double CombinedRating { get; set; }
    }
    public class MenuListDTO : SocketResponseDTO
    {
        public IList<ListMenuDTO> Menu { get; set;}
    }

    public class NewDailyMenuDTO
    {
        public DateTime CurrentDate { get; set; }
        public IList<MenuList> Menu { get; set;}
    }

    public class MenuList
    {
        public string MenuItemName { get; set; }
    }
}
