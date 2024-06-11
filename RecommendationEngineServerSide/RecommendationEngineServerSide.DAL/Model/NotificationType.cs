using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class NotificationType
    {
        [Key]
        public int NotificationTypeId { get; set; }

        public string? NotificationTypeName { get; set; }

        public int IsDeleted { get; set; }
    }
}
