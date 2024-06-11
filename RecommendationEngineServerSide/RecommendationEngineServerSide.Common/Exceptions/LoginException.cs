using System;

namespace RecommendationEngineServerSide.Common.Exceptions
{
    public class LoginException : CustomException
    {
        public LoginException(string message) : base(message)
        {
        }

        public static LoginException WrongPassword()
        {
            return new LoginException("Wrong password. Please try again.");
        }

        public static LoginException NoUserPresent()
        {
            return new LoginException("The user by this name doesn't exist. Please try again.");
        }
    }
}
