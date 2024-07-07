using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class MenuFeedback
    {
        public int MenuFeedbackId { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MenuFeedbackQuestionId { get; set; }
        public virtual MenuFeedbackQuestion MenuFeedbackQuestion { get; set; }

        public string MenuFeedbackAnswer { get; set; }
    }
}
