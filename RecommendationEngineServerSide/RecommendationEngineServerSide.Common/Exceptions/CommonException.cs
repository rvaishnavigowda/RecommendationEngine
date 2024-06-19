﻿using System;
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
        public static CommonException NullInputException()
        {
            return new CommonException("Improper input. Please fill the details");
        }

        public static CommonException HandleNullNotification()
        {
            return new CommonException("There are no notifications to show");
        }
    }
}
