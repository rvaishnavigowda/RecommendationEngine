using System;

namespace RecommendationEngineServerSide.Common.Exceptions
{
    public class DailyMenuException : CustomException
    {
        public DailyMenuException(string message) : base(message)
        {
        }

        public static DailyMenuException OrderPlacedException()
        {
            return new DailyMenuException("You have already placed the order for the day.");
        }

        public static DailyMenuException MenuNotRolledException()
        {
            return new DailyMenuException("The menu for tomorrow has not been rolled out yet. Please check it after some time.");
        }
    }
}
