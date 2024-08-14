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

    public enum FoodType
    {
        Veg=1,
        NonVeg,
        Egg,
    }

    public enum CuisineType
    {
        NorthIndian=1,
        SouthIndian,
        Chinese
    }
    public enum SpiceLevel
    {
        Low=1,
        Medium,
        High
    }
    public enum SweetLevel
    {
        No=0,
        Yes=1
    }
}
