using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.Enum
{
    public enum NotificationTypeEnum
    {
        NewMenuItemAdded = 1,
        NextDayMenuRecommendation = 2,
        AvailabilityStatusOfMenuItems = 3,
        ImproveMenuItem = 4
    }

    public enum UserType
    {
        Employee=1,
        Chef,
        Admin
    }

    public enum MenuStatus
    {
        Active=1,
        Deletd=2,
        DIscarded=3,
    }
}
