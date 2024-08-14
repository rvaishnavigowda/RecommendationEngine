using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.Exceptions
{
    public class MenuException:CustomException
    {
        public MenuException(string message) : base(message)
        {
        }

        public static MenuException HandleWrongMenuItem()
        {
            return new MenuException("The item you are trying to add doen't exist in the menu.");
        }

        public static MenuException HandleDailyMenuAdded()
        {
            return new MenuException("Daily menu for today has already been rolled out . You can't roll out menu twice");
        }

        public static MenuException HandleNoMenuFound()
        {
            return new MenuException("There was no item found that has to discarded this month.");
        }

        public static MenuException HandleNoNotification()
        {
            return new MenuException("It hasn't been a month since the last deletion of items.");
        }

        public static MenuException HandleMenuNOtRolled()
        {
            return new MenuException("You have not rolled out the menu for today yet.");
        }
    }
}
