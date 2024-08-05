using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class OrderDetailDTO : SocketResponseDTO
    {
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }

        
        public IList<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }

    public class OrderItemDTO
    {
        public string MenuName { get; set; }
        public string OrderMenutype { get; set; }
    }

    public class DailyOrderDetailsDTO : SocketResponseDTO
    {
        public List<string> OrderItem { get; set; }

    }
}
