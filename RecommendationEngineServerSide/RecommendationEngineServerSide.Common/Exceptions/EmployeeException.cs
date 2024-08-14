using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.Exceptions
{
    public class EmployeeException : CustomException
    {
        public EmployeeException(string message) : base(message)
        {

        }

        public static EmployeeException HandleOrderNotPlaced()
        {
            return new EmployeeException("You hadn't placed any orders earlier to give feedback.");
        }

        public static EmployeeException HandleFeedbackGiven()
        {
            return new EmployeeException("The feedback to the menu item is already given.");
        }

        public static EmployeeException HandleNoFeedback() 
        {
            return new EmployeeException("There are no monthly notifications.");
        }
    }
}
