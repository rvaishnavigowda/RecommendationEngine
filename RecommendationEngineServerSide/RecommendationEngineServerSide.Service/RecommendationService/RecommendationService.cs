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
            var dailyMenus = await _unitOfWork.DailyMenu.GetAll();

            var menuFeedbacks = feedbacks
                .GroupBy(f => f.MenuId)
                .Select(g => new
                {
                    MenuId = g.Key,
                    AverageRating = g.Average(f => f.Rating),
                    SentimentScore = g.Sum(f => SentimentAnalysisService.AnalyzeSentiment(f.Comment))
                })
                .ToDictionary(x => x.MenuId, x => new { x.AverageRating, x.SentimentScore });

            var menuOrders = dailyMenus
                .Join(
                    userOrders,
                    dm => dm.DailyMenuId,
                    uo => uo.DailyMenuId,
                    (dm, uo) => new { MenuId = dm.MenuId, OrderId = uo.OrderId }
                )
                .GroupBy(d => d.MenuId)
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
                    OrderCount = menuOrders.ContainsKey(m.MenuId) ? menuOrders[m.MenuId] : 0,
                    CombinedRating = CalculateCombinedRating(
                menuFeedbacks.ContainsKey(m.MenuId) ? menuFeedbacks[m.MenuId].AverageRating : 0,
                menuFeedbacks.ContainsKey(m.MenuId) ? menuFeedbacks[m.MenuId].SentimentScore : 0,
                menuOrders.ContainsKey(m.MenuId) ? menuOrders[m.MenuId] : 0)
                })
                .OrderByDescending(m => m.CombinedRating)
                .ToList();

            return recommendedMenuItems;
        }

        private double CalculateCombinedRating(double averageRating, double sentimentScore, int orderCount)
        {
            double ratingWeight = 0.4;
            double sentimentWeight = 0.3;
            double orderWeight = 0.3;

            return (averageRating * ratingWeight) + (sentimentScore * sentimentWeight) + (orderCount * orderWeight);
        }
    }

   
}

