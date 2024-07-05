﻿using RecommendationEngineServerSide.Common.DTO;
using RecommendationEngineServerSide.DAL.UnitfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.EmplyoeeService
{
    public interface IEmplyoeeService
    {
        Task<List<string>> GetNotification(string userName);
        Task<NotificationDTO> UpgradeMenuFeedback(string userName);
        Task<EmployeeUpdateDTO> GetUserPreference(string userName);
        Task<DailyMenuDTO> GetDailyMenuList(DailyMenuDTO dailyMenu);
        Task<OrderDetailDTO> PlaceOrder(OrderDetailDTO orderDetailDTO);
        Task GiveFeedback(FeedbackDTO feedbackDTO);
    }
}
