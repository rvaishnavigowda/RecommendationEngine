using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.Exceptions
{
    public  class AdminException : CustomException
    {
        public AdminException(string message) : base(message)
        {
        }

        public static void HandleMenuTypeNotFound()
        {
            throw new CustomException("The menu type you are trying to add doesn't exist. Please try adding the menu type first.");
        }

        public static void HandleMenuItemAlreadyExists()
        {
            throw new CustomException("The menu item you are trying to add already exists.");
        }

        public static void HandleMenuItemNotFound()
        {
            throw new CustomException("The menu item you are trying to update doesn't exist.");
        }
    }
}
