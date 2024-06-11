using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class SocketRequestDTO
    {
        public string? Controller {  get; set; }

        public string? Action { get; set; }

        public JsonElement Data { get; set; }
    }
}
