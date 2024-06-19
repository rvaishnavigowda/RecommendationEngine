using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Service.NotificationService
{
    public interface INotificationService
    {
        Task AddNotification(string notificationMessage, int notificationType, DateTime notificationDate);
        Task<List<string>> GetNotification(int userId);
    }
}
