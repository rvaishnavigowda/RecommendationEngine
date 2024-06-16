﻿namespace RecommendationEngineClientSide.DTO
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
        public int Rating { get; set; }
    }

    public class OrderDetailRequestDto
    {
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public IList<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public string MenuName { get; set; }
        public int Quantity { get; set; }
    }

    public class FeedbackDto
    {
        public string UserName { get; set; }
        public string MenuName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
