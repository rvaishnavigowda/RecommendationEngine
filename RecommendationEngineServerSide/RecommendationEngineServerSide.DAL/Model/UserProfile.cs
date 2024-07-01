using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServerSide.DAL.Model
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PreferenceId { get; set; }
        public virtual ProfileAnswer ProfileAnswer { get; set; }
    }
}
