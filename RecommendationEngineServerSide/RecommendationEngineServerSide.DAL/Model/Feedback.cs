using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime FeedbackDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Menu? Menu { get; set; }

        public int ISDeleted { get; set; }
    }
}
