using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.Common.DTO
{
    public class FeedbackDTO 
    {
        public string UserName { get; set; }
        public string MenuName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime FeedbackDate { get; set; }

    }
}
