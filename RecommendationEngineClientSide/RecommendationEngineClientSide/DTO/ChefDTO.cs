using RecommendationEngineClientSide.DTO;

namespace RecommendationEngineClientSide.ChefDTO
{
    public class DailyMenuDto : SocketResponseDTO
    {
        public string UserName { get; set; }
        public DateTime CurrentDate { get; set; }
        public IList<DailyMenuListDto> MenuList { get; set; }
    }

    public class DailyMenuListDto
    {
        public string MenuName { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }

    public class ListMenuDto
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

    public class MenuListDto : SocketResponseDTO
    {
        public IList<ListMenuDto> Menu { get; set; }
    }

    public class NewDailyMenuDto
    {
        public DateTime CurrentDate { get; set; }
        public IList<MenuListItemDto> Menu { get; set; }
    }

    public class MenuListItemDto
    {
        public string MenuItemName { get; set; }
    }
}
