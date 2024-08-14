using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class MenuItemDTO
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuItemType { get; set; }
        public decimal Price { get; set; }
        public double AverageRating { get; set; }
        public double SentimentScore { get; set; }
        public int OrderCount { get; set; }
        public double CombinedRating { get; set; }
    }
}
