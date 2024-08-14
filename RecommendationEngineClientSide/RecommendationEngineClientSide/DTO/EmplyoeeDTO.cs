namespace RecommendationEngineClientSide.DTO
{
    public class DailyMenuRequestDto
    {
        public string UserName { get; set; }
        public DateTime CurrentDate { get; set; }
    }

    public class DailyMenuResponseDto : SocketResponseDTO
    {
        public IList<DailyMenuListDto> MenuList { get; set; }
    }

    public class DailyMenuListDto
    {
        public string MenuName { get; set; }
        public decimal Price { get; set; }
        public string MenuType { get; set; }
        public decimal Rating { get; set; }
    }

    public class OrderDetailRequestDto : SocketResponseDTO
    {
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        
        public IList<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public string MenuName { get; set; }
        public string OrderMenutype { get; set; }

    }

    public class OrderDTO :SocketResponseDTO
    {
        public string MenuName { get; set; }
        public int OrderCount { get; set; }

        public List<OrderDTO> OrderList { get; set; }
    }

    public class FeedbackDto
    {
        public string UserName { get; set; }
        public string MenuName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime FeedbackDate { get; set; }
    }

    public class UserMenuUpgradeListDTO
    {
        public string UserName { get; set; }
        public string MenuName { get; set; }
        public List<string> menuFeedback { get; set; }
    }
    public class DailyOrderDetailsDTO : SocketResponseDTO
    {
        public List<string> OrderItem { get; set; }

    }
}
