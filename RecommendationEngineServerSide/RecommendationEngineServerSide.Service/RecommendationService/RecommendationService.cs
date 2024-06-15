using AutoMapper;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


    }
}

//public class RecommendationService
//{
//    private readonly IUnitOfWork _unitOfWork;

//    public RecommendationService(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//    }

//    public async Task<List<MenuItemDTO>> GetRecommendationsForChef(int topN = 5)
//    {
//        var allOrders = await _unitOfWork.Order.GetAll();
//        var allFeedback = await _unitOfWork.Feedback.GetAll();

//        var orderData = allOrders.GroupBy(o => o.MenuItemId)
//                                 .Select(g => new
//                                 {
//                                     MenuItemId = g.Key,
//                                     OrderCount = g.Count(),
//                                     Orders = g.ToList()
//                                 })
//                                 .ToList();

//        var feedbackData = allFeedback.GroupBy(f => f.MenuItemId)
//                                      .Select(g => new
//                                      {
//                                          MenuItemId = g.Key,
//                                          AverageRating = g.Average(f => f.Rating),
//                                          Comments = g.Select(f => f.Comment).ToList()
//                                      })
//                                      .ToList();

//        // Combine order and feedback data
//        var combinedData = orderData.Join(feedbackData, o => o.MenuItemId, f => f.MenuItemId, (o, f) => new
//        {
//            o.MenuItemId,
//            o.OrderCount,
//            f.AverageRating,
//            o.Orders,
//            f.Comments
//        }).ToList();

//        // Calculate scores
//        var scoredItems = combinedData.Select(data => new MenuItemDTO
//        {
//            MenuItemId = data.MenuItemId,
//            Score = CalculateScore(data)
//        }).OrderByDescending(item => item.Score)
//          .Take(topN)
//          .ToList();

//        return scoredItems;
//    }

//    private double CalculateScore(dynamic data)
//    {
//        double score = 0;

//        // Order frequency factor
//        score += data.OrderCount * 0.4;

//        // Rating factor
//        score += data.AverageRating * 0.4;

//        // Day-specific trend factor (e.g., if the item was popular on the same day last week/month)
//        var dayTrend = data.Orders.Where(o => o.OrderDate.DayOfWeek == DateTime.Now.DayOfWeek).Count();
//        score += dayTrend * 0.2;

//        return score;
//    }
//}

//public class MenuItemDTO
//{
//    public int MenuItemId { get; set; }
//    public double Score { get; set; }
//}
