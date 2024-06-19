using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class UserNotification
    {
        public int UserNotificationId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

    }
}
