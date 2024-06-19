using AutoMapper;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.RecommendationService
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecommendationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MenuItemDTO>> GetRecommendedMenuItems()
        {
            var feedbacks = await _unitOfWork.Feedback.GetAll();
            var menus = await _unitOfWork.Menu.GetAll();
            var userOrders = await _unitOfWork.UserOrder.GetAll();

            var menuFeedbacks = feedbacks
                .GroupBy(f => f.MenuId)
                .Select(g => new
                {
                    MenuId = g.Key,
                    AverageRating = g.Average(f => f.Rating),
                    SentimentScore = g.Sum(f => SentimentAnalyzer.AnalyzeSentiment(f.Comment))
                })
                .ToDictionary(x => x.MenuId, x => new { x.AverageRating, x.SentimentScore });

            var menuOrders = userOrders
                .GroupBy(uo => uo.DailyMenuId)
                .Select(g => new
                {
                    MenuId = g.Key,
                    OrderCount = g.Count()
                })
                .ToDictionary(x => x.MenuId, x => x.OrderCount);

            var recommendedMenuItems = menus
                .Where(m => !m.ISDeleted)
                .Select(m => new MenuItemDTO
                {
                    MenuItemId = m.MenuId,
                    MenuItemName = m.MenuName,
                    MenuItemType = m.MenuType?.MenuTypeName,
                    Price = m.Price,
                    AverageRating = menuFeedbacks.ContainsKey(m.MenuId) ? menuFeedbacks[m.MenuId].AverageRating : 0,
                    SentimentScore = menuFeedbacks.ContainsKey(m.MenuId) ? menuFeedbacks[m.MenuId].SentimentScore : 0,
                    OrderCount = menuOrders.ContainsKey(m.MenuId) ? menuOrders[m.MenuId] : 0
                })
                .OrderByDescending(m => CalculateFinalScore(m))
                .ToList();

            return recommendedMenuItems;
        }

        private double CalculateFinalScore(MenuItemDTO menuItem)
        {
            double ratingWeight = 0.4;
            double sentimentWeight = 0.3;
            double orderWeight = 0.3;

            return (menuItem.AverageRating * ratingWeight) + (menuItem.SentimentScore * sentimentWeight) + (menuItem.OrderCount * orderWeight);
        }
    }

    public static class SentimentAnalyzer
    {
        private static readonly HashSet<string> PositiveWords = new HashSet<string> { "good", "great", "excellent", "amazing", "awesome", "fantastic" };
        private static readonly HashSet<string> NegativeWords = new HashSet<string> { "bad", "poor", "terrible", "awful", "horrible" };

        public static double AnalyzeSentiment(string comment)
        {
            var words = comment.ToLower().Split(' ');
            double score = 0;

            foreach (var word in words)
            {
                if (PositiveWords.Contains(word))
                {
                    score += 1;
                }
                else if (NegativeWords.Contains(word))
                {
                    score -= 1;
                }
            }

            return score;
        }
    }
}




    

