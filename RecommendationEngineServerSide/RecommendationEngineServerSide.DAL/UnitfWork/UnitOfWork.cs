using RecommendationEngineServerSide.DAL.Context;
using RecommendationEngineServerSide.DAL.Model;
using RecommendationEngineServerSide.DAL.Repository.DailyMenuRepo;
using RecommendationEngineServerSide.DAL.Repository.FeedbackRepo;
using RecommendationEngineServerSide.DAL.Repository.MenuRepo;
using RecommendationEngineServerSide.DAL.Repository.MenuTypeRepo;
using RecommendationEngineServerSide.DAL.Repository.NotificationRepo;
using RecommendationEngineServerSide.DAL.Repository.NotificationTypeRepo;
using RecommendationEngineServerSide.DAL.Repository.OrderRepo;
using RecommendationEngineServerSide.DAL.Repository.UserNotificationRepo;
using RecommendationEngineServerSide.DAL.Repository.UserOrderRepo;
using RecommendationEngineServerSide.DAL.Repository.UserRepo;
using RecommendationEngineServerSide.DAL.Repository.UserTypeRepo;
using System;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.UnitfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;

        public UnitOfWork(DBContext context)
        {
            _context = context;
            Feedback = new FeedbackRepository(_context);
            DailyMenu = new DailyMenuRepository(_context);
            Menu = new MenuRepository(_context);
            MenuType = new MenuTypeRepository(_context);
            Order = new OrderRepository(_context);
            User = new UserRepository(_context);
            UserType = new UserTypeRepository(_context);
            UserOrder = new UserOrderRepository(_context);  
            Notification= new NotificationRepository(_context);
            NotificationType = new NotificationTypeRepository(_context);
            UserNotification= new UserNotificationRepository(_context);
        }

        public IFeedbackRepository Feedback { get; }
        public IDailyMenuRepository DailyMenu { get; }
        public IMenuRepository Menu { get; }
        public IMenuTypeRepository MenuType { get; }
        public IOrderRepository Order { get; }
        public IUserRepository User { get; }
        public IUserTypeRepository UserType { get; }

        public INotificationTypeRepository NotificationType { get; }
        public INotificationRepository Notification { get; }

        public IUserOrderRepository UserOrder { get; }

        public IUserNotificationRepository UserNotification { get; }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
