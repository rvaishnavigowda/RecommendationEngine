using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class ProfileAnswer
    {
        [Key]
        public int PAId { get; set; }
        public string ProfileAnswerSolution { get; set;}
        public int ProfileQuestionId { get; set; }
        public virtual ProfileQuestion ProfileQuestion { get; set; }

    }
}
