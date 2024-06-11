﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClientSide.Services
{
    public interface IRequestService
    {
        Task<string> SendRequestAsync(string requestJson);
    }
}
