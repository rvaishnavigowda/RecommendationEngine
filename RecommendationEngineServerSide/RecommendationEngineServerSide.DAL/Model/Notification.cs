using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string NotificationMessage { get; set; }
        public int NotificationTypeId { get; set; }

        public virtual NotificationType NotificationType { get; set; }

        public bool IsDeleted { get; set; }
    }
}
