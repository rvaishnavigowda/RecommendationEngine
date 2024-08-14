using RecommendationEngineServerSide.DAL.Repository.DailyMenuRepo;
using RecommendationEngineServerSide.DAL.Repository.FeedbackRepo;
using RecommendationEngineServerSide.DAL.Repository.MenuFeedbackQuestionRepo;
using RecommendationEngineServerSide.DAL.Repository.MenuFeedbackRepo;
using RecommendationEngineServerSide.DAL.Repository.MenuRepo;
using RecommendationEngineServerSide.DAL.Repository.MenuTypeRepo;
using RecommendationEngineServerSide.DAL.Repository.NotificationRepo;
using RecommendationEngineServerSide.DAL.Repository.NotificationTypeRepo;
using RecommendationEngineServerSide.DAL.Repository.OrderRepo;
using RecommendationEngineServerSide.DAL.Repository.ProfileAnswerRepo;
using RecommendationEngineServerSide.DAL.Repository.ProfileQuestionRepo;
using RecommendationEngineServerSide.DAL.Repository.UserNotificationRepo;
using RecommendationEngineServerSide.DAL.Repository.UserOrderRepo;
using RecommendationEngineServerSide.DAL.Repository.UserProfileRepo;
using RecommendationEngineServerSide.DAL.Repository.UserRepo;
using RecommendationEngineServerSide.DAL.Repository.UserTypeRepo;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.UnitfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFeedbackRepository Feedback { get; }
        IDailyMenuRepository DailyMenu { get; }
        IMenuRepository Menu { get; }
        IMenuTypeRepository MenuType { get; }
        IOrderRepository Order { get; }
        IUserRepository User { get; }
        IUserTypeRepository UserType { get; }

        IUserOrderRepository UserOrder { get; }

        INotificationRepository Notification { get; }

        INotificationTypeRepository NotificationType { get; }
        IUserNotificationRepository UserNotification { get; }

        IMenuFeedbackQuestionRepository MenuFeedbackQuestion { get; }

        IMenuFeedbackRepository MenuFeedback { get; }

        IUserProfileRepository UserProfile { get; }

        IProfileQuestionRepository ProfileQuestion { get; }
        IProfileAnwerRepository ProfileAnswer { get; }

        Task Save(); 

    }
}
