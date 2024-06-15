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

        public static AdminException HandleMenuTypeNotFound()
        {
            return new AdminException("The menu type you are trying to add doesn't exist. Please try adding the menu type first.");
        }

        public static AdminException HandleMenuItemAlreadyExists()
        {
            return new AdminException("The menu item you are trying to add already exists.");
        }

        public static AdminException HandleMenuItemNotFound()
        {
            return new AdminException("The menu item you are trying to update doesn't exist.");
        }
    }
}
