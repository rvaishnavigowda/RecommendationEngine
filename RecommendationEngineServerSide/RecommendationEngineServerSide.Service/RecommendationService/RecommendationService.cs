using AutoMapper;
using Castle.Core.Smtp;
using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.Common.Enum;
using RecommendationEngineServerSide.Common.Exceptions;
using RecommendationEngineServerSide.DAL.Model;
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

            Console.WriteLine($"Feedbacks count: {feedbacks.Count()}");
            Console.WriteLine($"Menus count: {menus.Count()}");
            Console.WriteLine($"UserOrders count: {userOrders.Count()}");
            Console.WriteLine($"DailyMenus count: {dailyMenus.Count()}");

            var menuFeedbacks = feedbacks
                .GroupBy(f => f.MenuId)
                .Select(g => new
                {
                    MenuId = g.Key,
                    AverageRating = g.Average(f => f.Rating),
                    SentimentScore = g.Sum(f => SentimentAnalysisService.AnalyzeSentiment(f.Comment))
                })
                .ToDictionary(x => x.MenuId, x => new { x.AverageRating, x.SentimentScore });

            Console.WriteLine($"MenuFeedbacks count: {menuFeedbacks.Count}");

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

            Console.WriteLine($"MenuOrders count: {menuOrders.Count}");

            foreach (var menu in menus)
            {
                Console.WriteLine($"MenuId: {menu.MenuId}, MenuName: {menu.MenuName}, MenuStatus: {menu.MenuStatus}");
            }

            var recommendedMenuItems = menus
                .Where(m => m.MenuStatus == 1)
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

            Console.WriteLine($"RecommendedMenuItems count: {recommendedMenuItems.Count}");
            recommendedMenuItems.ForEach(item => Console.WriteLine($"MenuItem: {item.MenuItemName}, CombinedRating: {item.CombinedRating}"));

            return recommendedMenuItems;
        }
        public async Task<List<ListMenuDTO>> GetPoorRatedMenuList()
        {
            var menuList = await GetRecommendedMenuItems();
            List<ListMenuDTO> menuItems = new List<ListMenuDTO>();
            foreach(var menu in menuList)
            {
                if(menu.OrderCount>0)
                {
                    if (menu.CombinedRating < 2)
                    {
                        ListMenuDTO listMenu = new ListMenuDTO()
                        {
                            MenuItemName = menu.MenuItemName,
                            MenuItemType = menu.MenuItemType,
                            Rating = menu.CombinedRating,
                            OrderCount=menu.OrderCount
                        };
                        menuItems.Add(listMenu);
                    }
                }
                
            }
            if(menuList.Count > 0)
            {
                return menuItems;
            }
            else
            {
                throw MenuException.HandleNoMenuFound();
            }
        }
        public async Task<List<MenuItemDTO>> GetPersonalizedDailyMenu(int userId, DateTime date)
        {
            var userProfile = (await _unitOfWork.UserProfile.GetAll()).Where(a => a.UserId == userId).ToList();
            if (userProfile.Any())
            {
                List<ProfileAnswer> profileDetails = new List<ProfileAnswer>();
                foreach (var item in userProfile)
                {
                    var profileAnswers = (await _unitOfWork.ProfileAnswer.GetAll()).Where(a => a.PAId == item.ProfileAnswerId).ToList();
                    profileDetails.AddRange(profileAnswers);
                }

                var dietaryPreference = profileDetails.FirstOrDefault(pa => pa.ProfileQuestionId == 1)?.ProfileAnswerSolution;
                var preferredSpiceLevel = profileDetails.FirstOrDefault(pa => pa.ProfileQuestionId == 2)?.ProfileAnswerSolution;
                var regionalCuisinePreference = profileDetails.FirstOrDefault(pa => pa.ProfileQuestionId == 3)?.ProfileAnswerSolution;
                var sweetToothPreference = profileDetails.FirstOrDefault(pa => pa.ProfileQuestionId == 4)?.ProfileAnswerSolution;

                var dailyMenus = (await _unitOfWork.DailyMenu.GetAll()).Where(a => a.DailyMenuDate == date).ToList();
                var menus = await _unitOfWork.Menu.GetAll();
                var dailyMenuItems = dailyMenus
                    .Join(
                        menus,
                        dm => dm.MenuId,
                        m => m.MenuId,
                        (dm, m) => m
                    )
                    .Where(m => m.MenuStatus == 1)
                    .ToList();
                var filteredMenuItems = dailyMenuItems
                    .Where(m => dietaryPreference == "Vegetarian" ? m.FoodTypeId == (int)FoodType.Veg :
                                dietaryPreference == "Non-Vegetarian" ? m.FoodTypeId != (int)FoodType.Veg : true)
                    .OrderByDescending(m =>
                        (preferredSpiceLevel == "High" && m.SpiceLevel == (int)SpiceLevel.High ? 1 : 0) +
                        (preferredSpiceLevel == "Medium" && m.SpiceLevel == (int)SpiceLevel.Medium ? 1 : 0) +
                        (preferredSpiceLevel == "Low" && m.SpiceLevel == (int)SpiceLevel.Low ? 1 : 0) +
                        (regionalCuisinePreference == "North Indian" && m.CuisineTypeId == (int)CuisineType.NorthIndian ? 1 : 0) +
                        (regionalCuisinePreference == "South Indian" && m.CuisineTypeId == (int)CuisineType.SouthIndian ? 1 : 0) +
                        (sweetToothPreference == "Yes" && m.IsSweet ? 1 : 0)
                    )
                    .Select(m => new MenuItemDTO
                    {
                        MenuItemId = m.MenuId,
                        MenuItemName = m.MenuName,
                        MenuItemType = m.MenuType?.MenuTypeName,
                        Price = m.Price
                    })
                    .ToList();
                return filteredMenuItems;
            }
            else
            {
                var dailyMenuList = (await _unitOfWork.DailyMenu.GetAll()).Where(a => a.DailyMenuDate == date).ToList();
                List<MenuItemDTO> filteredMenuItems = new List<MenuItemDTO>();
                foreach (var item in dailyMenuList)
                {
                    MenuItemDTO menuItem = new MenuItemDTO()
                    {
                        MenuItemName = item.Menu.MenuName,
                        Price = item.Menu.Price,
                        MenuItemType = item.Menu.MenuType.MenuTypeName
                    };
                    filteredMenuItems.Add(menuItem);
                }
                return filteredMenuItems;
            }
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

