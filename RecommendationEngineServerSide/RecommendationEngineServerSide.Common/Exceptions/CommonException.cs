using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.Exceptions
{
    public  class CommonException :CustomException
    {
        public CommonException(string message) : base(message)
        {
        }
        public static Exception NullInputException()
        {
            return new CustomException("Improper input. Please fill the details");
        }
    }
}
