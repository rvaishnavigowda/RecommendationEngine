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
            return new MenuException("Daily menu for today has already been added. You can't add menu twice");
        }
    }
}
